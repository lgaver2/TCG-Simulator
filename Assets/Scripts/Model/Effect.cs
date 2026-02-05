using System;
using System.Collections.Generic;
using UnityEngine;

public interface Effect
{
    public GameAction GetGameAction(List<CardView> targets);
}
