using UniRx;
using System.Numerics;
using System.Collections.Generic;
public class CookiesModel
{
    public ReactiveProperty<BigInteger> Cookies { get; private set; }
    

    private readonly CompositeDisposable _disposable = new();

    public CookiesModel()
    {
        Cookies = new ReactiveProperty<BigInteger>();
        Cookies.Value = new BigInteger(0);
        Cookies.Subscribe(ValidateCookies).AddTo(_disposable);       
    }

    private void ValidateCookies(BigInteger value)
    {
        if (value < 0)
        {
            Cookies.Value = 0;
        }
    }  
}
