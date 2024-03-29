using System.Collections;
using UnityEngine;
using Zenject;
using System.Numerics;
using UniRx;

[CreateAssetMenu(fileName = nameof(HaveCookieAchievement), menuName = "ScriptableObjects/" + nameof(HaveCookieAchievement))]
public class HaveCookieAchievement : Achievement
{
    public int CookieRequirement;

    private CookiesModel _cookiesViewModel;
    private readonly CompositeDisposable _disposable = new();

    [Inject] 
    private void Construct(CookiesModel cookiesViewModel, AchievementsModel achievementsModel)
    {
        _cookiesViewModel = cookiesViewModel;
        _achievementsModel = achievementsModel;
        _cookiesViewModel.Cookies.Subscribe(OnCookiesValueChanged).AddTo(_disposable);
    }

    private void OnCookiesValueChanged(BigInteger value)
    {
        if (!IsAchievementReceived() && value >= CookieRequirement)
        {
            _achievementsModel.Achievements[this] = true;
        }
    }
}
