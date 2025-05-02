using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

namespace Player.Camera
{
    [Serializable]
    public class CameraSetup
    {
        [field: SerializeField] public CinemachineCamera Camera { get; private set; }
        
        [field: SerializeField] public CameraPosition CameraPosition { get; private set; }
        
        [field: SerializeField] public List<CameraPosition> AvailablePositionsChanges { get; private set; }
    }
}