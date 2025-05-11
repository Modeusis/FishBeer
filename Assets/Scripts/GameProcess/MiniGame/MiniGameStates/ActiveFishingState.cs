using System.Linq;
using System.Resources;
using DG.Tweening;
using GameProcess.MiniGame.StateUiScreens;
using Player.FishStorage;
using UI.Bars;
using UnityEngine;
using Utilities.EventBus;
using Utilities.FSM;

namespace GameProcess.MiniGame.MiniGameStates
{
    public class ActiveFishingState : State
    {
        private readonly EventBus _eventBus;
        
        private readonly BaseInput _input;
        
        private readonly FishStorage _fishStorage;
        
        private readonly MiniGameSetup _miniGameSetup;
        private readonly FishSetup _fishSetup;
        
        private readonly FishingRodAnimationHandler _fishingRodAnimation;
        private readonly FishingLineHandler _fishingLine;
        
        private readonly FishingActiveScreen _fishingActiveScreen;
        
        private MiniGame _currentMiniGame;
        
        private Vector2 _catchZonePositionBounds;
        private float _catchZoneWidth;

        private float _tempTimer;
        
        public ActiveFishingState(StateType stateType, EventBus eventBus, BaseInput input, MiniGameSetup miniGameSetup,
            FishingRodAnimationHandler fishingRodAnimationHandler, FishingLineHandler fishingLineHandler,
            FishingActiveScreen fishingActiveScreen, FishSetup fishSetup, FishStorage fishStorage)
        {
            StateType = stateType;
            
            _eventBus = eventBus;
            
            _input = input;
            
            _fishStorage = fishStorage;
            
            _miniGameSetup = miniGameSetup;
            
            _fishSetup = fishSetup;
            
            _fishingRodAnimation = fishingRodAnimationHandler;
            
            _fishingLine = fishingLineHandler;
            
            _fishingActiveScreen = fishingActiveScreen;
        }
        
        public override void Enter()
        {
            _tempTimer = 0f;
            
            _fishingActiveScreen.ActiveScreen.DOKill();
            
            _fishingActiveScreen.ActiveScreen.DOFade(1, 0.5f);
            
            _fishingRodAnimation.Throw();
            
            SetMiniGame();
            SetupMiniGameUi();
            
            _fishingActiveScreen.UpdateProgress(0);
        }

        public override void Update()
        {
            _fishingLine.RenderFishingLine();
            
            _tempTimer += Time.deltaTime;
            
            _fishingActiveScreen.UpdateProgress(Mathf.Lerp(0f, 1f, _tempTimer / _currentMiniGame.GameDuration));

            if (_tempTimer >= _currentMiniGame.GameDuration)
            {
                MiniGameLoose();
            }

            _fishingActiveScreen.ToggleTooltip(_fishingActiveScreen.CallCatch());

            if (_input.gameplay.Interact.WasPressedThisFrame())
            {
                if (!_fishingActiveScreen.CallCatch())
                {
                    MiniGameLoose();
                    
                    return;
                }
                
                MiniGameWin();
                
                _fishingRodAnimation.Catch();
            }
        }

        public override void Exit()
        {
            _fishingActiveScreen.ActiveScreen.DOKill();
            
            _fishingActiveScreen.ActiveScreen.DOFade(0, 0.5f);
        }

        private void SetMiniGame()
        {
            foreach (var miniGame in _miniGameSetup.MiniGames)
            {
                var chance = Random.Range(0f, 1f);

                if (chance < miniGame.MiniGameChance)
                {
                    _currentMiniGame = miniGame;
                    
                    return;
                }
            }

            _currentMiniGame = _miniGameSetup.MiniGames
                .FirstOrDefault(miniGame => miniGame.Difficulty == MiniGameTypeDifficulties.Easy);
        }
        
        private void SetupMiniGameUi()
        {
            _fishingActiveScreen.SetRandomCatchZone(_currentMiniGame.PercentageBounds);
        }

        private void MiniGameWin()
        {
            var possibleFishes =
                _fishSetup.AvailableFishes.Where(fish => fish.Difficulty == _currentMiniGame.Difficulty).ToList();
            
            var randomFishId = Random.Range(0, possibleFishes.Count);

            if (possibleFishes.Count == 0)
            {
                Debug.Log("No such difficulty class fishes, get default fish");
                
                _eventBus.Publish(_fishSetup.AvailableFishes[0]);
                _eventBus.Publish(PlayerActionType.FishCatchSuccess);
                
                _fishStorage.AddFish(_fishSetup.AvailableFishes[0]);
                
                return;
            }
            
            _eventBus.Publish(possibleFishes[randomFishId]);
            _eventBus.Publish(PlayerActionType.FishCatchSuccess);
            
            _fishStorage.AddFish(possibleFishes[randomFishId]);
        }
        
        private void MiniGameLoose()
        {
            _eventBus.Publish(MiniGameStep.Toggled);
            _eventBus.Publish(PlayerActionType.FishCatchFailure);
        }
    }
}