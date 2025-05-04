using UnityEngine;

namespace Player
{
    public class ResourceManager
    {
        public int FishBaits { get; private set; }
        
        public int Beer { get; private set; }
        
        public float Money { get; private set; }

        public ResourceManager(int fishBaits, int beer, float money)
        {
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