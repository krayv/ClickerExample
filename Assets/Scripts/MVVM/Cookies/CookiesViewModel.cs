using UniRx;
using System.Numerics;
using Zenject;
using System;
public class CookiesViewModel
{
    public ReactiveProperty<BigInteger> Cookies => _cookiesModel.Cookies;
    public ReactiveProperty<BigInteger> CookiesPerSecond => _cookieProductionModel.CookiesPerSecond;

    private readonly CompositeDisposable _disposable = new();
    private CookiesModel _cookiesModel;
    private UIController _uiController;
    private CookieProductionModel _cookieProductionModel;

    private double _previuousCookiesLeft = 0f;

    [Inject]
    private void Construct(CookiesModel cookiesModel, UIController uiController, CookieProductionModel cookieProduction)
    {
        _cookiesModel = cookiesModel;
        _uiController = uiController;
        _cookieProductionModel = cookieProduction;
    }
    
    public void ClickCookie()
    {
        _cookiesModel.Cookies.Value++;
    }

    public void SwitchBuildingsView()
    {
        _uiController.SwitchView<BuildingsView>();
    }

    public void SwitchAchievementsView()
    {
        _uiController.SwitchView<AchievementsView>();
    }

    public void SwitchUpgradesButton()
    {
        _uiController.SwitchView<UpgradesView>();
    }

    public void ProduceCookies(float timeFactor)
    {
        double income = (double)CookiesPerSecond.Value * timeFactor + _previuousCookiesLeft;
        _previuousCookiesLeft = income % 1d;
        _cookiesModel.Cookies.Value += (BigInteger)(income);
    }
}
