using System.Data;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Bars
{
    public class BaseBar : MonoBehaviour
    {
        [SerializeField] private Material bar;
        [SerializeField, Range(0, 100)] private float startFill = 20f;
        
        private float _fill;

        private float Fill
        {
            get => _fill;
            set
            {
                if (_fill == value)
                    return;
                
                _fill = value / 100f;
                
                bar.DOFloat(_fill, "_Progress", 0.1f);
            }
        }

        private void Start()
        {
            Fill = startFill;
        }

        private void Update()
        {
            UpdateRule();
        }
        
        protected virtual void UpdateRule()
        {
            
        }
    }
}