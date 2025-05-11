using System;
using System.Collections.Generic;
using UI.ResourceCounters;
using Zenject;

namespace Player.FishStorage
{
    public class FishStorage : IDisposable
    {
        [Inject] private ResourceManager _resourceManager;
        
        private List<Fish> _fishes;

        private FishCountView _fishCountView;
        
        public FishStorage(FishCountView view)
        {
            _fishes = new List<Fish>();
            
            _fishCountView = view;
        }
        
        public int GetFishesAmount() => _fishes.Count;
        
        public void AddFish(Fish fish)
        {
            _fishes.Add(fish);
            
            _fishCountView.UpdateFishAmount(GetFishesAmount());
        }

        public void EatFish()
        {
            // _fishes.Remove(_fishes[0]);
            
            _fishCountView.UpdateFishAmount(GetFishesAmount());
        }
        
        public void SellFishes()
        {
            if (GetFishesAmount() == 0)
                return;
            
            float total = 0f;
            
            foreach (var fish in _fishes)
            {
                total += fish.Price;
            }
            
            _resourceManager.AddMoney(total);
        }

        public void Dispose()
        {
            
        }
    }
}