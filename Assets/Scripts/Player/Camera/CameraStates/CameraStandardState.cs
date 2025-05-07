using System.Collections.Generic;
using System.Linq;
using GameProcess.Interactions;
using UnityEngine;
using UnityEngine.InputSystem;
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
        
        private IInteractable _lastCalledInteractable;
        
        private UnityEngine.Camera _mainCamera;
        
        public CameraStandardState(StateType stateType, List<CameraSetup> availableCameras, CameraSetup currentCamera, EventBus eventBus, BaseInput input)
        {
            StateType = stateType;
            
            _input = input;            
            
            _eventBus = eventBus;
            _eventBus.Subscribe<CameraPosition>(HandleCameraChange);
            
            _cameras = availableCameras;
            
            _currentCamera = currentCamera;
            
            _mainCamera = UnityEngine.Camera.main;
        }

        public override void Enter()
        {
            
        }
        
        public override void Update()
        {
            if (_input.gameplay.Move.WasPressedThisFrame())
            {
                HandleMove(_input.gameplay.Move.ReadValue<Vector2>());
            }
            
            HandleRaycast();
            HandleInteraction();
        }
        
        public override void Exit()
        {
            _lastCalledInteractable = null;
        }

        private void HandleMove(Vector2 input)
        {
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

        private void HandleRaycast()
        {
            Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            
            if (!Physics.Raycast(ray, out RaycastHit hit))
                return;

            if (!hit.collider.TryGetComponent(out IInteractable interactable))
            {
                _lastCalledInteractable?.Unfocus();
                
                _lastCalledInteractable = null;
                
                return;
            }

            if (_lastCalledInteractable != interactable)
            {
                _lastCalledInteractable?.Unfocus();

                _lastCalledInteractable = null;
            }
            
            if (_lastCalledInteractable != null)
                return;
            
            _lastCalledInteractable = interactable;
                
            _lastCalledInteractable?.Focus();

        }
        
        private void HandleInteraction()
        {
            if (!_input.gameplay.Click.WasPressedThisFrame())
                return;
            
            _lastCalledInteractable?.Interact();
        }
    }
}