using System.Collections.Generic;
using System.Numerics;
using System.Linq;
using UniRx;
public struct GameProgressJSONDataFormat
{
    public Dictionary<int, int> PurchasedBuildings;
    public Dictionary<int, bool> PurchasedUpgrades;
    public Dictionary<int, bool> AchievedAchieves;
    public BigInteger CookiesInBank;

    public GameProgressJSONDataFormat(Dictionary<Building, int> purchasedBuildings, Dictionary<GameUpgrade, bool> purchasedUpgrades, Dictionary<Achievement, bool> achievedAchieves, BigInteger cookiesInBank)
    {
        PurchasedBuildings = Item.ConvertSOKeyDictionaryToIDKeyDictionary(purchasedBuildings);
        PurchasedUpgrades = Item.ConvertSOKeyDictionaryToIDKeyDictionary(purchasedUpgrades);
        AchievedAchieves = Item.ConvertSOKeyDictionaryToIDKeyDictionary(achievedAchieves);
        CookiesInBank = cookiesInBank;
    }

    public GameProgressJSONDataFormat(ReactiveDictionary<Building, int> purchasedBuildings, ReactiveDictionary<GameUpgrade, bool> purchasedUpgrades, ReactiveDictionary<Achievement, bool> achievedAchieves, BigInteger cookiesInBank)
    {
        PurchasedBuildings = Item.ConvertSOKeyDictionaryToIDKeyDictionary(purchasedBuildings);
        PurchasedUpgrades = Item.ConvertSOKeyDictionaryToIDKeyDictionary(purchasedUpgrades);
        AchievedAchieves = Item.ConvertSOKeyDictionaryToIDKeyDictionary(achievedAchieves);
        CookiesInBank = cookiesInBank;
    }
}
