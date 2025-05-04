using System.Collections.Generic;
using Zenject;

namespace Player.FishStorage
{
    public class FishStorage
    {
        [Inject] private ResourceManager _resourceManager;
        
        private List<Fish> _fishes;

        private int GetFishAmount() => _fishes.Count;
        
        private void AddFish(Fish fish)
        {
            _fishes.Add(fish);
        }

        private void EatFish()
        {
            _fishes.Remove(_fishes[0]);
        }
        
        private void SellFishes()
        {
            if (GetFishAmount() == 0)
                return;
            
            float total = 0f;
            
            foreach (var fish in _fishes)
            {
                total += fish.Price;
            }
            
            _resourceManager.AddMoney(total);
        }
    }
}