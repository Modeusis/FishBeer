using UnityEngine;
using Utilities.FSM;

namespace GameProcess.MiniGame.MiniGameStates
{
    public class IdleFishingState : State
    {
        private readonly FishingRodAnimationHandler _fishingRodAnimationHandler;
        
        private readonly FishingLineHandler _fishingLineHandler;
        
        public IdleFishingState(StateType stateType, FishingRodAnimationHandler fishingRodAnimationHandler, FishingLineHandler fishingLineHandler)
        {
            StateType = stateType;
            
            _fishingRodAnimationHandler  = fishingRodAnimationHandler;
            
            _fishingLineHandler = fishingLineHandler;
        }
        
        public override void Enter()
        {
            _fishingRodAnimationHandler.Idle();
            
            _fishingLineHandler.HideFishingLine();
        }

        public override void Update()
        {
            
        }

        public override void Exit()
        {
            
        }
    }
}