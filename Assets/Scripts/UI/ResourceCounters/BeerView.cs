using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI.ResourceCounters
{
    [RequireComponent(typeof(TMP_Text))]
    public class BeerView : BaseResourceCounter
    {
        public void UpdateBeer(int beerCount)
        {
            if (_fieldText == null)
            {
                _fieldText = GetComponent<TMP_Text>();
            }
            
            _fieldText.text = $"x{beerCount}";
            
            _sequence?.Kill();
            
            _sequence = DOTween.Sequence();
            
            _sequence.Append(transform.DOScale(scaleOnChange, duration / 2));
            _sequence.Append(transform.DOScale(1, duration / 2));
        }
    }
}