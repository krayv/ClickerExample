using System.Numerics;
using UniRx;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = nameof(BakeAllTimeCookieAchievement), menuName = "ScriptableObjects/" + nameof(BakeAllTimeCookieAchievement))]
public class BakeAllTimeCookieAchievement : Achievement
{
    private GameStatisticModel _gameStatisticModel;
    private CompositeDisposable _disposable = new CompositeDisposable();

    [SerializeField] long _requiredValue;

    [Inject]
    private void Construct(GameStatisticModel gameStatistic)
    {
        _gameStatisticModel = gameStatistic;
        _gameStatisticModel.CookiesBaked.AsObservable().Subscribe(OnBakedCookiesValueChanged).AddTo(_disposable);
    }

    private void OnBakedCookiesValueChanged(BigInteger value)
    {
        if (value >= _requiredValue && !IsAchievementReceived())
        {
            _achievementsModel.Achievements[this] = true;
        }
    }
}
