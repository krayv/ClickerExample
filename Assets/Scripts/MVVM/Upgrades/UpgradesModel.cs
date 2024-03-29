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

        loader.GameProgress.Subscribe(SetData).AddTo(_disposable);       
    }

    private void SetData(GameProgress gameProgress)
    {
        Upgrades = gameProgress.PurchasedUpgrades.ToReactiveDictionary();
        foreach (var upgrade in Upgrades)
        {
            if (upgrade.Key.RequieredAchievement == null || _achievementsModel.Achievements[upgrade.Key.RequieredAchievement])
            {
                AchievedUpgrades.Add(upgrade.Key);
            }
        }
    }
}
