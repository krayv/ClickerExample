using System.Collections;
using UnityEngine;
using Zenject;
using System.IO;
using Newtonsoft.Json;
using UniRx;

public class DefaultGameProgressLoader : IGameProgressLoader
{
    private IResourceLoader _resourceLoader;

    public ReactiveProperty<GameProgress> _gameProgress;

    public ReactiveProperty<GameProgress> GameProgress => _gameProgress;

    [Inject]
    private void Construct(IResourceLoader resourceLoader)
    {
        _resourceLoader = resourceLoader;
        _gameProgress = new ReactiveProperty<GameProgress>();
        LoadProgressData();
    }

    public void LoadProgressData()
    {
        try
        {
            string rawData;
#if UNITY_WEBGL
            rawData = PlayerPrefs.GetString("Data");
            if (string.IsNullOrEmpty(rawData))
            {
                Debug.Log("Start a new game");
            }
            else
            {
                Debug.Log("Game data loaded: " + rawData);
            }
#else
            rawData = File.ReadAllText(Application.persistentDataPath + "saveFile.json");
#endif
            GameProgressJSONDataFormat data = JsonConvert.DeserializeObject<GameProgressJSONDataFormat>(rawData);
            _gameProgress.Value = new GameProgress(data, _resourceLoader);
            
        }
        catch
        {
            var data = new GameProgressJSONDataFormat()
            {
                CookiesInBank = 0,
                CookiesClicked = 0,
                CookiesProduced = 0,
                AchievedAchieves = new System.Collections.Generic.Dictionary<int, bool>(),
                PurchasedBuildings = new System.Collections.Generic.Dictionary<int, int>(),
                PurchasedUpgrades = new System.Collections.Generic.Dictionary<int, bool>()
            };
            _gameProgress.Value = new GameProgress(data, _resourceLoader);
        }
    }
}
