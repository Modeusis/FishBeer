using Player.Camera;
using Sounds;
using UnityEngine;
using Utilities.EventBus;
using Zenject;

namespace UI.Pages
{
    public class Page : MonoBehaviour
    {
        protected void Open()
        {
            gameObject.SetActive(true);
        }
        
        protected void Close()
        {
            gameObject.SetActive(false);
        }
    }
}