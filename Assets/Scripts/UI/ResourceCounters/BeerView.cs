using TMPro;
using UnityEngine;

namespace UI.ResourceCounters
{
    [RequireComponent(typeof(TMP_Text))]
    public class BeerView : MonoBehaviour
    {
        private TMP_Text _fieldText;

        public void UpdateBeer(int beerCount)
        {
            if (_fieldText == null)
            {
                _fieldText = GetComponent<TMP_Text>();
            }
            
            _fieldText.text = $"x{beerCount}";
        }
    }
}