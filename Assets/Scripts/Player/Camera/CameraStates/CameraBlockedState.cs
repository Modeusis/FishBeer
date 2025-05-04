using UnityEngine;
using Utilities.FSM;

namespace Player.Camera.CameraStates
{
    public class CameraBlockedState : State
    {
        public CameraBlockedState(StateType stateType)
        {
            StateType = stateType;
        }
        
        public override void Enter()
        {
            Debug.Log("Entered blocked state");
        }

        public override void Update()
        {
            
        }

        public override void Exit()
        {
            
        }
    }
}