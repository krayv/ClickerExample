using System.Collections.Generic;

public interface IResourceLoader
{
    public List<Building> LoadBuildings();

    public BuildingUIItem LoadBuildingBuildingUIItemPrefab();
}
