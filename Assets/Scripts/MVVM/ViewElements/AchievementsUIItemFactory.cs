using System.Collections;
using UnityEngine;
using UniRx;
using Zenject;

public class AchievementsUIItemFactory : UIItemFactory<Achievement, AchievementUIItem>
{
    private AchievementsModel _achievementsModel;

    [Inject]
    private void Construct(AchievementsModel achievementsModel)
    {
        _achievementsModel = achievementsModel;
    }

    public override void InstantiateItems(Transform container)
    {
        _container = container;
        foreach (var building in _achievementsModel.Achievements)
        {
            InstantiateItem(building.Key);
        }
        _achievementsModel.Achievements.ObserveAdd().Subscribe(OnAddItem);
    }

    private void OnAddItem(DictionaryAddEvent<Achievement, bool> achievement)
    {
        InstantiateItem(achievement.Key);
    }
}
