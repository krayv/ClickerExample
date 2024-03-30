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
    [SerializeField] protected Image inactiveForeground;
    [SerializeField] private RectTransform mainRectTransform;
    [SerializeField] private Vector2 tooltipOffset;

    private Tooltip _tooltip;
    protected readonly CompositeDisposable _disposable = new();

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
            .Subscribe(xs => _tooltip.ShowTooltip(Item, mainRectTransform, tooltipOffset)).AddTo(_disposable);
        var mouseExit = Observable.EveryFixedUpdate().AsObservable()
            .Where(_ => _tooltip.CurrentItem == Item && !RectTransformUtility.RectangleContainsScreenPoint(mainRectTransform, Input.mousePosition))
            .Subscribe(xs => _tooltip.HideTooltip()).AddTo(_disposable);
    }

    public virtual void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public virtual void Activate()
    {
        gameObject.SetActive(true);
    }

    public virtual void DestroySelf()
    {
        Destroy(gameObject);
    }

    protected void OnDestroy()
    {
        _disposable.Clear();
    }
}
