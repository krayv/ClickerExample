using System.Collections;
using UnityEngine;
using UniRx;
using Zenject;
using System.Numerics;

public class GameStatisticModel
{
    public ReactiveProperty<BigInteger> CookiesBaked;
    public ReactiveProperty<BigInteger> CookiesClicked;

    private CookiesViewModel _cookiesViewModel;
    private IGameProgressLoader _gameProgressLoader;
    private readonly CompositeDisposable _disposable = new();

    [Inject]
    private void Construct(CookiesViewModel cookiesViewModel, IGameProgressLoader gameProgressLoader)
    {
        _cookiesViewModel = cookiesViewModel;
        _gameProgressLoader = gameProgressLoader;
        CookiesBaked = new ReactiveProperty<BigInteger>();
        CookiesClicked = new ReactiveProperty<BigInteger>();

        _cookiesViewModel.OnClickCookie += OnClickCookie;
        _cookiesViewModel.OnProduceCookie += OnProduceCookies;

        _gameProgressLoader.GameProgress.Subscribe(OnGameProgressValueChanged).AddTo(_disposable);
        _gameProgressLoader = gameProgressLoader;
    }

    private void OnClickCookie(BigInteger value)
    {
        CookiesClicked.Value += value;
        CookiesBaked.Value += value;
    }

    private void OnProduceCookies(BigInteger value)
    {
        CookiesBaked.Value += value;
    }

    private void OnGameProgressValueChanged(GameProgress gameProgress)
    {
        CookiesClicked.Value = gameProgress.CookiesClicked;
        CookiesBaked.Value = gameProgress.CookiesProduced;
    }
}
