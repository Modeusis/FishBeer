using DG.Tweening;
using GameProcess.MiniGame.StateUiScreens;
using Player.FishStorage;
using Sounds;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities.EventBus;
using Utilities.FSM;

namespace GameProcess.MiniGame.MiniGameStates
{
    public class FinishFishingState : State
    {
        private readonly EventBus _eventBus;
        
        private readonly FishingFinishScreen _fishingFinishScreen;
        
        private readonly SoundService _soundService;
        
        private readonly FishingRodAnimationHandler _fishingRodAnimation;
        
        private Fish _pulledFish;
        
        private GameObject _fishViewInstance;
        
        public FinishFishingState(StateType stateType, EventBus eventBus, SoundService soundService, FishingFinishScreen finishScreen)
        {
            StateType = stateType;
            
            _eventBus = eventBus;
            _eventBus.Subscribe<Fish>(HandleSuccessFishing);

            _soundService = soundService;
            
            _fishingFinishScreen = finishScreen;
            
            _fishingFinishScreen.LeaveButton.onClick.AddListener(LeaveFishing);
            _fishingFinishScreen.RestartMiniGameButton.onClick.AddListener(ContinueFishing);
            
            
        }
        
        public override void Enter()
        {
            ToggleFinishScreen(true);
            
            _soundService.Play2DSfx(SoundType.Glorious, 1f);
            
            RotateFishView();
        }

        public override void Update()
        {
            
        }

        public override void Exit()
        {
            ToggleFinishScreen(false);
            
            _fishingFinishScreen.FishViewTransform.DOKill();
        }

        private void LeaveFishing()
        {
            _eventBus.Publish(MiniGameStep.Idle);
        }
        
        private void ContinueFishing()
        {
            _eventBus.Publish(MiniGameStep.Toggled);
        }

        private void HandleSuccessFishing(Fish fish)
        {
            if (_fishViewInstance != null)
            {
                _fishViewInstance.transform.DOKill();
                
                Object.Destroy(_fishViewInstance);
                
                _fishViewInstance = null;
            }
            
            _pulledFish = fish;
            
            _fishingFinishScreen.FishNameTextField.text = fish.Name;
            
            _fishViewInstance = Object.Instantiate(_pulledFish.FishPrefab);
            
            _fishViewInstance.transform.position = _fishingFinishScreen.FishViewTransform.position;
            _fishViewInstance.transform.rotation = _fishingFinishScreen.FishViewTransform.rotation;
            
            _fishingFinishScreen.FishViewTransform.gameObject.layer = LayerMask.NameToLayer("Overlay");
        }

        private void ToggleFinishScreen(bool isActive)
        {
            _fishingFinishScreen.FinishFishingScreen.DOKill();
            
            _fishingFinishScreen.FinishFishingScreen.interactable = isActive;
            
            _fishingFinishScreen.FinishFishingScreen.DOFade(isActive ? 1f : 0f, _fishingFinishScreen.ToggleDuration);
        }

        private void RotateFishView()
        {
            if (_fishViewInstance == null)
                return;
            
            Debug.Log("Test");
            
            _fishViewInstance.transform.DOKill();
            
            _fishViewInstance.transform.DOLocalRotate(new Vector3(0, 360f, 45f), _fishingFinishScreen.SpinDuration,
                    RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart);
        }
    }
}