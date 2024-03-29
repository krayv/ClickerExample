using System.Collections;
using UnityEngine;
using Zenject;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

public class DefaultGameProgressSaver : IGameProgressSaver
{
    private BuildingsModel _buildingsModel;
    private AchievementsModel _achievementsModel;
    private CookiesModel _cookiesModel;
    private UpgradesModel _upgradesModel;
    private IGameProgressLoader _gameProgressLoader;

    [Inject]
    private void Construct(BuildingsModel buildingsModel, AchievementsModel achievementsModel, CookiesModel cookiesModel, UpgradesModel upgradesModel, IGameProgressLoader gameProgressLoader)
    {
        _buildingsModel = buildingsModel;
        _achievementsModel = achievementsModel;
        _cookiesModel = cookiesModel;
        _upgradesModel = upgradesModel;
        _gameProgressLoader = gameProgressLoader;
    }

    public void SaveGame()
    {
        GameProgressJSONDataFormat data = new GameProgressJSONDataFormat(_buildingsModel.Buildings, _upgradesModel.Upgrades, _achievementsModel.Achievements, _cookiesModel.Cookies.Value);
        string serializedData = JsonConvert.SerializeObject(data);
        File.WriteAllText(Application.dataPath + "saveFile.json", serializedData);
        Debug.Log("Game Saved: " + serializedData);
    }

    public void ResetGameProgress()
    {
        File.Delete(Application.dataPath + "saveFile.json");
        Debug.Log("The Game Save deleted!");
        _gameProgressLoader.LoadProgressData();
    }
}
