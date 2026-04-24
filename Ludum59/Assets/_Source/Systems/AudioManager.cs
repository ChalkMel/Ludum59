using System;
using UnityEngine;

namespace _Source
{
  [RequireComponent(typeof(AudioSource))]
  public class AudioManager : MonoBehaviour
  {
    [SerializeField] private AudioClip mouse;
    [SerializeField] private AudioClip key;
    AudioSource audioSource;
  
    private void Awake()
    {
      audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
      if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) && !audioSource.isPlaying)
      {
        audioSource.clip = mouse;
        audioSource.Play();
      }

      if (Input.anyKeyDown && !Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1) && !audioSource.isPlaying)
      {
        audioSource.clip = key;
        audioSource.Play();
      }
    }
  }
}