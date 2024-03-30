using System.Numerics;
using UnityEngine;
[CreateAssetMenu(fileName = nameof(ClickUpgrade), menuName = "ScriptableObjects/" + nameof(ClickUpgrade))]
public class ClickUpgrade : GameUpgrade
{
    [SerializeField] private float _clickProductionMultiplier;
    public override BigInteger CalculateProduction(BigInteger currentProduction)
    {
        return (BigInteger)((double)currentProduction * _clickProductionMultiplier);
    }
}
