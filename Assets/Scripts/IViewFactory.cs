using UnityEngine;

public interface IViewFactory
{
    public View CreateViewFromPrefab(View view, Transform origin);
}
