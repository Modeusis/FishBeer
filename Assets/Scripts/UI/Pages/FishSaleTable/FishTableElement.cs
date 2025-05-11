namespace UI.Pages.FishSaleTable
{
    public class FishTableElement
    {
        private string FishName { get; set; } 
        
        private int FishQuantity { get; set; }
        private int FishRare  { get; set; }
        
        private float TotalPrice { get; set; }

        public FishTableElement(string fishName, int fishQuantity, int fishRare, float totalPrice)
        {
            FishName = fishName;
            
            FishRare = fishRare;
            FishQuantity = fishQuantity;
            
            TotalPrice = totalPrice;
        }
    }
}