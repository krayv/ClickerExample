using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultViewFactory : Factory, IViewFactory
{
    public View CreateViewFromPrefab(View view, Transform origin)
    {
        diContainer.InstantiatePrefab(view, origin);
        return view;
    }
}
