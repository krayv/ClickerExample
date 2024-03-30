using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;
using Zenject;
using UniRx;

[CreateAssetMenu(fileName = nameof(ReachCookieProductionAchievement), menuName = "ScriptableObjects/" + nameof(ReachCookieProductionAchievement))]
public class ReachCookieProductionAchievement : Achievement
{
    [SerializeField] private long _cookieProductionRequirement;

    private CookieProductionModel _cookieProductionModel;
    private readonly CompositeDisposable _disposable = new();

    [Inject]
    private void Construct(CookieProductionModel cookieProductionModel, AchievementsModel achievementsModel)
    {
        _cookieProductionModel = cookieProductionModel;
        _achievementsModel = achievementsModel;
        _cookieProductionModel.CookiesPerSecond.Subscribe(OnCookieProductionChanged).AddTo(_disposable);
    }

    private void OnCookieProductionChanged(BigInteger value)
    {
        if (value >= _cookieProductionRequirement)
        {
            _achievementsModel.Achievements[this] = true;
        }
    }
}
