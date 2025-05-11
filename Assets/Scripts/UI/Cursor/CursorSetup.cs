using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Cursor
{
    [CreateAssetMenu(menuName = "Setups/Cursor setup")]
    public class CursorSetup : ScriptableObject
    {
        [field: SerializeField] public List<Cursor> Cursors { get; private set; } 
    }

    [Serializable]
    public class Cursor
    {
        [field: SerializeField] public CursorType CursorType { get; private set; }
        
        [field: SerializeField] public Sprite CursorImage { get; private set; }
    }
}