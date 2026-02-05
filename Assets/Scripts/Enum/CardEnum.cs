using UnityEngine;

namespace  CardEnum
{
    public enum CardType
    {
        Royal,
        Monster,
        Action
    }

    public enum CardLocation
    {
        HandZone,
        BattleZone,
        FortressZone,
        DropZone
    }

    public enum TargetType
    {
        Player,
        Monster,
    }

    public enum TargetModeEnum
    {
        None,
        Manual,
        Auto
    }

    public enum EqualityType
    {
        Equal,
        NotEqual,
        Less,
        Big
    }
}