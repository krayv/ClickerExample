using System.Collections.Generic;

public interface IResourceLoader
{
    public List<Building> LoadBuildings();

    public Dictionary<Achievement, bool> LoadAchievements();

    public TUIItem LoadUIItem<TUIItem>() where TUIItem : UIItem;
}
