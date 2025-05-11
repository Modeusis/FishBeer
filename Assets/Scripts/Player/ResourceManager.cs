using UI.ResourceCounters;
using UnityEngine;

namespace Player
{
    public class ResourceManager
    {
        private readonly MoneyView _moneyView;
        private readonly FishBaitsView _fishBaitsView;
        private readonly BeerView _beerView;
        
        private int _fishBaits;
        private int _beer;
        private float _money;

        public int FishBaits
        {
            get => _fishBaits;
            private set
            {
                _fishBaits = value;
                
                _fishBaitsView.UpdateFishBaits(_fishBaits);
            }
        }

        public int Beer
        {
            get => _beer;
            private set
            {
                _beer = value;
                
                _beerView.UpdateBeer(_beer);
            }
        }

        public float Money
        {
            get => _money;
            private set
            {
                _money = value;
                
                _moneyView.UpdateMoney(_money);
            }
        }

        public ResourceManager(int fishBaits, int beer, float money,
            MoneyView moneyView, FishBaitsView fishBaitsView, BeerView beerView)
        {
            _moneyView = moneyView;
            _fishBaitsView = fishBaitsView;
            _beerView = beerView;
            
            FishBaits = fishBaits;
            Beer = beer;
            Money = money;
        }
        
        public void AddMoney(float value)
        {
            if (value < 0)
                return;
            
            Money += value;
        }

        public void AddFishBaits(int value)
        {
            if (value < 0)
                return;
            
            FishBaits += value;
        }
        
        public void AddBeer(int value)
        {
            if (value < 0)
                return;
            
            Beer += value;
        }

        public void SpendMoney(float value)
        {
            if (value < 0)
                return;
            
            if (value > Money)
            {
                Debug.LogWarning("You do not have enough money to spend " + value);
                
                return;
            }
            
            Money -= value;
        }

        public void SpendFishBait()
        {
            if (FishBaits <= 0)
                return;
            
            FishBaits--;
        }

        public void DrinkBeer()
        {
            if (Beer <= 0)
                return;
            
            Beer--;
        }
    }
}