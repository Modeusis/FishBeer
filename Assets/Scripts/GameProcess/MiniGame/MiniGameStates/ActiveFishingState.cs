using GameProcess.MiniGame.StateUiScreens;
using Player.FishStorage;
using UnityEngine;
using Utilities.EventBus;
using Utilities.FSM;

namespace GameProcess.MiniGame.MiniGameStates
{
    public class ActiveFishingState : State
    {
        private readonly EventBus _eventBus;
        
        private readonly BaseInput _input;
        
        private readonly MiniGameSetup _miniGameSetup;
        
        private readonly FishSetup _fishSetup;
        
        private readonly FishingRodAnimationHandler _fishingRodAnimation;
        
        private readonly FishingActiveScreen _fishingActiveScreen;
        
        public ActiveFishingState(StateType stateType, EventBus eventBus, BaseInput input, MiniGameSetup miniGameSetup,
            FishingRodAnimationHandler fishingRodAnimationHandler, FishingActiveScreen fishingActiveScreen, FishSetup fishSetup)
        {
            StateType = stateType;
            
            _eventBus = eventBus;
            
            _input = input;
            
            _miniGameSetup = miniGameSetup;
            
            _fishSetup = fishSetup;
            
            _fishingRodAnimation = fishingRodAnimationHandler;
            
            _fishingActiveScreen = fishingActiveScreen;
        }
        
        public override void Enter()
        {
            Debug.Log("ActiveFishingState Enter");
            
            _fishingRodAnimation.Throw();
        }

        public override void Update()
        {
            if (_input.gameplay.Interact.WasPressedThisFrame())
            {
                _eventBus.Publish(_fishSetup.AvailableFishes[0]);
                
                // _fishingRodAnimation.Catch();
            }
        }

        public override void Exit()
        {
            
        }

        private void SetDifficulty()
        {
            
        }
        
        public void ShowMiniGameUi()
        {
            
        }
    }
}