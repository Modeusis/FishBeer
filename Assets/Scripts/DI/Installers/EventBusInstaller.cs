using UnityEngine;
using Utilities.EventBus;
using Zenject;

namespace DI.Installers
{
    public class EventBusInstaller : MonoInstaller
    {
        private EventBus _eventBus;

        public override void InstallBindings()
        {
            _eventBus = new EventBus();
            
            Container.Bind<EventBus>().FromInstance(_eventBus).AsSingle().NonLazy();
        }
    }
}