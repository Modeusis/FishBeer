namespace UI.Pages.FishSaleTable
{
    public class FishTotal
    {
        private int _currentQuantity;
        
        private float _oneFishPrice;
        public string FishName { get; }

        public string FishQuantity => _currentQuantity.ToString();

        public string FishPrice => (_oneFishPrice * _currentQuantity).ToString();
        
        public FishTotal(string fishName, float fishPrice)
        {
            FishName = fishName;
            
            _currentQuantity = 1;
            
            _oneFishPrice = fishPrice;
        }

        public void AddFish()
        {
            _currentQuantity++;
        }
    }
}