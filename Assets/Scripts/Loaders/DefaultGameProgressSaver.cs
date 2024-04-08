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
    private GameStatisticModel _gameStatisticModel;
    private IGameProgressLoader _gameProgressLoader;

    [Inject]
    private void Construct(BuildingsModel buildingsModel, AchievementsModel achievementsModel, CookiesModel cookiesModel, UpgradesModel upgradesModel, GameStatisticModel gameStatisticModel, IGameProgressLoader gameProgressLoader)
    {
        _buildingsModel = buildingsModel;
        _achievementsModel = achievementsModel;
        _cookiesModel = cookiesModel;
        _upgradesModel = upgradesModel;
        _gameProgressLoader = gameProgressLoader;
        _gameStatisticModel = gameStatisticModel;
    }

    public void SaveGame()
    {
        GameProgressJSONDataFormat data = new GameProgressJSONDataFormat(_buildingsModel.Buildings, _upgradesModel.Upgrades,
            _achievementsModel.Achievements, _cookiesModel.Cookies.Value, _gameStatisticModel.CookiesBaked.Value, _gameStatisticModel.CookiesClicked.Value);

        string serializedData = JsonConvert.SerializeObject(data);
#if UNITY_WEBGL
        PlayerPrefs.SetString("Data", serializedData);
        Debug.Log("Game Saved in PP: " + serializedData);
#else
        File.WriteAllText(Application.persistentDataPath + "saveFile.json", serializedData);
        Debug.Log("Game Saved: " + serializedData);
#endif

    }

    public void ResetGameProgress()
    {
#if UNITY_WEBGL
        PlayerPrefs.DeleteKey("Data");
#else
        File.Delete(Application.persistentDataPath + "saveFile.json");
#endif       
        Debug.Log("The Game Save deleted!");
        _gameProgressLoader.LoadProgressData();
    }
}
