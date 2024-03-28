using System.Collections;
using UnityEngine;
using Zenject;

public class DefaultGameProgressSaver : IGameProgressSaver
{
    private BuildingsModel _buildingsModel;
    private AchievementsModel _achievementsModel;
    private CookiesModel _cookiesModel;
    private UpgradesModel _upgradesModel;

    [Inject]
    private void Construct(BuildingsModel buildingsModel, AchievementsModel achievementsModel, CookiesModel cookiesModel, UpgradesModel upgradesModel)
    {
        _buildingsModel = buildingsModel;
        _achievementsModel = achievementsModel;
        _cookiesModel = cookiesModel;
        _upgradesModel = upgradesModel;
    }

    public void SaveGame()
    {
        throw new System.NotImplementedException();
    }
}
