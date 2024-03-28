using UniRx;
using Zenject;

public class UpgradesModel 
{
    public ReactiveDictionary<GameUpgrade, bool> Upgrades { get; private set; }
    public ReactiveCollection<GameUpgrade> AchievedUpgrades { get; private set; }

    private IResourceLoader _loader;
    private AchievementsModel _achievementsModel;

    [Inject]
    private void Construct(IResourceLoader loader, AchievementsModel achievementsModel)
    {
        _loader = loader;
        _achievementsModel = achievementsModel;
        AchievedUpgrades = new ReactiveCollection<GameUpgrade>();

        Upgrades = loader.LoadProgressUpgrades().ToReactiveDictionary();
        foreach (var upgrade in Upgrades)
        {
            if (upgrade.Key.RequiereAchievement == null || _achievementsModel.Achievements[upgrade.Key.RequiereAchievement])
            {
                AchievedUpgrades.Add(upgrade.Key);
            }
        }
    }
}
