using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BuildingsView : View
{
    private BuildingsViewModel _buildingsView;

    [Inject]
    private void Construct(BuildingsViewModel buildingsViewModel)
    {
        _buildingsView = buildingsViewModel;
    }
}
