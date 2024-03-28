using System.Collections.Generic;

public interface IResourceLoader
{
    public Dictionary<Building, int> LoadProgressBuildings();

    public Dictionary<Achievement, bool> LoadProgressAchievements();

    public Dictionary<GameUpgrade, bool> LoadProgressUpgrades();

    public TUIItem LoadUIItem<TUIItem>() where TUIItem : UIItem;
}
