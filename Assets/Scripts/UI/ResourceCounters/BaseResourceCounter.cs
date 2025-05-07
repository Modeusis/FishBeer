using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI.ResourceCounters
{
    public class BaseResourceCounter : MonoBehaviour
    {
        [SerializeField] protected TMP_Text _fieldText;
        [SerializeField] protected float scaleOnChange = 1.1f;
        [SerializeField] protected float duration = 0.4f;
        
        protected Sequence _sequence;
    }
}