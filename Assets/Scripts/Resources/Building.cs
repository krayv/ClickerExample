using UnityEngine;

[CreateAssetMenu(fileName = nameof(Building), menuName = "ScriptableObjects/" + nameof(Building))]
public class Building : ScriptableObject
{
    public string Name;
    public int ID;
    public long BasePrice;
    public long BaseProduction;
}
