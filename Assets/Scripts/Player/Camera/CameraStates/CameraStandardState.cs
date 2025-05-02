using System.Collections.Generic;
using System.Numerics;
using Utilities.EventBus;
using Utilities.FSM;

namespace Player.Camera.CameraStates
{
    public class CameraStandardState : State
    {
        private readonly EventBus _eventBus;
        
        private readonly BaseInput _input;
        
        private readonly List<CameraSetup> _cameras;
        
        private CameraSetup _currentCamera;
        
        public CameraStandardState(StateType stateType, List<CameraSetup> availableCameras, CameraSetup currentCamera, EventBus eventBus, BaseInput input)
        {
            StateType = stateType;
            
            _input = input;            
            
            _eventBus = eventBus;
            _eventBus.Subscribe<CameraPosition>(HandleCameraChange);
            
            _cameras = availableCameras;
            
            _currentCamera = currentCamera;
            
            
        }

        public override void Enter()
        {
            
        }
        
        public override void Update()
        {
            if (_input.gameplay.Move.WasPerformedThisFrame())
            {
                HandleMove(_input.gameplay.Move.ReadValue<Vector2>());
            }
        }
        
        public override void Exit()
        {
            
        }

        private void HandleMove(Vector2 input)
        {
            switch (input.X, input.Y)
            {
                case (0, 1):
                    _eventBus.Publish(CameraPosition.Forward);
                    break;
                case (0, -1):
                    _eventBus.Publish(CameraPosition.Down);
                    break;
                case (1, 0):
                    _eventBus.Publish(CameraPosition.Left);
                    break;
                case (-1, 0):
                    _eventBus.Publish(CameraPosition.Neutral);
                    break;
            }
        }
        
        private void HandleCameraChange(CameraPosition position)
        {
            if (!_currentCamera.AvailablePositionsChanges.Contains(position))
            {
                return;
            }

            var newCamera = _cameras.Find(camera => camera.CameraPosition == position);
            
            if (newCamera == null)
            {
                return;   
            }
            
            _currentCamera = newCamera;
        }
    }
}