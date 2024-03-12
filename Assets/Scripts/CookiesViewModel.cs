using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Numerics;
using Zenject;

public class CookiesViewModel
{
    public ReactiveProperty<BigInteger> Cookies { get; private set; }

    private readonly CompositeDisposable _disposable = new();
    private CookiesModel _cookiesModel;

    public CookiesViewModel()
    {
        Cookies = new ReactiveProperty<BigInteger>();      
    }

    [Inject]
    private void Construct(CookiesModel cookiesModel)
    {
        this._cookiesModel = cookiesModel;
        _cookiesModel.Cookies.Subscribe((value) => Cookies.Value = value).AddTo(_disposable);
    }
    
    public void ClickCookie()
    {
        _cookiesModel.Cookies.Value++;
    }
}
