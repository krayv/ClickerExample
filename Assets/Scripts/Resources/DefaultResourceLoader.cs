using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Zenject;

public class DefaultResourceLoader : IResourceLoader
{
    private const string BuildingsPath = "Buildings";
    private const string BuildingsUIItemPath = "UI/BuildingItem";
    private const string AchievementUIItemPath = "UI/AchievementItem";
    private const string AchievementsPath = "Achievements";
    private const string UIElementsPath = "UI";

    private DiContainer _diContainer;

    [Inject]
    private void Construct(DiContainer diContainer)
    {
        _diContainer = diContainer;
    }

    public List<Building> LoadBuildings()
    {
        return Resources.LoadAll<Building>(BuildingsPath).ToList();
    }

    public BuildingUIItem LoadBuildingUIItemPrefab()
    {
        return Resources.Load<BuildingUIItem>(BuildingsUIItemPath);
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

    public AchievementUIItem LoadAchievementUIItemPrefab()
    {
        return Resources.Load<AchievementUIItem>(AchievementUIItemPath);
    }

    public TUIItem LoadUIItem<TUIItem>() where TUIItem : UIItem
    {
        return Resources.LoadAll<TUIItem>(UIElementsPath).FirstOrDefault();
    }
}
