using UnityEngine;
using Zenject;
using UniRx;

public class BuildingsUIItemFactory : UIItemFactory<Building, BuildingUIItem>
{
    private BuildingsViewModel _buildingsViewModel;

    [Inject]
    private void Construct(BuildingsViewModel buildingsViewModel)
    {
        _buildingsViewModel = buildingsViewModel;
    }

    public override void InstantiateItems(Transform container)
    {
        _container = container;
        foreach (var building in _buildingsViewModel.Buildings)
        {
            InstantiateItem(building.Key);
        }
        _buildingsViewModel.Buildings.ObserveAdd().Subscribe(OnAddItem);
    }

    private void OnAddItem(DictionaryAddEvent<Building, int> building)
    {
        InstantiateItem(building.Key);
    }
}
