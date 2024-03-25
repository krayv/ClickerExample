using System.Collections;
using UnityEngine;
using Zenject;
using UniRx;

public class UpgradesUIItemFactory : UIItemFactory<GameUpgrade, UpgradeUIItem>
{
    private UpgradesViewModel _upgradesViewModel;

    private readonly CompositeDisposable _disposable = new();

    [Inject]
    private void Construct(UpgradesViewModel upgradesViewModel)
    {
        _upgradesViewModel = upgradesViewModel;
    }

    public override void InstantiateItems(Transform container)
    {
        _container = container;
        foreach (var building in _upgradesViewModel.Upgrades)
        {
            InstantiateItem(building.Key);
        }
        _upgradesViewModel.Upgrades.ObserveAdd().Subscribe(OnAddItem).AddTo(_disposable);
    }

    private void OnAddItem(DictionaryAddEvent<GameUpgrade, bool> achievement)
    {
        InstantiateItem(achievement.Key);
    }
}
