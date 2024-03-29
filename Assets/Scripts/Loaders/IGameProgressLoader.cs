using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public interface IGameProgressLoader
{
    public ReactiveProperty<GameProgress> GameProgress { get; }

    public void LoadProgressData();
}
