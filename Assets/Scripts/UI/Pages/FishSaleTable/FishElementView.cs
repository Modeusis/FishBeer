using TMPro;

namespace UI.Pages.FishSaleTable
{
    public class FishElementView
    {
        private TMP_Text fishNameText;
        private TMP_Text fishRareText;
        private TMP_Text fishQuantityText;
        private TMP_Text totalPriceText;

        public void UpdateView(string fishName, string fishQuantity, string fishRare, string fishPrice)
        {
            fishNameText.text = fishName;
            
            fishRareText.text = fishRare;
            
            fishQuantityText.text = fishQuantity;
            
            totalPriceText.text = fishPrice;
        } 
    }
}