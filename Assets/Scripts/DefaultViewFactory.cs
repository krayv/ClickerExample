using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultViewFactory : Factory, IViewFactory
{
    public View CreateViewFromPrefab(View view, Transform origin)
    {
        view = GameObject.Instantiate<View>(view, origin);
        diContainer.Inject(view);
        foreach (var unityObject in view.ObjectsForInject)
        {
            diContainer.Inject(unityObject);
        }
        return view;
    }
}
