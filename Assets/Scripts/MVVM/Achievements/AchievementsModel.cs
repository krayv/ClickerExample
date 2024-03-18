using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Zenject;

public class AchievementsModel
{
    public ReactiveDictionary<Achievement, bool> Achievements { get; private set; }

    private IResourceLoader _loader;

    [Inject]
    private void Construct(IResourceLoader loader)
    {
        _loader = loader;
        Achievements = loader.LoadAchievements().ToReactiveDictionary();
        foreach (var achievement in Achievements)
        {
            achievement.Key.StartObserve();
        }
    }
}
