using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public abstract class View : MonoBehaviour
{
    public List<MonoBehaviour> ObjectsForInject = new List<MonoBehaviour>();

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
