using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniRx;
using UniRx.Triggers;
using System;
using Zenject;

public abstract class UIItem : MonoBehaviour
{
    [SerializeField] protected Image icon;
    [SerializeField] private RectTransform mainRectTransform;
    [SerializeField] private Vector2 tooltipOffset;

    private Tooltip _tooltip;

    public abstract Item Item 
    { 
        get; 
    }

    [Inject]
    private void Construct(Tooltip tooltip)
    {
        _tooltip = tooltip;
    }

    public abstract void SetupUIItem(Item item);

    public void Awake()
    {
        var mouseOver = Observable.EveryFixedUpdate().AsObservable()
            .Where(_ => _tooltip.CurrentItem != Item && RectTransformUtility.RectangleContainsScreenPoint(mainRectTransform, Input.mousePosition))
            .Subscribe(xs => _tooltip.ShowTooltip(Item, mainRectTransform, tooltipOffset));
        var mouseExit = Observable.EveryFixedUpdate().AsObservable()
            .Where(_ => _tooltip.CurrentItem == Item && !RectTransformUtility.RectangleContainsScreenPoint(mainRectTransform, Input.mousePosition))
            .Subscribe(xs => _tooltip.HideTooltip());
    }

}
