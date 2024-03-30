using TMPro;
using UniRx;
using Zenject;

public class UpgradesModel 
{
    public ReactiveDictionary<GameUpgrade, bool> Upgrades { get; private set; }
    public ReactiveCollection<GameUpgrade> AchievedUpgrades { get; private set; }

    private IGameProgressLoader _loader;
    private AchievementsModel _achievementsModel;
    private readonly CompositeDisposable _disposable = new();

    [Inject]
    private void Construct(IGameProgressLoader loader, AchievementsModel achievementsModel)
    {
        _loader = loader;
        _achievementsModel = achievementsModel;
        AchievedUpgrades = new ReactiveCollection<GameUpgrade>();
        Upgrades = new ReactiveDictionary<GameUpgrade, bool>();
        loader.GameProgress.Subscribe(SetData).AddTo(_disposable);       
    }


    private void SetData(GameProgress gameProgress)
    {       
        foreach (var upgrade in gameProgress.PurchasedUpgrades)
        {
            Upgrades[upgrade.Key] = upgrade.Value;
        }

        foreach (var upgrade in Upgrades)
        {
            if (upgrade.Key.RequieredAchievement == null || _achievementsModel.Achievements[upgrade.Key.RequieredAchievement])
            {
                AchievedUpgrades.Add(upgrade.Key);
            }
        }
    }
}
