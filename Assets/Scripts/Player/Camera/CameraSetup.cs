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
        
        [field: SerializeField] public List<AvailableCameraInputs> AvailablePositionsChanges { get; private set; }

        public void Activate()
        {
            Camera.gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            Camera.gameObject.SetActive(false);
        }
    }

    [Serializable]
    public class AvailableCameraInputs
    {
        [field: SerializeField] public CameraPosition PositionType { get; private set; } 
        
        [field: SerializeField] public Vector2 CameraVector { get; private set; } 
    }
}