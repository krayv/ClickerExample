using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UniRx;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = nameof(ClickCookiesAmountAchievement), menuName = "ScriptableObjects/" + nameof(ClickCookiesAmountAchievement))]
public class ClickCookiesAmountAchievement : Achievement
{
    private GameStatisticModel _gameStatistic;

    [SerializeField] private long _requireCookiesClicked;

    private CompositeDisposable _disposables = new CompositeDisposable();

    [Inject]
    private void Construct(GameStatisticModel gameStatistic)
    {
        _gameStatistic = gameStatistic;
        _gameStatistic.CookiesClicked.AsObservable().Subscribe(OnUpdateCookiesClicked).AddTo(_disposables);
    }

    private void OnUpdateCookiesClicked(BigInteger value)
    {
        if (value >= _requireCookiesClicked && !IsAchievementReceived())
        {
            _achievementsModel.Achievements[this] = true;
        }
    }
}
