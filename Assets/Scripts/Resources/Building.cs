using UnityEngine;
using UniRx;
using System.Numerics;
using Zenject;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName = nameof(Building), menuName = "ScriptableObjects/" + nameof(Building))]
public class Building : PurchasableItem
{
    public long BaseProduction;

    private BuildingsModel _buildingsModel;
    private UpgradesModel _upgradesModel;

    [Inject]
    private void Construct(BuildingsModel buildingsModel, UpgradesModel upgrades)
    {
        _buildingsModel = buildingsModel;
        _upgradesModel = upgrades;
    }
   
    public BigInteger GetSummaryProduction()
    {
        BigInteger summaryProduction = _buildingsModel.Buildings[this] * BaseProduction;
        List<BuildingUpgrade> buildingUpgrades = _upgradesModel.Upgrades.Where(u => u.Value && u.Key is BuildingUpgrade).Select(u=>u.Key as BuildingUpgrade).Where(bu=>bu.Building == this).ToList();
        if (!buildingUpgrades.Any())
        {
            return summaryProduction;
        }

        foreach (BuildingUpgrade buildingUpgrade in buildingUpgrades)
        {
            summaryProduction = buildingUpgrade.CalculateProduction(summaryProduction);
        }
        return summaryProduction;
    }
}
