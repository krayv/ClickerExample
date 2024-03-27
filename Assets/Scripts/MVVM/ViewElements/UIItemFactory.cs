using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System.Linq;

public abstract class UIItemFactory<TItem, TUIItem> : Factory, IUIItemFactory where TItem: Item where TUIItem: UIItem
{
    private IResourceLoader _resourceLoader;

    protected Transform _container;
    protected TUIItem _itemPrefab;
    protected List<TUIItem> _instantiatedItems = new List<TUIItem>();

    [Inject]
    protected void Construct(IResourceLoader resourceLoader)
    {
        _resourceLoader = resourceLoader;
        _itemPrefab = _resourceLoader.LoadUIItem<TUIItem>();
    }

    public abstract void InstantiateItems(Transform container);

    protected void ReorderElements()
    {
        var orderedItems = _instantiatedItems.OrderByDescending(i => i.Item.Order);
        foreach (var item in orderedItems)
        {
            item.gameObject.transform.SetAsFirstSibling();
        }
    }

    protected void InstantiateItem(TItem achievement)
    {
        TUIItem item = GameObject.Instantiate(_itemPrefab, _container);
        diContainer.Inject(item);
        item.SetupUIItem(achievement);
        _instantiatedItems.Add(item);
        ReorderElements();
    }
}
