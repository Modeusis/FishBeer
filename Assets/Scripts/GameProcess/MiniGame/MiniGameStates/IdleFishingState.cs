using UnityEngine;
using Utilities.FSM;

namespace GameProcess.MiniGame.MiniGameStates
{
    public class IdleFishingState : State
    {
        private readonly FishingRodAnimationHandler _fishingRodAnimationHandler;
        
        public IdleFishingState(StateType stateType, FishingRodAnimationHandler fishingRodAnimationHandler)
        {
            StateType = stateType;
            
            _fishingRodAnimationHandler  = fishingRodAnimationHandler;
        }
        
        public override void Enter()
        {
            Debug.Log("IdleFishingState Enter");
            
            _fishingRodAnimationHandler.Idle();
        }

        public override void Update()
        {
            
        }

        public override void Exit()
        {
            
        }
    }
}