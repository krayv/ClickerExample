using System.Collections;
using UnityEngine;
using Zenject;
using System.Numerics;
using UniRx;

[CreateAssetMenu(fileName = nameof(HaveBuildingAchievement), menuName = "ScriptableObjects/" + nameof(HaveBuildingAchievement))]
public class HaveBuildingAchievement : Achievement
{
    public Building Building;
    public int CountRequire;

    private BuildingsModel _buildingsModel;
    private readonly CompositeDisposable _disposable = new();

    [Inject]
    private void Construct(BuildingsModel buildingsModel)
    {
        _buildingsModel = buildingsModel;
    }

    public override void StartObserve()
    {
        _buildingsModel.Buildings.ObserveReplace().Where(_ => _.Key == Building).Subscribe(OnBuildingCountChanged).AddTo(_disposable);
    }

    private void OnBuildingCountChanged(DictionaryReplaceEvent<Building, int> replaceEvent)
    {
        if (replaceEvent.NewValue >= CountRequire && !IsAchievementReceived())
        {
            _achievementsModel.Achievements[this] = true;
        }
    }
}
