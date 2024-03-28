using System.Collections;
using System.Collections.Generic;

public struct GameProgress
{
    public Dictionary<Building, int> buyedBuildings;
    public Dictionary<Achievement, bool> achievedAchievements;
    public Dictionary<GameUpgrade, bool> buyedUpgrades;
}
