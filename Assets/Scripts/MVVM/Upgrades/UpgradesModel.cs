using UniRx;
using Zenject;

public class UpgradesModel 
{
    public ReactiveDictionary<GameUpgrade, bool> Upgrades { get; private set; }
    public ReactiveCollection<GameUpgrade> AchievedUpgrades { get; private set; }

    private IGameProgressLoader _loader;
    private AchievementsModel _achievementsModel;

    [Inject]
    private void Construct(IGameProgressLoader loader, AchievementsModel achievementsModel)
    {
        _loader = loader;
        _achievementsModel = achievementsModel;
        AchievedUpgrades = new ReactiveCollection<GameUpgrade>();

        Upgrades = loader.GetProgressData().PurchasedUpgrades.ToReactiveDictionary();
        foreach (var upgrade in Upgrades)
        {
            if (upgrade.Key.RequieredAchievement == null || _achievementsModel.Achievements[upgrade.Key.RequieredAchievement])
            {
                AchievedUpgrades.Add(upgrade.Key);
            }
        }
    }
}
