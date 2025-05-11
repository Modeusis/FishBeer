using Animations;
using GameProcess.Interactions;
using Player;
using Player.FishStorage;
using Sounds;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace GameProcess.Fabrics
{
    public class FishSpawner : MonoBehaviour
    {
        [Inject] private FishStorage _fishStorage;
        [Inject] private SoundService _soundService;
        
        [SerializeField] private EatableFish eatableFishInteractor;
        
        [SerializeField] private EatableFishAnimationHandler fishPrefab;
        
        [SerializeField] private Transform fishTransform;

        private EatableFishAnimationHandler _fishInstance;

        private void Update()
        {
            if (!ValidateSpawnFish())
                return;

            SpawnFish();
        }

        private void SpawnFish()
        {
            _fishInstance = Instantiate(fishPrefab, fishTransform.position, fishTransform.rotation);
            _fishInstance.onFinishEating.AddListener(eatableFishInteractor.HandleFinishEating);
            _fishInstance.SetupEatableFish(_soundService);
            
            eatableFishInteractor.animationHandler = _fishInstance;
        }

        private bool ValidateSpawnFish()
        {
            if (_fishInstance != null)
                return false;

            if (_fishStorage.GetFishesAmount() == 0)
                return false;
            
            return true;
        }
    }
}