using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;
public class UpgradesViewModel
{
    private UpgradesModel _upgradesModel;
    private CookiesModel _cookieModel;

    public ReactiveDictionary<GameUpgrade, bool> Upgrades => _upgradesModel.Upgrades;

    [Inject]
    private void Construct(UpgradesModel upgradesModel, CookiesModel cookiesModel)
    {
        _upgradesModel = upgradesModel;
        _cookieModel = cookiesModel;
    }

    public void BuyUpgrade(GameUpgrade upgrade)
    {
        _cookieModel.Cookies.Value -= upgrade.BasePrice;
        _upgradesModel.Upgrades[upgrade] = true;
    }
}
