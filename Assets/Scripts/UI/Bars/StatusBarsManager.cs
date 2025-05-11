using System;
using UI.Pages;
using UnityEngine;
using UnityEngine.Serialization;
using Utilities.EventBus;
using Zenject;

namespace UI.Bars
{
    public class StatusBarsManager : MonoBehaviour
    {
        private EventBus _eventBus;
        
        [Header("Lose screen")]
        [SerializeField] private LoseScreenPage loseScreen;
        
        [Header("Bars")]
        [SerializeField] private BaseBar hpBar;
        [SerializeField] private BaseBar drunkBar;
        [SerializeField] private BaseBar moodBar;

        [Header("HpBar Settings")] 
        [SerializeField] private float onFishEatenIncreaseValue = 20f;
        [SerializeField] private float decreaseHpBarValue = 0.5f;
        [SerializeField] private float fastHpBarDecreaseValue = 1.5f;
        [SerializeField, Range(0, 100f)] private float drunkHpBarPercentageLimit = 80f;
        
        [Header("DrunkBar Settings")] 
        [SerializeField] private float decreaseDrunkBarValue = 1f;
        [SerializeField] private float onBeerToggleDrunkBarValue = 15f;
        
        [Header("MoodBar Settings")] 
        [SerializeField] private float onBeerToggleMoodBarValue = 20f;
        [SerializeField] private float onFishCatchIncreaseValue = 10f;
        [SerializeField] private float decreaseMoodBarValue = 0.5f;
        [SerializeField] private float fastMoodBarDecreaseValue = 1.5f;
        [SerializeField] private float onFishLoseDecreaseValue = 15f;
        [SerializeField, Range(0, 100f)] private float drunkMoodBarPercentageLimit = 10f;

        private bool _isLoseScreenShowed;
        
        private float _currentDrunkBarDecreaseValue;
        
        private float _currentHpBarDecreaseValue;

        private float CurrentHpBarDecreaseValue
        {
            get => _currentHpBarDecreaseValue;
            set
            {
                if (_currentHpBarDecreaseValue == value)
                    return;
                
                _currentHpBarDecreaseValue = value;
                
                Debug.Log($"hp decrease speed changed {_currentHpBarDecreaseValue}");
            }
        }
        
        private float _currentMoodBarDecreaseValue;
        
        private float CurrentMoodBarDecreaseValue
        {
            get => _currentMoodBarDecreaseValue;
            set
            {
                if (_currentMoodBarDecreaseValue == value)
                    return;
                
                _currentMoodBarDecreaseValue = value;
                
                Debug.Log($"mood decrease speed changed {_currentMoodBarDecreaseValue}");
            }
        }
        
        [Inject]
        public void Initialize(EventBus eventBus)
        {
            _eventBus = eventBus;
            _eventBus.Subscribe<PlayerActionType>(HandlePlayerAction);
            
            _currentDrunkBarDecreaseValue = decreaseDrunkBarValue;
        }

        private void Update()
        {
            if (_isLoseScreenShowed)
                return;
            
            if (hpBar.Fill <= 0 || moodBar.Fill <= 0)
            {
                ShowLooseScreen();
            }
            
            CurrentHpBarDecreaseValue = drunkBar.Fill > drunkHpBarPercentageLimit ? fastHpBarDecreaseValue : decreaseHpBarValue;
            CurrentMoodBarDecreaseValue = drunkBar.Fill < drunkMoodBarPercentageLimit ? fastMoodBarDecreaseValue : decreaseMoodBarValue;
            
            hpBar.UpdateBar(_currentHpBarDecreaseValue);
            drunkBar.UpdateBar(_currentDrunkBarDecreaseValue);
            moodBar.UpdateBar(_currentMoodBarDecreaseValue);
        }

        private void HandlePlayerAction(PlayerActionType playerActionType)
        {
            switch (playerActionType)
            {
                case PlayerActionType.BeerDrink:
                {
                    OnBeerToggle();
                    
                    break;
                }
                case PlayerActionType.FishEat:
                {
                    OnFishEat();
                    
                    break;
                }
                case PlayerActionType.FishCatchFailure:
                {
                    OnFishLose();
                    
                    break;
                }
                case PlayerActionType.FishCatchSuccess:
                {
                    OnFishCatch();
                    
                    break;
                }
                default:
                {
                    Debug.LogWarning("No handler for this player action type");
                    
                    return;
                }
            }
        }
        
        private void OnBeerToggle()
        {
            drunkBar.OneTimeIncrease(onBeerToggleDrunkBarValue);
            moodBar.OneTimeIncrease(onBeerToggleMoodBarValue);
        }
        
        private void OnFishCatch()
        {
            moodBar.OneTimeIncrease(onFishCatchIncreaseValue);
        }
        
        private void OnFishLose()
        {
            moodBar.OneTimeIncrease(onFishLoseDecreaseValue);
        }
        
        private void OnFishEat()
        {
            hpBar.OneTimeIncrease(onFishEatenIncreaseValue);
        }
        
        private void ShowLooseScreen()
        {
            _isLoseScreenShowed = true;
            
            Time.timeScale = 0f;
            
            loseScreen.Show();
        }
    }
}