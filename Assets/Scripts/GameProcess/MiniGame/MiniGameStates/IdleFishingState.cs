using UnityEngine;
using Utilities.FSM;

namespace GameProcess.MiniGame.MiniGameStates
{
    public class IdleFishingState : State
    {
        public IdleFishingState(StateType stateType)
        {
            StateType = stateType;
        }
        
        public override void Enter()
        {
            Debug.Log("IdleFishingState Enter");
        }

        public override void Update()
        {
            
        }

        public override void Exit()
        {
            
        }
    }
}