using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameProcess.MiniGame
{
    [CreateAssetMenu(menuName = "Setups/MiniGame Setup")]
    public class MiniGameSetup : ScriptableObject
    {
        [SerializeField] private List<MiniGame> miniGames;
        
        public IReadOnlyList<MiniGame> MiniGames => miniGames.OrderBy(game => game.MiniGameChance).ToList();
    }

    [Serializable]
    public class MiniGame
    {
        [field: SerializeField] public MiniGameTypeDifficulties MiniGameTypeDifficulties { get; private set; }
        
        [field: SerializeField, Range(0, 1f)] public float MiniGameChance { get; private set; }
    }
}