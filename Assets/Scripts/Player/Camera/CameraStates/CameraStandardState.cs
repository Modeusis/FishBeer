using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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
        
        private StateType _lastCalledState;
        
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
            Debug.Log("Enter idle state");
        }
        
        public override void Update()
        {
            if (_input.gameplay.Move.WasPressedThisFrame())
            {
                HandleMove(_input.gameplay.Move.ReadValue<Vector2>());
            }
        }
        
        public override void Exit()
        {
            
        }

        private void HandleMove(Vector2 input)
        {
            Debug.Log("HandleMove input " + input);
            
            for (int i = 0; i < _currentCamera.AvailablePositionsChanges.Count; i++)
            {
                if (input == _currentCamera.AvailablePositionsChanges[i].CameraVector)
                {
                    HandleCameraChange(_currentCamera.AvailablePositionsChanges[i].PositionType);
                    
                    return;
                }
            }
        }
        
        private void HandleCameraChange(CameraPosition position)
        {
            var newCamera = _cameras.Find(camera => camera.CameraPosition == position);
            
            if (newCamera == null)
            {
                return;   
            }
            
            _currentCamera = newCamera;
            
            _currentCamera.Activate();

            var inactiveCameras = _cameras
                .Where(cam => cam.CameraPosition != _currentCamera.CameraPosition);
            
            foreach (var camera in inactiveCameras)
            {
                camera.Deactivate();
            }
        }
    }
}