using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.UI;
using UniRx;

public class BuildingsView : View
{
    private BuildingsViewModel _buildingsView;
    private BuildingsUIItemFactory _buildingsUIItemFactory;

    [SerializeField] private Transform container;
    [SerializeField] protected Button closeButton;

    [Inject]
    private void Construct(BuildingsViewModel buildingsViewModel, BuildingsUIItemFactory buildingsUIItemFactory)
    {
        _buildingsView = buildingsViewModel;
        _buildingsUIItemFactory = buildingsUIItemFactory;
        _buildingsUIItemFactory.InstantiateItems(container);
        closeButton.OnClickAsObservable().Subscribe(_ => CloseView());
    }
}
