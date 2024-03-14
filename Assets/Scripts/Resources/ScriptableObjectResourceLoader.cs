using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ScriptableObjectResourceLoader : IResourceLoader
{
    private const string BuildingsPath = "Resources/Buildings";
    public List<Building> LoadBuildings()
    {
        return Resources.LoadAll<Building>(BuildingsPath).ToList();
    }
}
