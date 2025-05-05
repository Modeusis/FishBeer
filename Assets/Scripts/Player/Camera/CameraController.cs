using System;
using System.Collections.Generic;
using System.Linq;
using GameProcess.Interactions;
using GameProcess.MiniGame;
using Player.Camera.CameraStates;
using Player.FishStorage;
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
        
        [SerializeField] private MiniGameSetup miniGameVariants;
        [SerializeField] private FishSetup availableFishes;
        
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
                { StateType.Blocked, new CameraBlockedState(StateType.Blocked) },
                { StateType.MiniGame, new CameraMiniGameState(StateType.MiniGame) }
            };

            var transitions = new List<Transition>()
            {
                new Transition(StateType.Idle, StateType.Blocked, () => _eventBus.WasCalledThisFrame<InteractionType>()),
                new Transition(StateType.MiniGame, StateType.Idle, () => _input.gameplay.Break.WasPerformedThisFrame()),
                new Transition(StateType.Blocked, StateType.Idle, () => _eventBus.WasCalledThisFrame<CameraUnblocker>()),
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