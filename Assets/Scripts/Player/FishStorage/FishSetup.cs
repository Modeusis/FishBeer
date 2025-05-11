using System.Collections.Generic;
using UnityEngine;

namespace Player.FishStorage
{
    [CreateAssetMenu(menuName = "Setups/Fish Setup")]
    public class FishSetup : ScriptableObject
    {
        [SerializeField] private List<Fish> availableFishes;
        
        public IReadOnlyList<Fish> AvailableFishes => availableFishes;
    }
}