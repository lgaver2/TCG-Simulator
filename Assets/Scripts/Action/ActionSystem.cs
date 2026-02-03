using System;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks; // Required Namespace

public class ActionSystem : Singleton<ActionSystem>
{
    private List<GameAction> reactions = null;
    private static Dictionary<Type, List<Action<GameAction>>> preSubs = new();
    private static Dictionary<Type, List<Action<GameAction>>> postSubs = new();
    private static Dictionary<Type, Func<GameAction, UniTask>> performers = new();
    private static Dictionary<Delegate, Action<GameAction>> delegateWrappers = new();

    public bool IsPerforming { get; private set; } = false;
    

    public async UniTaskVoid Perform(GameAction action, Action OnPerformFinished = null)
    {
        if (IsPerforming) return;
        
        IsPerforming = true;
        
        await Flow(action);

        IsPerforming = false;
        OnPerformFinished?.Invoke();
    }

    public void AddAction(GameAction action)
    {
        reactions?.Add(action);
    }

    private async UniTask Flow(GameAction action, Action OnFlowFinished = null)
    {
        reactions = action.PreReactions;
        PerformSubscribers(action, preSubs);
        await PeformReactions();

        reactions = action.PerformedActions;
        await PerformPerformer(action);
        await PeformReactions();
        
        reactions = action.PostReactions;
        PerformSubscribers(action, postSubs);
        await PeformReactions();

        OnFlowFinished?.Invoke();
    }

    private async UniTask PeformReactions()
    {
        foreach (var reaction in reactions)
        {
            await Flow(reaction);
        }
    }

    private void PerformSubscribers(GameAction action, Dictionary<Type, List<Action<GameAction>>> subs)
    {
        Type type = action.GetType();
        if (subs.ContainsKey(type))
        {
            var listeners = new List<Action<GameAction>>(subs[type]);
            foreach (var sub in listeners)
            {
                sub(action);
            }
        }
    }

    private async UniTask PerformPerformer(GameAction action)
    {
        Type type = action.GetType();
        if (performers.ContainsKey(type))
        {
            await performers[type](action);
        }
    }

    public static void AttachPerformer<T>(Func<T, UniTask> performer) where T : GameAction
    {
        Type type = typeof(T);
        
        UniTask wrappedPerformer(GameAction action) => performer((T)action);
        
        if (performers.ContainsKey(type)) 
            performers[type] = wrappedPerformer;
        else 
            performers.Add(type, wrappedPerformer);
    }

    public static void DetachPerformer<T>() where T : GameAction
    {
        Type type = typeof(T);
        if (performers.ContainsKey(type)) performers.Remove(type);
    }

    public static void SubscribeReaction<T>(Action<T> reaction, ReactionTiming timing) where T : GameAction
    {
        var subs = timing == ReactionTiming.PRE ? preSubs : postSubs;
        
        if (delegateWrappers.ContainsKey(reaction)) return;

        Action<GameAction> wrappedReaction = (action) => reaction((T)action);
        delegateWrappers.Add(reaction, wrappedReaction);

        if (!subs.ContainsKey(typeof(T)))
        {
            subs.Add(typeof(T), new List<Action<GameAction>>());
        }
        subs[typeof(T)].Add(wrappedReaction);
    }

    public static void UnSubscribeReaction<T>(Action<T> reaction, ReactionTiming timing) where T : GameAction
    {
        var subs = timing == ReactionTiming.PRE ? preSubs : postSubs;

        if (delegateWrappers.TryGetValue(reaction, out var wrapper))
        {
            if (subs.ContainsKey(typeof(T)))
            {
                subs[typeof(T)].Remove(wrapper);
            }
            delegateWrappers.Remove(reaction);
        }
    }
}


public enum ReactionTiming
{
    PRE,
    POST
}