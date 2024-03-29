using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Zenject;

public class AchievementsModel
{
    public ReactiveDictionary<Achievement, bool> Achievements { get; private set; }

    private readonly CompositeDisposable _disposable = new();

    private IGameProgressLoader _loader;

    [Inject]
    private void Construct(IGameProgressLoader loader)
    {
        _loader = loader;
        loader.GameProgress.Subscribe(SetData).AddTo(_disposable);
    }

    private void SetData(GameProgress gameProgress)
    {
        Achievements = gameProgress.AchievedAchievements.ToReactiveDictionary();
    }
}
