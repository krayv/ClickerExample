using System.Numerics;
using System;
public static class GameConstantsAndFormulas 
{
    public const float INCREASE_PRICE_PER_BUILDING = 0.1f;
    public const float SELL_BUILDING_PRICE_MODIFIER = 0.75f;

    public static BigInteger CalculateBuildingPrice(int buildingCount, long basePrice)
    {
        if (buildingCount == 0)
        {
            return basePrice;
        }
        BigInteger value;
        var pow = MathF.Pow(1f + INCREASE_PRICE_PER_BUILDING, buildingCount);
        value = (BigInteger)(basePrice * pow);
        return value;
    }
}
