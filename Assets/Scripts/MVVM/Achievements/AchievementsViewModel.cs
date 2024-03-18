using Zenject;
using UniRx;

public class AchievementsViewModel 
{
    private AchievementsModel _achievementsModel;

    public ReactiveDictionary<Achievement, bool> Achievements => _achievementsModel.Achievements;

    [Inject]
    private void Construct(AchievementsModel achievementsModel)
    {
        _achievementsModel = achievementsModel;
    }
}
