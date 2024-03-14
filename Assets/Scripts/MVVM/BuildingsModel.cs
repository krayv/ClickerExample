using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Zenject;

public class BuildingsModel
{
    public ReactiveDictionary<Building, int> OwnedBuildings { get; private set; }

    public ReactiveCollection<Building> Buildings { get; private set; }

    private readonly CompositeDisposable _disposable = new();

    private IResourceLoader _resourceLoader;

    [Inject]
    private void Construct(IResourceLoader loader)
    {
        _resourceLoader = loader;
        Buildings = _resourceLoader.LoadBuildings().ToReactiveCollection();

        OwnedBuildings = new ReactiveDictionary<Building, int>();
        OwnedBuildings.ObserveAdd().Subscribe(ValidateBuildings).AddTo(_disposable);
    }

    private void ValidateBuildings(DictionaryAddEvent<Building, int> keyValuePair)
    {
        if (keyValuePair.Value <= 0)
        {
            OwnedBuildings.Remove(keyValuePair.Key);
        }
    }
}
