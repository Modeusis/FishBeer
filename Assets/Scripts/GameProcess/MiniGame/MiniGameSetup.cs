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
        [field: SerializeField] public MiniGameTypeDifficulties Difficulty { get; private set; }
        
        [field: SerializeField, Range(0, 1f)] public float MiniGameChance { get; private set; }

        [field: SerializeField] public float GameDuration { get; private set; } = 4f;
        [field: SerializeField] public Vector2 PercentageBounds { get; private set; } = new Vector2(0.3f, 0.6f);
    }
}