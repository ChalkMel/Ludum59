using System;
using UnityEngine;

public class OverloadSystem : MonoBehaviour
{
  private int Current { get; set; }
  private int Max => 100;
  public event Action<int> OnChanged;
  public event Action OnGameOver;
 
  public void Add(int value)
  {
    Current = Math.Min(Current + value, Max);
    OnChanged?.Invoke(Current);
        
    if (Current >= Max)
      OnGameOver?.Invoke();
  }

  public void Remove(int value)
  {
    Current = Math.Max(Current - value, 0);
    OnChanged?.Invoke(Current);
  }
}