using UniRx;
using System.Numerics;
using Zenject;

public class CookiesViewModel
{
    public ReactiveProperty<BigInteger> Cookies => _cookiesModel.Cookies;

    private readonly CompositeDisposable _disposable = new();
    private CookiesModel _cookiesModel;
    private UIController _uiController;

    [Inject]
    private void Construct(CookiesModel cookiesModel, UIController uiController)
    {
        _cookiesModel = cookiesModel;
        _uiController = uiController;
    }
    
    public void ClickCookie()
    {
        _cookiesModel.Cookies.Value++;
    }

    public void OpenBuildingsView()
    {
        _uiController.OpenView<BuildingsView>();
    }
}
