using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Zenject;

public class DefaultResourceLoader : IResourceLoader
{
    private const string BuildingsPath = "Buildings";
    private const string AchievementsPath = "Achievements";
    private const string UpgradesPath = "Upgrades";
    private const string UIElementsPath = "UI";

    private DiContainer _diContainer;

    [Inject]
    private void Construct(DiContainer diContainer)
    {
        _diContainer = diContainer;
    }

    public Dictionary<Building, int> LoadBuildings()
    {
        List<Building> buildings = Resources.LoadAll<Building>(BuildingsPath).ToList();
        Dictionary<Building, int> buildingsDic = new Dictionary<Building, int>();
        foreach (Building building in buildings)
        {
            _diContainer.Inject(building);
            buildingsDic.Add(building, 0);
        }
        return buildingsDic;
    }

    public Dictionary<Achievement, bool> LoadAchievements()
    {
        List<Achievement> achievements = Resources.LoadAll<Achievement>(AchievementsPath).ToList();
        Dictionary<Achievement, bool> achievementsDic = new Dictionary<Achievement, bool>();
        foreach (Achievement achievement in achievements)
        {
            _diContainer.Inject(achievement);
            achievementsDic.Add(achievement, false);
        }
        return achievementsDic;
    }

    public TUIItem LoadUIItem<TUIItem>() where TUIItem : UIItem
    {
        return Resources.LoadAll<TUIItem>(UIElementsPath).FirstOrDefault();
    }

    public Dictionary<GameUpgrade, bool> LoadUpgrades()
    {
        List<GameUpgrade> upgrades = Resources.LoadAll<GameUpgrade>(UpgradesPath).ToList();
        Dictionary<GameUpgrade, bool> upgradesDic = new Dictionary<GameUpgrade, bool>();
        foreach (GameUpgrade upgrade in upgrades)
        {
            _diContainer.Inject(upgrade);
            upgradesDic.Add(upgrade, false);
        }
        return upgradesDic;
    }
}
