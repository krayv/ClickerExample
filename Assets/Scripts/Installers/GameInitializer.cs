using Zenject;

public class GameInitializer : IInitializable
{
    private CookieProductionModel _cookieProductionModel;

    public void Initialize()
    {
        _cookieProductionModel.RecalculateCookieProduction();
    }

    [Inject]
    private void Construct(CookieProductionModel cookieProductionModel)
    {
        _cookieProductionModel = cookieProductionModel;
    }
}
