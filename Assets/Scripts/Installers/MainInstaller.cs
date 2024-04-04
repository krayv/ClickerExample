using Zenject;
using UnityEngine;

public class MainInstaller : MonoInstaller
{
    [SerializeField] private UIController _uiController;
    [SerializeField] private Tooltip _tooltip;
    //[SerializeField] private ClickTextObjectPool _clickTextObjectPool;
    public override void InstallBindings()
    {
        Container.Bind<UIController>().FromInstance(_uiController).AsSingle().NonLazy();
        Container.Bind<Tooltip>().FromInstance(_tooltip).AsSingle().NonLazy();

        Container.Bind<IResourceLoader>().To<DefaultResourceLoader>().FromNew().AsSingle();
        Container.Bind<IGameProgressLoader>().To<DefaultGameProgressLoader>().FromNew().AsSingle();       
        Container.Bind<IViewFactory>().To<DefaultViewFactory>().AsSingle();
        Container.Bind<BuildingsUIItemFactory>().FromNew().AsTransient();
        Container.Bind<AchievementsUIItemFactory>().FromNew().AsTransient();
        Container.Bind<UpgradesUIItemFactory>().FromNew().AsTransient();
     

        Container.Bind<CookiesModel>().FromNew().AsSingle().NonLazy();       
        Container.Bind<BuildingsModel>().FromNew().AsSingle();       
        Container.Bind<AchievementsModel>().FromNew().AsSingle();      
        Container.Bind<UpgradesModel>().FromNew().AsSingle();
        Container.Bind<CookieProductionModel>().FromNew().AsSingle();
        Container.Bind<GameStatisticModel>().FromNew().AsSingle();
        Container.Bind<IGameProgressSaver>().To<DefaultGameProgressSaver>().FromNew().AsSingle();

        Container.Bind<CookiesViewModel>().FromNew().AsSingle().NonLazy();
        Container.Bind<BuildingsViewModel>().FromNew().AsSingle();
        Container.Bind<AchievementsViewModel>().FromNew().AsSingle();
        Container.Bind<UpgradesViewModel>().FromNew().AsSingle();
        Container.Bind<GameStatisticModelView>().FromNew().AsSingle();
       
        Container.Bind<IInitializable>().To<SOItemInjector>().FromNew().AsSingle();
        Container.Bind<IInitializable>().To<GameInitializer>().FromNew().AsSingle();

        //Container.Bind<ClickTextObjectPool>().FromInstance(_clickTextObjectPool).AsSingle();
    }
}
