using System.Collections;
using UnityEngine;
using Zenject;

public class AchievementsView : View
{
    private AchievementsViewModel _achievementsViewModel;
    private AchievementsUIItemFactory _achievementsUIItemFactory;

    [SerializeField] private Transform container;

    [Inject]
    private void Construct(AchievementsViewModel achievementsViewModel, AchievementsUIItemFactory achievementsUIItemFactory)
    {
        _achievementsViewModel = achievementsViewModel;
        _achievementsUIItemFactory = achievementsUIItemFactory;
        _achievementsUIItemFactory.InstantiateItems(container);
    }  
}
