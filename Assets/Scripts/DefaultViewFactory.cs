using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class DefaultViewFactory : IViewFactory
{
    DiContainer diContainer;

    [Inject]
    private void Construct(DiContainer diContainer)
    {
        this.diContainer = diContainer;
    }

    public View CreateViewFromPrefab(View view, Transform origin)
    {
        diContainer.InstantiatePrefab(view, origin);
        return view;
    }
}
