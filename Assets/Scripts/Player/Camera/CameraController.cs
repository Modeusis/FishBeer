using System;
using System.Collections.Generic;
using System.Linq;
using Player.Camera.CameraStates;
using UnityEngine;
using Utilities.EventBus;
using Utilities.FSM;
using Zenject;

namespace Player.Camera
{
    public class CameraController : MonoBehaviour
    {
        private BaseInput _input;
        
        private EventBus _eventBus;
        
        [SerializeField] private List<CameraSetup> cameras;
        
        [SerializeField] private CameraPosition startingPosition;
        
        private CameraSetup _currentCamera;

        private FSM _cameraStateMachine;
        
        [Inject]
        private void Initialize(BaseInput input, EventBus eventBus)
        {
            _input = input;
            
            _eventBus = eventBus;
            
            _currentCamera = cameras.FirstOrDefault(cam => cam.CameraPosition == startingPosition);
            
            var states = new Dictionary<StateType, State>()
            {
                { StateType.Idle, new CameraStandardState(StateType.Idle, cameras, _currentCamera, _eventBus, _input)},
                { StateType.Blocked, new CameraBlockedState(StateType.Blocked) }
            };

            var transitions = new List<Transition>()
            {
                new Transition(StateType.Idle, StateType.Blocked, () => _eventBus.WasCalledThisFrame<CameraBlocker>()),
                new Transition(StateType.Blocked, StateType.Idle, () => _input.gameplay.Break.WasPerformedThisFrame()),
            };
            
            _cameraStateMachine = new FSM(states, transitions, StateType.Idle);
        }

        private void Update()
        {
            _cameraStateMachine?.Update();
        }

        private void LateUpdate()
        {
             _cameraStateMachine?.LateUpdate();
        }
    }
}