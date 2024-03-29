using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SOItemInjector : IInitializable
{
    private DiContainer _diContainer;
    private IResourceLoader _resourceLoader;
    public void Initialize()
    {
        List<Item> items = _resourceLoader.LoadAllItems();
        foreach (Item item in items)
        {
            _diContainer.Inject(item);
        }
    }

    [Inject]
    private void Construct(IResourceLoader resourceLoader, DiContainer diContainer)
    {
        _resourceLoader = resourceLoader;
        _diContainer = diContainer;
    }
}
