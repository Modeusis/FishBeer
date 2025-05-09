using UnityEngine;
using Utilities.FSM;

namespace GameProcess.MiniGame.MiniGameStates
{
    public class ActiveFishingState : State
    {
        public ActiveFishingState(StateType stateType)
        {
            StateType = stateType;
        }
        
        public override void Enter()
        {
            Debug.Log("ActiveFishingState Enter");
        }

        public override void Update()
        {
            
        }

        public override void Exit()
        {
            
        }
    }
}