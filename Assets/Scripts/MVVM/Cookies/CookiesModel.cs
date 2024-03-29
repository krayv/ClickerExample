using UniRx;
using System.Numerics;
using System.Collections.Generic;
using Zenject;

public class CookiesModel
{
    public ReactiveProperty<BigInteger> Cookies { get; private set; }
    

    private readonly CompositeDisposable _disposable = new();
    private IGameProgressLoader _gameProgressLoader;

    [Inject]
    private void Construct(IGameProgressLoader gameProgressLoader)
    {
        _gameProgressLoader = gameProgressLoader;
        Cookies = new ReactiveProperty<BigInteger>();
        Cookies.Subscribe(ValidateCookies).AddTo(_disposable);
       _gameProgressLoader.GameProgress.Subscribe(SetData).AddTo(_disposable);
        
    }

    private void ValidateCookies(BigInteger value)
    {
        if (value < 0)
        {
            Cookies.Value = 0;
        }
    }

    private void SetData(GameProgress gameProgress)
    {
        Cookies.Value = gameProgress.Cookies;
    }
}
