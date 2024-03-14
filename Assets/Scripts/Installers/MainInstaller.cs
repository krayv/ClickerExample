using Zenject;
using UnityEngine;

public class MainInstaller : MonoInstaller
{
    [SerializeField] private UIController _uiController;

    public override void InstallBindings()
    {
        Container.Bind<UIController>().FromInstance(_uiController).AsSingle().NonLazy();
        Container.Bind<IResourceLoader>().To<DefaultResourceLoader>().FromNew().AsSingle();
        Container.Bind<IViewFactory>().To<DefaultViewFactory>().AsSingle();
        Container.Bind<BuildingsUIItemFactory>().FromNew().AsSingle();

        Container.Bind<CookiesModel>().FromNew().AsSingle().NonLazy();
        Container.Bind<CookiesViewModel>().FromNew().AsSingle().NonLazy();

        Container.Bind<BuildingsModel>().FromNew().AsSingle();
        Container.Bind<BuildingsViewModel>().FromNew().AsSingle();       
    }
}
