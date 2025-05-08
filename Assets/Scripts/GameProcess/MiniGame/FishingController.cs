using System;
using System.Collections.Generic;
using GameProcess.Interactions;
using Player.Camera;
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

        private FSM _fishingStateMachine;
        
        private MiniGameStep _currentFishingStep;
        
        [Inject]
        private void Initialize(EventBus eventBus, BaseInput input)
        {
            _input = input;
            
            _eventBus = eventBus;
            _eventBus.Subscribe<MiniGameStep>(HandleMiniGameStep);

            var transitions = new List<Transition>
            {
                new Transition(StateType.Idle, StateType.Toggled, () => _currentFishingStep == MiniGameStep.Toggled),
                new Transition(StateType.Toggled, StateType.Active, () => _currentFishingStep == MiniGameStep.Active),
                new Transition(StateType.Active, StateType.Finished, () => _currentFishingStep == MiniGameStep.Finishing),
                new Transition(StateType.Finished, StateType.Idle, () => _currentFishingStep == MiniGameStep.Idle),
                new Transition(StateType.Finished, StateType.Toggled, () => _currentFishingStep == MiniGameStep.Toggled),
                new Transition(StateType.Any, StateType.Idle, () => _input.gameplay.Break.WasPressedThisFrame())
            };
            
            
        }

        private void Update()
        {
            _fishingStateMachine?.Update();
        }

        private void LateUpdate()
        {
            _fishingStateMachine?.LateUpdate();
        }

        private void HandleCameraUnblocker(CameraUnblocker cameraUnblocker)
        {
            if (_currentFishingStep == MiniGameStep.Idle)
                return;
            
            
        }
        
        private void HandleMiniGameCall(InteractionType interactionType)
        {
            if (interactionType != InteractionType.Fishing)
                return; 
            
            
        }
        
        private void HandleMiniGameStep(MiniGameStep step)
        {
            _currentFishingStep = step;
        }
    }
}