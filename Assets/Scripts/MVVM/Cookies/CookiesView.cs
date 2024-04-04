using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using TMPro;
using Zenject;
using System.Numerics;
using DG.Tweening;
using System;

public class CookiesView : View
{

    private CookiesViewModel _cookiesViewModel;
    private IGameProgressSaver _gameProgressSaver;

    [SerializeField] private TextMeshProUGUI _cookiesCountText;
    [SerializeField] private TextMeshProUGUI _cpsText;
    [SerializeField] private Button _cookieButton;
    [SerializeField] private Button _openBuildingsButton;
    [SerializeField] private Button _openAchievementsButton;
    [SerializeField] private Button _openUpgradesButton;
    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _resetButton;
    [SerializeField] private Button _infoButton;

    [SerializeField] private ClickAnimationSettings clickAnimationSettings;

    private readonly CompositeDisposable _disposable = new();

    private Sequence _clickSequence;

    [Inject]
    private void Construct(CookiesViewModel cookiesViewModel, IGameProgressSaver gameProgressSaver)
    {
        _cookiesViewModel = cookiesViewModel;
        _gameProgressSaver = gameProgressSaver;

        _cookiesViewModel.Cookies.Subscribe(_ => _cookiesCountText.text = _.ToString()).AddTo(_disposable);
        _cookieButton.OnClickAsObservable().Subscribe(_ => OnClickCookie()).AddTo(_disposable);
        _openBuildingsButton.OnClickAsObservable().Subscribe(_ => _cookiesViewModel.SwitchBuildingsView());
        _openAchievementsButton.OnClickAsObservable().Subscribe(_ => _cookiesViewModel.SwitchAchievementsView());
        _openUpgradesButton.OnClickAsObservable().Subscribe(_ => _cookiesViewModel.SwitchUpgradesButton());
        _cookiesViewModel.CookiesPerSecond.Subscribe(UpdateCpS).AddTo(_disposable);
        _saveButton.OnClickAsObservable().Subscribe(_ => _gameProgressSaver.SaveGame());
        _resetButton.OnClickAsObservable().Subscribe(_ => _gameProgressSaver.ResetGameProgress());
        _infoButton.OnClickAsObservable().Subscribe(_ => _cookiesViewModel.SwitchInfoView());

        _clickSequence = DOTween.Sequence();
        _clickSequence.Append(_cookieButton.transform.DOScale(clickAnimationSettings.TargetScale, clickAnimationSettings.AnimationTime));
        _clickSequence.Append(_cookieButton.transform.DOScale(1f, clickAnimationSettings.RevertAnimationTime));
        _clickSequence.OnComplete(() => _clickSequence.Rewind());
    }

    private void Update()
    {
        _cookiesViewModel.ProduceCookies(Time.deltaTime);
    }

    private void OnClickCookie()
    {
        _cookiesViewModel.ClickCookie();
        if(_clickSequence.IsPlaying())
        { 
            _clickSequence.Rewind(); 
        }    
        _clickSequence.Play();  
    }

    private void OnDestroy()
    {
        _disposable.Clear();
    }

    private void UpdateCpS(BigInteger value)
    {
        _cpsText.text = value.ToString(); 
    }

    [Serializable]
    public struct ClickAnimationSettings
    {
        public float TargetScale;
        public float AnimationTime;
        public float RevertAnimationTime;
    }
}
