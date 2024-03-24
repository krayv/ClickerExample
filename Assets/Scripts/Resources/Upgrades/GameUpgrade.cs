using System.Collections;
using UnityEngine;
using System.Numerics;

public abstract class GameUpgrade : BuyableItem
{
    public UpgradeType UpgradeType;
    public int Weight;

    public abstract BigInteger CalculateProduction(BigInteger currentProduction);
}
