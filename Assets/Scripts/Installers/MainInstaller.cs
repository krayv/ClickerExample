using Zenject;
using UnityEngine;

public class MainInstaller : MonoInstaller
{
    [SerializeField] private UIController _uiController;
    [SerializeField] private Tooltip _tooltip;

    public override void InstallBindings()
    {
        Container.Bind<UIController>().FromInstance(_uiController).AsSingle().NonLazy();
        Container.Bind<Tooltip>().FromInstance(_tooltip).AsSingle().NonLazy();

        Container.Bind<IResourceLoader>().To<DefaultResourceLoader>().FromNew().AsSingle();
        Container.Bind<IViewFactory>().To<DefaultViewFactory>().AsSingle();
        Container.Bind<BuildingsUIItemFactory>().FromNew().AsTransient();
        Container.Bind<AchievementsUIItemFactory>().FromNew().AsTransient();

        Container.Bind<CookieProductionModel>().FromNew().AsSingle();

        Container.Bind<CookiesModel>().FromNew().AsSingle().NonLazy();
        Container.Bind<CookiesViewModel>().FromNew().AsSingle().NonLazy();

        Container.Bind<BuildingsModel>().FromNew().AsSingle();
        Container.Bind<BuildingsViewModel>().FromNew().AsSingle();

        Container.Bind<AchievementsModel>().FromNew().AsSingle();
        Container.Bind<AchievementsViewModel>().FromNew().AsSingle();
    }
}
