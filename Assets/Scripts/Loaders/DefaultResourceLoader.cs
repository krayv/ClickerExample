using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class DefaultResourceLoader : IResourceLoader
{
    private const string BuildingsPath = "Buildings";
    private const string AchievementsPath = "Achievements";
    private const string UpgradesPath = "Upgrades";
    private const string UIElementsPath = "UI";

    private DiContainer _diContainer;

    [Inject]
    private void Construct(DiContainer diContainer)
    {
        _diContainer = diContainer;
    }

    public TUIItem LoadUIItem<TUIItem>() where TUIItem : UIItem
    {
        return Resources.LoadAll<TUIItem>(UIElementsPath).FirstOrDefault();
    }

    public List<TItem> LoadItems<TItem>() where TItem : Item
    {
        string path = "";
        Type itemType = typeof(TItem);
        if (itemType == typeof(Building))
        {
            path = BuildingsPath;
        }
        if (itemType == typeof(Achievement))
        {
            path = AchievementsPath;
        }
        if (itemType == typeof(GameUpgrade))
        {
            path = UpgradesPath;
        }
        List<TItem> items = Resources.LoadAll<TItem>(path).ToList();
        return items;
    }

    public List<Item> LoadAllItems()
    {
        List<Item> items = Resources.LoadAll<Item>(BuildingsPath).ToList();
        items = items.Concat(Resources.LoadAll<Item>(AchievementsPath)).ToList();
        items = items.Concat(Resources.LoadAll<Item>(UpgradesPath)).ToList();
        return items;
    }
}
