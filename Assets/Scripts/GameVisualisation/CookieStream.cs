using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UniRx;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(ParticleSystem))]
public class CookieStream : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    private CookieProductionModel _cookieProductionModel;
    private CookiesViewModel _cookieViewModel;
    private CompositeDisposable _disposables = new CompositeDisposable();

    [SerializeField] private int _maxEmissionRate = 1000;
    [Inject]
    private void Construct(CookieProductionModel cookieProductionModel, CookiesViewModel cookiesViewModel)
    {
        _cookieProductionModel = cookieProductionModel;
        _cookieViewModel = cookiesViewModel;
        _cookieProductionModel.CookiesPerSecond.AsObservable().Subscribe(OnUpdateProduction).AddTo(_disposables);
        _cookieViewModel.OnClickCookie += OnClick;
    }

    private void OnValidate()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    private void OnUpdateProduction(BigInteger value)
    {
        var emission = _particleSystem.emission;
        emission.rateOverTime = Mathf.Clamp((int)value, 0, _maxEmissionRate);
    }

    private void OnClick(BigInteger value)
    {
        _particleSystem.Emit(Mathf.Clamp((int)value, 0, _maxEmissionRate));
    }
}
