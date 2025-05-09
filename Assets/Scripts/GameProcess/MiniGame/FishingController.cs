using System.Collections.Generic;
using GameProcess.Interactions;
using GameProcess.MiniGame.MiniGameStates;
using Player.Camera;
using Player.FishStorage;
using UnityEngine;
using Utilities.EventBus;
using Utilities.FSM;
using Zenject;

namespace GameProcess.MiniGame
{
    public class FishingController : MonoBehaviour
    {
        private EventBus _eventBus;
        
        private BaseInput _input;

        [SerializeField] private FishSetup fishSetup;
        
        [SerializeField] private MiniGameSetup miniGameSetup;
        
        [SerializeField] private FishingRodAnimationHandler fishingRod;
        
        private FSM _fishingStateMachine;
        
        private MiniGameStep _currentFishingStep;
        
        private CameraUnblocker _cameraUnblocker;
        
        [Inject]
        private void Initialize(EventBus eventBus, BaseInput input)
        {
            _input = input;
            
            _eventBus = eventBus;
            _eventBus.Subscribe<InteractionType>(HandleMiniGameCall);
            _eventBus.Subscribe<MiniGameStep>(HandleMiniGameStep);
            
            _cameraUnblocker = new CameraUnblocker();
            
            var transitions = new List<Transition>
            {
                new Transition(StateType.Idle, StateType.Toggled, () => _currentFishingStep == MiniGameStep.Toggled),
                new Transition(StateType.Toggled, StateType.Active, () => _currentFishingStep == MiniGameStep.Active),
                new Transition(StateType.Active, StateType.Finished, () => _eventBus.WasCalledThisFrame<Fish>()),
                new Transition(StateType.Finished, StateType.Idle, () => _currentFishingStep == MiniGameStep.Idle),
                new Transition(StateType.Finished, StateType.Toggled, () => _currentFishingStep == MiniGameStep.Toggled),
                new Transition(StateType.Any, StateType.Idle, MiniGameLeave)
            };

            var states = new Dictionary<StateType, State>()
            {
                { StateType.Idle, new IdleFishingState(StateType.Idle) },
                { StateType.Toggled, new ToggledFishingState(StateType.Toggled, _eventBus) },
                { StateType.Active, new ActiveFishingState(StateType.Active) },
                { StateType.Finished, new IdleFishingState(StateType.Finished) },
            };
            
            _fishingStateMachine = new FSM(states, transitions, StateType.Idle);
        }

        private void Update()
        {
            _fishingStateMachine?.Update();
        }

        private void LateUpdate()
        {
            _fishingStateMachine?.LateUpdate();
        }
        
        private void HandleMiniGameCall(InteractionType interactionType)
        {
            if (interactionType != InteractionType.Fishing)
                return; 
            
            _currentFishingStep = MiniGameStep.Toggled;
        }
        
        private void HandleMiniGameStep(MiniGameStep step)
        {
            _currentFishingStep = step;

            if (_currentFishingStep == MiniGameStep.Idle)
            {
                _eventBus.Publish(_cameraUnblocker);
                _eventBus.Publish(CameraPosition.Forward);
            }
        }

        private bool MiniGameLeave()
        {
            if (_currentFishingStep != MiniGameStep.Idle && _input.gameplay.Break.WasPressedThisFrame())
            {
                HandleMiniGameStep(MiniGameStep.Idle);
                
                return true;
            }
            
            return false;
        }
    }
}