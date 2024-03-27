using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.UI;
using UniRx;

public class UpgradesView : View
{
    private UpgradesViewModel _upgradeViewModel;
    private UpgradesUIItemFactory _upgradeUIItemFactory;

    [SerializeField] private Transform container;
    [SerializeField] protected Button closeButton;

    [Inject]
    private void Construct(UpgradesViewModel upgradesViewModel, UpgradesUIItemFactory upgradesUIItemFactory)
    {
        _upgradeViewModel = upgradesViewModel;
        _upgradeUIItemFactory = upgradesUIItemFactory;
        _upgradeUIItemFactory.InstantiateItems(container);
        closeButton.OnClickAsObservable().Subscribe(_ => CloseView());
    }
}
