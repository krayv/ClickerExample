using UnityEngine;
using UniRx;
using System.Numerics;
using Zenject;

public class BuildingsViewModel
{
    public ReactiveProperty<BigInteger> Cookies => _cookiesModel.Cookies;
    public ReactiveDictionary<Building, int> Buildings => _buildingsModel.Buildings;
    public ReactiveDictionary<Building, BigInteger> BuildingPrices => _buildingsModel.BuildingPrices;
    public ReactiveDictionary<Building, bool> AvailableBuildingsForBuying { get; private set; }
    public ReactiveDictionary<Building, bool> AvailableBuildingsForSelling { get; private set; }

    private readonly CompositeDisposable _disposable = new();
    private CookiesModel _cookiesModel;
    private BuildingsModel _buildingsModel;


    [Inject]
    private void Construct(CookiesModel cookiesModel, BuildingsModel buildingsModel)
    {
        AvailableBuildingsForBuying = new ReactiveDictionary<Building, bool>();
        AvailableBuildingsForSelling = new ReactiveDictionary<Building, bool>();
        
        _buildingsModel = buildingsModel;
        Buildings.ObserveAdd().Subscribe(OnAddBuilding).AddTo(_disposable);
        Buildings.ObserveReplace().Subscribe(UpdateAvailableBuildings).AddTo(_disposable);

        _cookiesModel = cookiesModel;
        Cookies.Subscribe(UpdateAvailableBuildings).AddTo(_disposable);

        foreach (var building in Buildings)
        {
            AddNewBuilding(building.Key, building.Value);
        }
    }

    private void OnAddBuilding(DictionaryAddEvent<Building, int> addBuilding)
    {
        AddNewBuilding(addBuilding.Key, addBuilding.Value);
    }
    
    private void AddNewBuilding(Building addBuilding, int count)
    {
        if(!AvailableBuildingsForBuying.ContainsKey(addBuilding))
            AvailableBuildingsForBuying.Add(addBuilding, IsAvailableForBuying(addBuilding));
        if(!AvailableBuildingsForSelling.ContainsKey(addBuilding))
            AvailableBuildingsForSelling.Add(addBuilding, count > 0);
    }

    private void UpdateAvailableBuildings(BigInteger value)
    {
        foreach (var buildingPrice in BuildingPrices)
        {
            AvailableBuildingsForBuying[buildingPrice.Key] = IsAvailableForBuying(buildingPrice.Key);
        }
    }

    private bool IsAvailableForBuying(Building building)
    {
        return BuildingPrices[building] <= Cookies.Value;
    }

    private void UpdateAvailableBuildings(DictionaryReplaceEvent<Building, int> buildingForUpdate)
    {
        AvailableBuildingsForBuying[buildingForUpdate.Key] = IsAvailableForBuying(buildingForUpdate.Key);
        AvailableBuildingsForSelling[buildingForUpdate.Key] = buildingForUpdate.NewValue > 0;
    }

    public void BuyBuilding(Building building)
    {
        _cookiesModel.Cookies.Value -= BuildingPrices[building];
        _buildingsModel.Buildings[building]++;
    }

    public void SellBuilding(Building building)
    {
        Cookies.Value += (BigInteger)((float)GameConstantsAndFormulas.CalculateBuildingPrice(Buildings[building] - 1, building.BasePrice) * GameConstantsAndFormulas.SELL_BUILDING_PRICE_MODIFIER);
        Buildings[building]--;

    }
}
