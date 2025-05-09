using DG.Tweening;
using Player.FishStorage;
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
        
        private readonly CanvasGroup _finishFishingScreen;
        private readonly float _toggleDuration;
        
        private readonly Transform _fishViewTransform;
        
        private readonly TMP_Text _fishNameTextField;
        
        private readonly Button _leaveButton;
        private readonly Button _restartMiniGameButton;
        
        private Fish _pulledFish;
        
        private GameObject _fishViewInstance;
        
        public FinishFishingState(StateType stateType, EventBus eventBus, TMP_Text fishName, Button leaveButton,
            Button restartMiniGameButton, CanvasGroup fishViewCanvasGroup, float toggleDuration, Transform fishViewTransform)
        {
            StateType = stateType;
            
            _eventBus = eventBus;
            _eventBus.Subscribe<Fish>(HandleSuccessFishing);
            
            _finishFishingScreen = fishViewCanvasGroup;
            _toggleDuration = toggleDuration;
            
            _fishViewTransform = fishViewTransform;
            
            _fishNameTextField = fishName;
            
            _leaveButton = leaveButton;
            _restartMiniGameButton = restartMiniGameButton;
            
            _leaveButton.onClick.AddListener(LeaveFishing);
            _restartMiniGameButton.onClick.AddListener(ContinueFishing);
        }
        
        public override void Enter()
        {
            ToggleFinishScreen(true);
        }

        public override void Update()
        {
            
        }

        public override void Exit()
        {
            ToggleFinishScreen(false);
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
                Object.Destroy(_fishViewInstance);
                
                _fishViewInstance = null;
            }
            
            _pulledFish = fish;
            
            _fishNameTextField.text = fish.Name;
            
            _fishViewInstance = Object.Instantiate(_pulledFish.FishPrefab);
            
            _fishViewInstance.transform.position = _fishViewTransform.position;
            _fishViewInstance.transform.rotation = _fishViewTransform.rotation;
            
            _fishViewTransform.gameObject.layer = LayerMask.NameToLayer("Overlay");
        }

        private void ToggleFinishScreen(bool isActive)
        {
            _finishFishingScreen.DOKill();
            
            _finishFishingScreen.interactable = isActive;
            
            _finishFishingScreen.DOFade(isActive ? 1f : 0f, _toggleDuration);
        }

        private void RotateFishView()
        {
            if (_fishViewInstance == null)
                return;
            
            var rotateVector = _fishViewTransform.rotation.eulerAngles;
            
            _fishViewInstance.transform.DOKill();
            
            _fishViewInstance.transform.DOLocalRotate(new Vector3(0f, _fishViewTransform.localEulerAngles.y, 0f), _toggleDuration);
        }
    }
}