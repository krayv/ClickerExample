using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Zenject;
using UnityEngine.UI;
using TMPro;

public class Notification : MonoBehaviour
{
    private AchievementsModel _achievementsModel;
    private IGameProgressLoader _gameProgressLoader;
    private CompositeDisposable _disposables = new();

    [SerializeField] private Button _closeButton;
    [SerializeField] private TextMeshProUGUI _achieveNameText;
    [SerializeField] private Image _achieveIcon;

    private GameProgress _gameProgress => _gameProgressLoader.GameProgress.Value;

    [Inject]
    private void Construct(AchievementsModel achievementsModel, IGameProgressLoader gameProgressLoader)
    {
        _achievementsModel = achievementsModel;
        _gameProgressLoader = gameProgressLoader;
        _achievementsModel.Achievements.ObserveReplace().Where(_ => _.NewValue && !_gameProgress.AchievedAchievements[_.Key]).Subscribe(OnNewAchieve).AddTo(_disposables);
        _closeButton.OnClickAsObservable().Subscribe(_ => CloseNotification()).AddTo(_disposables);
    }

    private void OnNewAchieve(DictionaryReplaceEvent<Achievement, bool> replaceEvent)
    {
        _achieveIcon.sprite = replaceEvent.Key.Icon;
        _achieveNameText.text = replaceEvent.Key.Name;
        gameObject.SetActive(true);
    }

    private void CloseNotification()
    {
        gameObject.SetActive(false);
    }
}
