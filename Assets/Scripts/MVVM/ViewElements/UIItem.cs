using UnityEngine;
using UnityEngine.UI;
public abstract class UIItem : MonoBehaviour
{
    [SerializeField] protected Image icon;

    public abstract void SetupUIItem(Item item);
}
