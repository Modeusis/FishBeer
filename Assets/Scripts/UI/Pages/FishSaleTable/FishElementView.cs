using TMPro;
using UnityEngine;

namespace UI.Pages.FishSaleTable
{
    public class FishElementView : MonoBehaviour
    {
        [SerializeField] private TMP_Text fishNameText;
        [SerializeField] private TMP_Text fishQuantityText;
        [SerializeField] private TMP_Text totalPriceText;

        public void SetFishNameText(string text)
        {
            fishNameText.text = text;
        }

        public void SetFishQuantityText(string text)
        {
            fishQuantityText.text = text;
        }

        public void SetTotalPriceText(string text)
        {
            totalPriceText.text = text;
        }
    }
}