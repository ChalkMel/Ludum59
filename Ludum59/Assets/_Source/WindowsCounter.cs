using System;
using UnityEngine;

public class WindowsCounter : MonoBehaviour
{
    [SerializeField] private static int _closedCount;
    public static Action ChangeCounter;

    private void Start()
    {
        
    }

    public static void AddCount(int value)
    {
        _closedCount += value;
        ChangeCounter?.Invoke();
    }
    public static int GetCount()
    {
        return _closedCount;
    }
}
