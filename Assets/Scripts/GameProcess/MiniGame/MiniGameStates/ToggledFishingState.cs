using Player.Camera;
using UnityEngine;
using Utilities.EventBus;
using Utilities.FSM;

namespace GameProcess.MiniGame.MiniGameStates
{
    public class ToggledFishingState : State
    {
        private readonly EventBus _eventBus;
        
        public ToggledFishingState(StateType stateType, EventBus eventBus)
        {
            StateType = stateType;
            
            _eventBus = eventBus;
        }
        
        public override void Enter()
        {
            Debug.Log($"ToggledFishingState Enter");
            
            _eventBus.Publish(CameraPosition.Fishing);
        }

        public override void Update()
        {
            
        }

        public override void Exit()
        {
            
        }
    }
}