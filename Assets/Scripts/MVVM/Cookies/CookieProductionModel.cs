using UniRx;
using Zenject;
using System.Collections.Generic;
using System.Numerics;
public class CookieProductionModel
{
    private CookiesModel _cookiesModel;
    private BuildingsModel _buildingsModel;
    private UpgradesModel _upgradesModel;

    public ReactiveProperty<BigInteger> CookiesPerSecond { get; private set; }

    private readonly CompositeDisposable _disposable = new();

    [Inject]
    private void Construct(CookiesModel cookiesModel, BuildingsModel buildingsModel, UpgradesModel upgradesModel)
    {
        _cookiesModel = cookiesModel;
        _buildingsModel = buildingsModel;
        _upgradesModel = upgradesModel;

        CookiesPerSecond = new ReactiveProperty<BigInteger>();

        _buildingsModel.Buildings.ObserveAdd().Subscribe(_ => RecalculateCookieProduction()).AddTo(_disposable);
        _buildingsModel.Buildings.ObserveReplace().Subscribe(_ => RecalculateCookieProduction()).AddTo(_disposable);
        _upgradesModel.Upgrades.ObserveReplace().Where(_ => _.NewValue == true).Subscribe(_ => RecalculateCookieProduction()).AddTo(_disposable);
    }

    private void RecalculateCookieProduction()
    {
        BigInteger value;

        foreach (var building in _buildingsModel.Buildings)
        {
            value += building.Key.GetSummoryProduction();
        }
        CookiesPerSecond.Value = value;
    }
}
