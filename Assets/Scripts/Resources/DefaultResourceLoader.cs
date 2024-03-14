using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DefaultResourceLoader : IResourceLoader
{
    private const string BuildingsPath = "Buildings";
    private const string BuildingsUIItemPath = "UI/BuildingItem";

    public List<Building> LoadBuildings()
    {
        return Resources.LoadAll<Building>(BuildingsPath).ToList();
    }

    public BuildingUIItem LoadBuildingBuildingUIItemPrefab()
    {
        return Resources.Load<BuildingUIItem>(BuildingsUIItemPath);
    }
}
