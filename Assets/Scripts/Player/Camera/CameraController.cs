using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Player.Camera
{
    public class CameraController : MonoBehaviour
    {
        [Inject] private BaseInput _input;
        
        [SerializeField] private List<CameraSetup> cameras;
        
        private CameraSetup _currentCamera;
    }
}