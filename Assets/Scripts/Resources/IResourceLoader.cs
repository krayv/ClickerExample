using System.Collections.Generic;

public interface IResourceLoader
{
    public Dictionary<Building, int> LoadBuildings();

    public Dictionary<Achievement, bool> LoadAchievements();

    public Dictionary<GameUpgrade, bool> LoadUpgrades();

    public TUIItem LoadUIItem<TUIItem>() where TUIItem : UIItem;
}
