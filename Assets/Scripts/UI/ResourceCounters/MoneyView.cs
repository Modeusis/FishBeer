using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI.ResourceCounters
{
    [RequireComponent(typeof(TMP_Text))]
    public class MoneyView : BaseResourceCounter
    {
        public void UpdateMoney(float money)
        {
            if (_fieldText == null)
            {
                _fieldText = GetComponent<TMP_Text>();
            }
            
            _fieldText.text = $"{money}$";
            
            _sequence?.Kill();
            
            _sequence = DOTween.Sequence();
            
            _sequence.Append(transform.DOScale(scaleOnChange, duration / 2));
            _sequence.Append(transform.DOScale(1, duration / 2));
        }
    }
}