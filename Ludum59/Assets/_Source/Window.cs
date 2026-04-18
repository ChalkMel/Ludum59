using System;
using UnityEngine;
using UnityEngine.UI;
public abstract class Window : MonoBehaviour
{
    protected Button _closeBtn;
    protected bool _isUnlocked;

    public virtual void Awake()
    {
        _closeBtn = transform.GetChild(0).GetComponent<Button>();
    }

    public virtual void Close()
    {
        WindowsCounter.AddCount(1);
        Destroy(gameObject);
    }
}
