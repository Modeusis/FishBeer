using UnityEngine;
using Utilities.FSM;

namespace Player.Camera.CameraStates
{
    public class CameraMiniGameState : State
    {
        
        public CameraMiniGameState(StateType stateType)
        {
            StateType = stateType;
        }
        
        public override void Enter()
        {
            Debug.Log("Entered minigame state");
        }

        public override void Update()
        {
            
        }

        public override void Exit()
        {
            
        }
    }
}