using System.Collections;
using UnityEngine;
using System.Numerics;

public abstract class GameUpgrade : BuyableItem
{
    public abstract BigInteger CalculateProduction(BigInteger currentProduction);
}
