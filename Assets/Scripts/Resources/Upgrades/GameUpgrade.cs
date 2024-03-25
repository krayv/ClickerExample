using System.Collections;
using UnityEngine;
using System.Numerics;
using Zenject;
using UniRx;

public abstract class GameUpgrade : BuyableItem
{
    public Achievement RequiereAchievement;


    private AchievementsModel _achievementsModel;
    private UpgradesModel _upgradesModel;
    private readonly CompositeDisposable _disposable = new();

    [Inject]
    private void Construct(AchievementsModel achievementsModel, UpgradesModel upgradesModel)
    {
        _achievementsModel = achievementsModel;
        _upgradesModel = upgradesModel;

        _achievementsModel.Achievements.ObserveReplace().Where(_ => _.Key == RequiereAchievement).Subscribe(OnUpdateAchieve).AddTo(_disposable);
    }

    private void OnUpdateAchieve(DictionaryReplaceEvent<Achievement, bool> replaceEvent)
    {
        if (replaceEvent.NewValue)
        {
            _upgradesModel.AchievedUpgrades.Add(this);
        }
    }

    public abstract BigInteger CalculateProduction(BigInteger currentProduction);

}
