using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class DefaultResourceLoader : IResourceLoader
{
    private const string BuildingsPath = "Buildings";
    private const string AchievementsPath = "Achievements";
    private const string UpgradesPath = "Upgrades";
    private const string UIElementsPath = "UI";

    private DiContainer _diContainer;
    private IGameProgressLoader _gameProgressLoader;

    private GameProgress _gameProgress;

    [Inject]
    private void Construct(DiContainer diContainer, IGameProgressLoader gameProgressLoader)
    {
        _diContainer = diContainer;
        _gameProgressLoader = gameProgressLoader;
        _gameProgress = gameProgressLoader.LoadGame();
    }

    public Dictionary<Building, int> LoadProgressBuildings()
    {
        List<Building> buildings = Resources.LoadAll<Building>(BuildingsPath).ToList();
        Dictionary<Building, int> buildingsDic = new Dictionary<Building, int>();
        foreach (Building building in buildings)
        {
            _diContainer.Inject(building);
            buildingsDic.Add(building, _gameProgress.buyedBuildings[building]);
        }
        return buildingsDic;
    }

    public List<Building> LoadBuildings()
    {
        return Resources.LoadAll<Building>(BuildingsPath).ToList();
    }

    public Dictionary<Achievement, bool> LoadProgressAchievements()
    {
        List<Achievement> achievements = Resources.LoadAll<Achievement>(AchievementsPath).ToList();
        Dictionary<Achievement, bool> achievementsDic = new Dictionary<Achievement, bool>();
        foreach (Achievement achievement in achievements)
        {
            _diContainer.Inject(achievement);
            achievementsDic.Add(achievement, _gameProgress.achievedAchievements[achievement]);
        }
        return achievementsDic;
    }

    public List<Achievement> LoadAchievements()
    {
        return Resources.LoadAll<Achievement>(AchievementsPath).ToList();
    }

    public TUIItem LoadUIItem<TUIItem>() where TUIItem : UIItem
    {
        return Resources.LoadAll<TUIItem>(UIElementsPath).FirstOrDefault();
    }

    public Dictionary<GameUpgrade, bool> LoadProgressUpgrades()
    {
        List<GameUpgrade> upgrades = Resources.LoadAll<GameUpgrade>(UpgradesPath).ToList();
        Dictionary<GameUpgrade, bool> upgradesDic = new Dictionary<GameUpgrade, bool>();
        foreach (GameUpgrade upgrade in upgrades)
        {
            _diContainer.Inject(upgrade);
            upgradesDic.Add(upgrade, _gameProgress.buyedUpgrades[upgrade]);
        }
        return upgradesDic;
    }

    public List<GameUpgrade> LoadUpgrades()
    {
        return Resources.LoadAll<GameUpgrade>(UpgradesPath).ToList();
    }
}
