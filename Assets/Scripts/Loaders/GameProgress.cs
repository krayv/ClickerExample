using System.Collections.Generic;
using System.Numerics;

public struct GameProgress
{
    public Dictionary<Building, int> PurchasedBuildings;
    public Dictionary<Achievement, bool> AchievedAchievements;
    public Dictionary<GameUpgrade, bool> PurchasedUpgrades;
    public BigInteger Cookies;
    public BigInteger CookiesProduced;
    public BigInteger CookiesClicked;
    
    public GameProgress(GameProgressJSONDataFormat data, IResourceLoader resourceLoader)
    {
        PurchasedBuildings = new Dictionary<Building, int>();
        List<Building> buildings = resourceLoader.LoadItems<Building>();

        foreach (Building building in buildings)
        {
            PurchasedBuildings.Add(building, data.PurchasedBuildings.ContainsKey(building.ID) ? data.PurchasedBuildings[building.ID] : 0);
        }

        AchievedAchievements = new Dictionary<Achievement, bool>();
        List<Achievement> achievements = resourceLoader.LoadItems<Achievement>();
        foreach (Achievement achievement in achievements)
        {
            AchievedAchievements.Add(achievement, data.AchievedAchieves.ContainsKey(achievement.ID) && data.AchievedAchieves[achievement.ID]);
        }

        PurchasedUpgrades = new Dictionary<GameUpgrade, bool>();
        List<GameUpgrade> upgrades = resourceLoader.LoadItems<GameUpgrade>();
        foreach (GameUpgrade upgrade in upgrades)
        {
            PurchasedUpgrades.Add(upgrade, data.PurchasedUpgrades.ContainsKey(upgrade.ID) && data.PurchasedUpgrades[upgrade.ID]);
        }

        Cookies = data.CookiesInBank;
        CookiesProduced = data.CookiesProduced;
        CookiesClicked = data.CookiesClicked;
    }
}
