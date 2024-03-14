using UnityEngine;
using Zenject;
using UniRx;

public class BuildingsUIItemFactory : Factory, IUIItemFactory
{
    private BuildingsViewModel _buildingsViewModel;
    private IResourceLoader _resourceLoader;

    private Transform _container;
    private BuildingUIItem _itemPrefab;

    [Inject]
    private void Construct(BuildingsViewModel buildingsViewModel, IResourceLoader resourceLoader)
    {
        _buildingsViewModel = buildingsViewModel;
        _resourceLoader = resourceLoader;
    }

    public void InstantiateItems(Transform container)
    {
        _itemPrefab = _resourceLoader.LoadBuildingBuildingUIItemPrefab();
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

    private void InstantiateItem(Building building)
    {
        BuildingUIItem item = GameObject.Instantiate(_itemPrefab, _container);
        diContainer.Inject(item);
        item.SetupUIItem(building);
    }  
}
