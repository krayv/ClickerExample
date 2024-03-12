using Zenject;
using UnityEngine;

public class MainInstaller : MonoInstaller
{
    [SerializeField] private CookiesView cookieView;

    public override void InstallBindings()
    {
        Container.Bind<CookiesModel>().FromNew().AsSingle().NonLazy();
        Container.Bind<CookiesViewModel>().FromNew().AsSingle().NonLazy();
        Container.Bind<IViewFactory>().To<DefaultViewFactory>().AsSingle();
    }
}
