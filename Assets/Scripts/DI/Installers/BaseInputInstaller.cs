using Zenject;

namespace DI.Installers
{
    public class BaseInputInstaller : MonoInstaller
    {
        private BaseInput _input;
        
        public override void InstallBindings()
        {
            _input = new BaseInput();
            
            Container.Bind<BaseInput>().FromInstance(_input).AsSingle().NonLazy();
        }
    }
}