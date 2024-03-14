using UnityEngine;

[CreateAssetMenu(fileName = nameof(Building), menuName = "ScriptableObjects/" + nameof(Building))]
public class Building : Item
{
    public long BasePrice;
    public long BaseProduction;
}
