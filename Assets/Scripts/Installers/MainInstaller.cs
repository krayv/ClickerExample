using Zenject;
using UnityEngine;

public class MainInstaller : MonoInstaller
{
    [SerializeField] private UIController _uiController;

    public override void InstallBindings()
    {
        Container.Bind<UIController>().FromInstance(_uiController).AsSingle().NonLazy();
        Container.Bind<IResourceLoader>().To<ScriptableObjectResourceLoader>().FromNew().AsSingle();
        Container.Bind<IViewFactory>().To<DefaultViewFactory>().AsSingle();

        Container.Bind<CookiesModel>().FromNew().AsSingle().NonLazy();
        Container.Bind<CookiesViewModel>().FromNew().AsSingle().NonLazy();

        Container.Bind<BuildingsModel>().FromNew().AsSingle().NonLazy();
        Container.Bind<BuildingsViewModel>().FromNew().AsSingle().NonLazy();       
    }
}
