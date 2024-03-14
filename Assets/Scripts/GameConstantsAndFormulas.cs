using System.Numerics;
using System;
public static class GameConstantsAndFormulas 
{
    public const float INCREASE_PRICE_PER_BUILDING = 0.03f;

    public static BigInteger CalculateBuildingPrice(int buildingCount, long basePrice)
    {
        if (buildingCount == 0)
        {
            return basePrice;
        }
        BigInteger value;
        double exp = 1 + (buildingCount * INCREASE_PRICE_PER_BUILDING);
        value = (long)Math.Pow(basePrice, exp);
        return value;
    }
}
