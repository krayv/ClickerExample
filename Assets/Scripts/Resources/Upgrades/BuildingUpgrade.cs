using System.Collections;
using System.Numerics;
using UnityEngine;
using Zenject;

public class BuildingUpgrade : GameUpgrade
{
    [SerializeField] private float _baseProductionMultiplier = 2f;
    [SerializeField] private Building _building;

    public override BigInteger CalculateProduction(BigInteger currentProduction)
    {       
        return (BigInteger)((double)currentProduction * _baseProductionMultiplier);
    }
}
