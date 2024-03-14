using UnityEngine;
using UniRx;
using System.Numerics;
using Zenject;

public class BuildingsViewModel
{
    public ReactiveProperty<BigInteger> Cookies => _cookiesModel.Cookies;
    public ReactiveDictionary<Building, int> Buildings => _buildingsModel.Buildings;
    public ReactiveDictionary<Building, BigInteger> BuildingPrices => _buildingsModel.BuildingPrices;
    public ReactiveDictionary<Building, bool> AwaibleBuildingsForBuying { get; private set; }

    private readonly CompositeDisposable _disposable = new();
    private CookiesModel _cookiesModel;
    private BuildingsModel _buildingsModel;


    [Inject]
    private void Construct(CookiesModel cookiesModel, BuildingsModel buildingsModel)
    {
        AwaibleBuildingsForBuying = new ReactiveDictionary<Building, bool>();

        _buildingsModel = buildingsModel;
        Buildings.ObserveAdd().Subscribe(OnAddBuilding).AddTo(_disposable);
        Buildings.ObserveReplace().Subscribe(UpdateAwaibleBuildings).AddTo(_disposable);

        _cookiesModel = cookiesModel;
        Cookies.Subscribe(UpdateAwaibleBuildings).AddTo(_disposable);            
    }

    private void OnAddBuilding(DictionaryAddEvent<Building, int> addBuilding)
    {
        AwaibleBuildingsForBuying.Add(addBuilding.Key, IsAwaibleForBuying(addBuilding.Key));
    }   

    private void UpdateAwaibleBuildings(BigInteger value)
    {
        foreach (var buildingPrice in BuildingPrices)
        {
            AwaibleBuildingsForBuying[buildingPrice.Key] = IsAwaibleForBuying(buildingPrice.Key);
        }
    }

    private bool IsAwaibleForBuying(Building building)
    {
        return BuildingPrices[building] <= Cookies.Value;
    }

    private void UpdateAwaibleBuildings(DictionaryReplaceEvent<Building, int> buildingForUpdate)
    {
         AwaibleBuildingsForBuying[buildingForUpdate.Key] = IsAwaibleForBuying(buildingForUpdate.Key);       
    }

    public void BuyBuilding(Building building)
    {
        _cookiesModel.Cookies.Value -= BuildingPrices[building];
        _buildingsModel.Buildings[building]++;
    }

    public void SellBuilding(Building building)
    {

    }
}
