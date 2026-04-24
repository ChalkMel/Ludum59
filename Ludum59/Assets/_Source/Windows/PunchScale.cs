using System;
using UnityEngine;
using DG.Tweening;
public class PunchScale : MonoBehaviour
{
    private const float duration = 0.2f;
    private Vector3 _scale = new Vector3(0.1f, 0.1f, 0);
    private void Awake()
    {
        gameObject.transform.DOPunchScale(_scale, duration, 1);
    }
}
