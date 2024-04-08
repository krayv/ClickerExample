using System.Collections;
using System.Numerics;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = nameof(BuildingUpgrade), menuName = "ScriptableObjects/" + nameof(BuildingUpgrade))]
public class BuildingUpgrade : GameUpgrade
{
    [SerializeField] private float _baseProductionMultiplier = 2f;
    [SerializeField] private Building _building;

    public Building Building
    {
        get
        {
            return _building;
        }
    }

    public override BigInteger CalculateProduction(BigInteger currentProduction)
    {
        return (BigInteger)((double)currentProduction * _baseProductionMultiplier);       
    }
}
