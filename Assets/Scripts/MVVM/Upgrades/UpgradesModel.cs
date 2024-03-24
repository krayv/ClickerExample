using UniRx;
using Zenject;

public class UpgradesModel 
{
    public ReactiveDictionary<GameUpgrade, bool> Upgrades { get; private set; }

    private IResourceLoader _loader;

    [Inject]
    private void Construct(IResourceLoader loader)
    {
        _loader = loader;
        Upgrades = loader.LoadUpgrades().ToReactiveDictionary();
    }
}
