using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Zenject;

public class AchievementsModel
{
    public ReactiveDictionary<Achievement, bool> Achievements { get; private set; }

    private IGameProgressLoader _loader;

    [Inject]
    private void Construct(IGameProgressLoader loader)
    {
        _loader = loader;
        Achievements = loader.GetProgressData().AchievedAchievements.ToReactiveDictionary();
    }
}
