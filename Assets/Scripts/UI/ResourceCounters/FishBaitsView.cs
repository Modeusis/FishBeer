using TMPro;
using UnityEngine;

namespace UI.ResourceCounters
{
    [RequireComponent(typeof(TMP_Text))]
    public class FishBaitsView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _fieldText;

        public void UpdateFishBaits(int baits)
        {
            if (_fieldText == null)
            {
                _fieldText = GetComponent<TMP_Text>();
            }
            
            _fieldText.text = $"x{baits}";
        }
    }
}