using UniRx;
using System.Numerics;
using Zenject;
using System;
using System.Linq;
public class CookiesViewModel
{
    public ReactiveProperty<BigInteger> Cookies => _cookiesModel.Cookies;
    public ReactiveProperty<BigInteger> CookiesPerSecond => _cookieProductionModel.CookiesPerSecond;

    private readonly CompositeDisposable _disposable = new();
    private CookiesModel _cookiesModel;
    private UIController _uiController;
    private CookieProductionModel _cookieProductionModel;
    private UpgradesModel _upgradesModel;

    private double _previousCookiesLeft = 0f;

    [Inject]
    private void Construct(CookiesModel cookiesModel, UIController uiController, CookieProductionModel cookieProduction, UpgradesModel upgradesModel)
    {
        _cookiesModel = cookiesModel;
        _uiController = uiController;
        _cookieProductionModel = cookieProduction;
        _upgradesModel = upgradesModel;
    }
    
    public void ClickCookie()
    {
        BigInteger value = 1;
        foreach (var cookieUpgrade in _upgradesModel.Upgrades.Where(u => u.Value && u.Key is ClickUpgrade))
        {
            value = cookieUpgrade.Key.CalculateProduction(value);
        }
        _cookiesModel.Cookies.Value += value;
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
        double income = (double)CookiesPerSecond.Value * timeFactor + _previousCookiesLeft;
        _previousCookiesLeft = income % 1d;
        _cookiesModel.Cookies.Value += (BigInteger)(income);
    }
}
