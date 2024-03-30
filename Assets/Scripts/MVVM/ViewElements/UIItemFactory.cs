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
    protected List<TUIItem> _removedItems = new List<TUIItem>();

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

    protected void InstantiateItem(TItem item)
    {
        if (_instantiatedItems.Any(i=>i.Item == item))
        {
            return;
        }
        var removedItem = _removedItems.FirstOrDefault(i => i.Item == item);
        if(removedItem != null)
        {
            removedItem.Activate();
            _removedItems.Remove(removedItem);
            _instantiatedItems.Add(removedItem);
            return;
        }

        TUIItem uiItem = GameObject.Instantiate(_itemPrefab, _container);
        diContainer.Inject(uiItem);
        uiItem.SetupUIItem(item);
        _instantiatedItems.Add(uiItem);
        ReorderElements();
    }

    protected void RemoveItem(TItem item)
    {
        TUIItem uiItem = _instantiatedItems.FirstOrDefault(i => i.Item == item);
        _removedItems.Add(uiItem);
        _instantiatedItems.Remove(uiItem);
        uiItem.Deactivate();
    }

    protected void DestroyItem(TItem item)
    {
        TUIItem uiItem = _instantiatedItems.FirstOrDefault(i => i.Item == item);
        _instantiatedItems.Remove(uiItem);
    }
}
