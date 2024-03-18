using System.Collections;
using UnityEngine;
using Zenject;

public abstract class UIItemFactory<TItem, TUIItem> : Factory, IUIItemFactory where TItem: Item where TUIItem: UIItem
{
    private IResourceLoader _resourceLoader;

    protected Transform _container;
    protected TUIItem _itemPrefab;

    [Inject]
    protected void Construct(IResourceLoader resourceLoader)
    {
        _resourceLoader = resourceLoader;
        _itemPrefab = _resourceLoader.LoadUIItem<TUIItem>();
    }

    public abstract void InstantiateItems(Transform container);


    protected void InstantiateItem(TItem achievement)
    {
        TUIItem item = GameObject.Instantiate(_itemPrefab, _container);
        diContainer.Inject(item);
        item.SetupUIItem(achievement);
    }
}
