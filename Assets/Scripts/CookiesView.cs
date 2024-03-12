using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using TMPro;
using Zenject;

public class CookiesView : View
{

    private CookiesViewModel cookiesViewModel;

    [SerializeField] private TextMeshProUGUI _cookiesCountText;
    [SerializeField] private Button _clickButton;

    private readonly CompositeDisposable _disposable = new();

    [Inject]
    private void Contsruct(CookiesViewModel cookiesViewModel)
    {
        this.cookiesViewModel = cookiesViewModel;
    }

    private void Awake()
    {
        cookiesViewModel.Cookies.Subscribe( _ => _cookiesCountText.text = _.ToString()).AddTo(_disposable);
        _clickButton.OnClickAsObservable().Subscribe(_ => cookiesViewModel.ClickCookie()).AddTo(_disposable);
    }

    private void OnDestroy()
    {
        _disposable.Clear();
    }
}
