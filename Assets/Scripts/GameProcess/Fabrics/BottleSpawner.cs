using Animations;
using GameProcess.Interactions;
using Player;
using Sounds;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace GameProcess.Fabrics
{
    public class BottleSpawner : MonoBehaviour
    {
        [Inject] private SoundService _soundService;
        [Inject] private ResourceManager _resourceManager;
        
        [SerializeField] private DrinkableBeerBottle drinkableBottleInteractor;
        
        [SerializeField] private DrinkableBottleAnimationHandle bottlePrefab;
        
        [SerializeField] private Transform bottleTransform;

        private DrinkableBottleAnimationHandle _bottleInstance;

        private void Update()
        {
            if (!ValidateSpawnBottle())
                return;

            SpawnBottle();
        }

        private void SpawnBottle()
        {
            _bottleInstance = Instantiate(bottlePrefab, bottleTransform.position, bottleTransform.rotation);
            _bottleInstance.onDrink.AddListener(drinkableBottleInteractor.FinishDrinking);
            _bottleInstance.SetupDrinkableBottle(_soundService);
            
            drinkableBottleInteractor.animationHandle = _bottleInstance;
            drinkableBottleInteractor.OnSpawnBottle();
        }

        private bool ValidateSpawnBottle()
        {
            if (_bottleInstance != null)
                return false;

            if (_resourceManager.Beer == 0)
                return false;
            
            return true;
        }
    }
}