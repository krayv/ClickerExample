using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Zenject;
using System.Numerics;

public class BuildingsModel
{
    public ReactiveDictionary<Building, int> Buildings { get; private set; }
    public ReactiveDictionary<Building, BigInteger> BuildingPrices { get; private set; }

    private readonly CompositeDisposable _disposable = new();

    private IGameProgressLoader _resourceLoader;

    [Inject]
    private void Construct(IGameProgressLoader loader)
    {

        _resourceLoader = loader;
        Buildings = new ReactiveDictionary<Building, int>();        
        Buildings.ObserveReplace().Subscribe(ValidateBuildings).AddTo(_disposable);
        Buildings.ObserveReplace().Subscribe(UpdatePrices).AddTo(_disposable);
        Buildings.ObserveAdd().Subscribe(OnAddBuilding).AddTo(_disposable);

        BuildingPrices = new ReactiveDictionary<Building, BigInteger>();

        _resourceLoader.GameProgress.Subscribe(SetData).AddTo(_disposable);
    }

    private void OnAddBuilding(DictionaryAddEvent<Building, int> building)
    {
        BuildingPrices.Add(building.Key, GameConstantsAndFormulas.CalculateBuildingPrice(Buildings[building.Key], building.Key.BasePrice));
    }

    private void UpdatePrices(DictionaryReplaceEvent<Building, int> buildingForUpdate)
    {
        BuildingPrices[buildingForUpdate.Key] = GameConstantsAndFormulas.CalculateBuildingPrice(Buildings[buildingForUpdate.Key], buildingForUpdate.Key.BasePrice);
    }

    private void ValidateBuildings(DictionaryReplaceEvent<Building, int> keyValuePair)
    {
        if (keyValuePair.NewValue < 0)
        {
            Buildings[keyValuePair.Key] = 0;
        }
    }

    private void SetData(GameProgress gameProgress)
    {
        foreach (var building in gameProgress.PurchasedBuildings)
        {
            Buildings[building.Key] = building.Value;
        }
    }
}
