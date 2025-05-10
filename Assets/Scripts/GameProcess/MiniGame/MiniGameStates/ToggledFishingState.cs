using DG.Tweening;
using GameProcess.MiniGame.StateUiScreens;
using Player;
using Player.Camera;
using Sounds;
using UnityEngine;
using Utilities.EventBus;
using Utilities.FSM;

namespace GameProcess.MiniGame.MiniGameStates
{
    public class ToggledFishingState : State
    {
        private readonly EventBus _eventBus;
        
        private readonly BaseInput _input;
        
        private readonly ResourceManager _resourceManager;
        
        private readonly ToggledFishingScreen _toggledFishingScreen;
        
        private readonly SoundService _soundService;
        
        private readonly FishingRodAnimationHandler _fishingRodAnimation;
        
        public ToggledFishingState(StateType stateType, EventBus eventBus, BaseInput input,
            ToggledFishingScreen toggleFishingScreen , FishingRodAnimationHandler fishingRodAnimation, ResourceManager resourceManager)
        {
            StateType = stateType;
            
            _input = input;
            
            _eventBus = eventBus;
            
            _resourceManager = resourceManager;
            
            _toggledFishingScreen = toggleFishingScreen;
            _toggledFishingScreen.ScreenCanvasGroup.interactable = false;
            
            _fishingRodAnimation = fishingRodAnimation;
        }
        
        public override void Enter()
        {
            _toggledFishingScreen.ScreenCanvasGroup.DOKill();
            _toggledFishingScreen.ScreenCanvasGroup.DOFade(1f, _toggledFishingScreen.UiTransitionDuration);
            
            _eventBus.Publish(CameraPosition.Fishing);
            
            _fishingRodAnimation.Toggle();
        }

        public override void Update()
        {
            if (_input.gameplay.Interact.WasPressedThisFrame() && _resourceManager.FishBaits > 0)
            {
                _resourceManager.SpendFishBait();
                
                _eventBus.Publish(MiniGameStep.Active);
            }
        }

        public override void Exit()
        {
            _toggledFishingScreen.ScreenCanvasGroup.DOKill();
            
            _toggledFishingScreen.ScreenCanvasGroup.DOFade(0f, _toggledFishingScreen.UiTransitionDuration);
        }
    }
}