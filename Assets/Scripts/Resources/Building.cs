using UnityEngine;

[CreateAssetMenu(fileName = nameof(Building), menuName = "ScriptableObjects/" + nameof(Building))]
public class Building : BuyableItem
{
    public long BaseProduction;
}
