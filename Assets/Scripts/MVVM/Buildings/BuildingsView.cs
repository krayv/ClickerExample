using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BuildingsView : View
{
    private BuildingsViewModel _buildingsView;
    private BuildingsUIItemFactory _buildingsUIItemFactory;

    [SerializeField] private Transform container;

    [Inject]
    private void Construct(BuildingsViewModel buildingsViewModel, BuildingsUIItemFactory buildingsUIItemFactory)
    {
        _buildingsView = buildingsViewModel;
        _buildingsUIItemFactory = buildingsUIItemFactory;
        _buildingsUIItemFactory.InstantiateItems(container);
    }


    public override void OpenView()
    {
        base.OpenView();       
    }
}
