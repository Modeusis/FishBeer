using Player.Camera;
using Sounds;
using UnityEngine;
using Utilities.EventBus;
using Zenject;

namespace UI.Pages
{
    public class Page : MonoBehaviour
    {
        public void Open()
        {
            gameObject.SetActive(true);
        }
        
        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}