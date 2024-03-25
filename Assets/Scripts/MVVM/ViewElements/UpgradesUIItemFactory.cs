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
        foreach (var upgrade in _upgradesViewModel.AchievedUpgrades)
        {
            InstantiateItem(upgrade);
        }
        _upgradesViewModel.AchievedUpgrades.ObserveAdd().Subscribe(OnAddItem).AddTo(_disposable);
    }

    private void OnAddItem(CollectionAddEvent<GameUpgrade> upgrade)
    {
        InstantiateItem(upgrade.Value);
    }
}
