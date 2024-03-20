using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public abstract class Achievement : Item
{
    protected AchievementsModel _achievementsModel;    

    [Inject]
    private void Construct(AchievementsModel achievementsModel)
    {
        _achievementsModel = achievementsModel;
    }

    public abstract void StartObserve();

    protected bool IsAchievementReceived()
    {
        return _achievementsModel.Achievements[this];
    }
}
