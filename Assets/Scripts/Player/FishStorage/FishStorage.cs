using System;
using System.Collections.Generic;
using UI.ResourceCounters;
using Zenject;

namespace Player.FishStorage
{
    public class FishStorage : IDisposable
    {
        private List<Fish> _fishes;

        public IReadOnlyList<Fish> Fishes => _fishes;
        
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
            _fishes.Remove(_fishes[0]);
            
            _fishCountView.UpdateFishAmount(GetFishesAmount());
        }
        
        public float SellFishes()
        {
            if (GetFishesAmount() == 0)
                return 0f;
            
            float total = 0f;
            
            foreach (var fish in _fishes)
            {
                total += fish.Price;
            }
            
            _fishes.Clear();
            
            return total;
        }

        public void Dispose()
        {
            
        }
    }
}