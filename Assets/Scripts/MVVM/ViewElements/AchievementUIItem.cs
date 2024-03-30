using System.Collections;
using UnityEngine;
using UniRx;
using Zenject;
using UnityEngine.UI;

public class AchievementUIItem : UIItem
{
    [SerializeField] private Image inactiveIcon;

    private AchievementsViewModel _achievementsViewModel;

    private Achievement _achievement;

    public override Item Item => _achievement;

    [Inject]
    private void Construct(AchievementsViewModel achievementsViewModel)
    {
        _achievementsViewModel = achievementsViewModel;
    }

    public override void SetupUIItem(Item item)
    {
        if(item is Achievement achievement)
        {
            _achievement = achievement;
            _achievementsViewModel.Achievements.ObserveReplace().Where(_ => _.Key == _achievement).Subscribe(OnGetAchievement).AddTo(_disposable);
            SetImageByAchievedStatus(_achievementsViewModel.Achievements[achievement]);                      
        }
    }

    private void OnGetAchievement(DictionaryReplaceEvent<Achievement, bool> newAchievement)
    {
        SetImageByAchievedStatus(newAchievement.NewValue);
    }

    private void SetImageByAchievedStatus(bool achieved)
    {
        icon.gameObject.SetActive(achieved);
        inactiveIcon.gameObject.SetActive(!achieved);      
    }
}
