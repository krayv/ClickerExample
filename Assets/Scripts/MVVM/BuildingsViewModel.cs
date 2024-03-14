using UnityEngine;
using UniRx;
using System.Numerics;
using Zenject;

public class BuildingsViewModel
{
    public ReactiveProperty<BigInteger> Cookies => _cookiesModel.Cookies;
    public ReactiveDictionary<Building, int> OwnedBuildings => _buildingsModel.OwnedBuildings;
    public ReactiveCollection<Building> Buildings => _buildingsModel.Buildings;

    private readonly CompositeDisposable _disposable = new();
    private CookiesModel _cookiesModel;
    private BuildingsModel _buildingsModel;


    [Inject]
    private void Construct(CookiesModel cookiesModel, BuildingsModel buildingsModel)
    {
        _cookiesModel = cookiesModel;
        _buildingsModel = buildingsModel;
    }

    private void BuyBuilding(Building building)
    {

    }
}
