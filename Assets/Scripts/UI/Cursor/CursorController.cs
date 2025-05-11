using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Utilities.EventBus;
using Zenject;

namespace UI.Cursor
{
    public class CursorController : MonoBehaviour
    {
        private EventBus _eventBus;
        
        [SerializeField] private CursorSetup cursorSetup;
        [SerializeField] private CursorType currentCursor;

        private Image cursorImage;
        
        private IReadOnlyList<Cursor> _cursors => cursorSetup.Cursors;
        
        [Inject]
        private void Initialize(EventBus eventBus)
        {
            UnityEngine.Cursor.visible = false;
            
            cursorImage = GetComponent<Image>();
            
            _eventBus = eventBus;
            _eventBus.Subscribe<CursorType>(ChangeCursor);
            
            ChangeCursor(currentCursor);
        }

        private void ChangeCursor(CursorType cursorType)
        {
            if (currentCursor == cursorType)
                return;

            var cursor = _cursors.FirstOrDefault(cursor => cursor.CursorType == cursorType);

            if (cursor == null)
            {
                Debug.LogWarning("Cursor wasn't found in setup");
                
                return;
            }

            currentCursor = cursorType;
            cursorImage.sprite = cursor.CursorImage;
        }

        void Update()
        {
            cursorImage.rectTransform.position = Mouse.current.position.ReadValue();
        }
    }
}