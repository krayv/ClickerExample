using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using TMPro;
using Zenject;

public class CookiesView : View
{

    private CookiesViewModel _cookiesViewModel;

    [SerializeField] private TextMeshProUGUI _cookiesCountText;
    [SerializeField] private Button _clickButton;
    [SerializeField] private Button _openBuildingsButton;

    private readonly CompositeDisposable _disposable = new();

    [Inject]
    private void Contsruct(CookiesViewModel cookiesViewModel)
    {
        _cookiesViewModel = cookiesViewModel;

        _cookiesViewModel.Cookies.Subscribe(_ => _cookiesCountText.text = _.ToString()).AddTo(_disposable);
        _clickButton.OnClickAsObservable().Subscribe(_ => _cookiesViewModel.ClickCookie()).AddTo(_disposable);
        _openBuildingsButton.OnClickAsObservable().Subscribe(_ => _cookiesViewModel.OpenBuildingsView());
    }
    private void OnDestroy()
    {
        _disposable.Clear();
    }
}
