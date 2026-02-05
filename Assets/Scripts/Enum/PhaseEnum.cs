using UnityEngine;

namespace Phases
{
    public enum PhaseEnum
    {
        StartPhase,
        MainPhase,
        AttackPhase,
        EndPhase
    }

    public enum StartStepEnum
    {
        UntapStep,
        CollectStep,
        ProduceStep,
        DrawStep,
        MoveStep
    }

    public enum AttackStepEnum
    {
        AttackDeclarationStep,
        
    }
}