using System;
using GameProcess.MiniGame;
using UnityEngine;

namespace Player.FishStorage
{
    [Serializable]
    public class Fish
    {
        [field: SerializeField] public string Name { get; private set;}
        
        [field: SerializeField] public float Price { get; private set;}
        
        [field: SerializeField] public GameObject FishPrefab { get; private set;}
        
        [field: SerializeField] public MiniGameTypeDifficulties Difficulty { get; private set;}
    }
}