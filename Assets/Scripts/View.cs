using UnityEngine;
using System;

[Serializable]
public abstract class View : MonoBehaviour
{
    public virtual void OpenView()
    {
        gameObject.SetActive(true);
    }

    public virtual void CloseView()
    {
        gameObject.SetActive(false);
    }

    public virtual void SwitchView()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
