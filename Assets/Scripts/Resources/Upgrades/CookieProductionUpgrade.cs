using System.Numerics;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(CookieProductionUpgrade), menuName = "ScriptableObjects/" + nameof(CookieProductionUpgrade))]
public class CookieProductionUpgrade : GameUpgrade
{
    [SerializeField] private float _productionMultiplier = 1.05f;
    public override BigInteger CalculateProduction(BigInteger currentProduction)
    {
        return (BigInteger)((double)currentProduction * _productionMultiplier);
    }
}
