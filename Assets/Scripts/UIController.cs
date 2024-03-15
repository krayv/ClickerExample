using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System.Linq;

public class UIController : MonoBehaviour
{
    private IViewFactory _viewFactory;

    [SerializeField] private Transform _origin;

    [SerializeField] private List<View> _viewsPrefabs = new List<View>();
    private readonly List<View> _instantiatedViews = new List<View>();

    [Inject]
    private void Construct(IViewFactory factory)
    {
        _viewFactory = factory;

    }

    public void Start()
    {
        OpenView<CookiesView>();
    }

    public void OpenView<TView>() where TView : View
    {
        View view = _instantiatedViews.Find(v => v is TView);
        if (view == null)
        {
            View viewPrefab = _viewsPrefabs.Find(v => v is TView);
            view = _viewFactory.CreateViewFromPrefab(viewPrefab, _origin);
            _instantiatedViews.Add(view);
        }
        view.OpenView();
    }

    public void CloseView<TView>() where TView : View
    {
        View view = _instantiatedViews.Find(v => v is TView);
        if (view == null)
        {
            return;
        }
        view.CloseView();
    }

    public void SwitchView<TView>() where TView : View
    {
        View view = _instantiatedViews.Find(v => v is TView);
        if (view == null)
        {
            View viewPrefab = _viewsPrefabs.Find(v => v is TView);
            view = _viewFactory.CreateViewFromPrefab(viewPrefab, _origin);
            _instantiatedViews.Add(view);
            view.OpenView();
            return;
        }
        view.SwitchView();
    }
}
