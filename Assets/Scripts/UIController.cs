using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UIController : MonoBehaviour
{
    private IViewFactory _viewFactory;


    [SerializeField] private View _cookiesView;
    [SerializeField] private Transform _origin;

    [Inject]
    private void Construct(IViewFactory factory)
    {
        _viewFactory = factory;
    }

    public void Start()
    {
        _viewFactory.CreateViewFromPrefab(_cookiesView, _origin);
    }
}
