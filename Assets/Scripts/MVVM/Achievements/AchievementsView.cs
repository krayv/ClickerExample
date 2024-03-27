using System.Collections;
using UnityEngine;
using Zenject;
using UnityEngine.UI;
using UniRx;

public class AchievementsView : View
{
    private AchievementsViewModel _achievementsViewModel;
    private AchievementsUIItemFactory _achievementsUIItemFactory;

    [SerializeField] private Transform container;
    [SerializeField] protected Button closeButton;

    [Inject]
    private void Construct(AchievementsViewModel achievementsViewModel, AchievementsUIItemFactory achievementsUIItemFactory)
    {
        _achievementsViewModel = achievementsViewModel;
        _achievementsUIItemFactory = achievementsUIItemFactory;
        _achievementsUIItemFactory.InstantiateItems(container);
        closeButton.OnClickAsObservable().Subscribe(_ => CloseView());
    }  
}
