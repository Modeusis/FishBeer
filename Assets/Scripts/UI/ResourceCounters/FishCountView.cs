using TMPro;
using UnityEngine;

namespace UI.ResourceCounters
{
    [RequireComponent(typeof(TMP_Text))]
    public class FishCountView : MonoBehaviour
    {
        private TMP_Text _fieldText;
        
        public void UpdateFishAmount(int fishCount)
        {
            if (_fieldText == null)
            {
                _fieldText = GetComponent<TMP_Text>();
            }
            
            _fieldText.text = $"x{fishCount}";
        }
    }
}