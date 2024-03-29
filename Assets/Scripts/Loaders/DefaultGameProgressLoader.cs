using System.Collections;
using UnityEngine;
using Zenject;
using System.IO;
using Newtonsoft.Json;

public class DefaultGameProgressLoader : IGameProgressLoader
{
    private IResourceLoader _resourceLoader;

    private GameProgress _gameProgress;

    [Inject]
    private void Construct(IResourceLoader resourceLoader)
    {
        _resourceLoader = resourceLoader;
        try
        {
            string rawData = File.ReadAllText(Application.dataPath + "saveFile.json");
            GameProgressJSONDataFormat data = JsonConvert.DeserializeObject<GameProgressJSONDataFormat>(rawData);
            _gameProgress = new GameProgress(data, _resourceLoader);
        }
        catch
        {
            var data = new GameProgressJSONDataFormat()
            {
                CookiesInBank = 0,
                AchievedAchieves = new System.Collections.Generic.Dictionary<int, bool>(),
                PurchasedBuildings = new System.Collections.Generic.Dictionary<int, int>(),
                PurchasedUpgrades = new System.Collections.Generic.Dictionary<int, bool>()
            };
            _gameProgress = new GameProgress(data, _resourceLoader);
        }
    }
    public GameProgress GetProgressData()
    {        
        return _gameProgress;
    }
}
