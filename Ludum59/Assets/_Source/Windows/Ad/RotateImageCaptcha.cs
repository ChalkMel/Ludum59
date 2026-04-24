using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class RotateImageCaptcha : MonoBehaviour
{
  [SerializeField] private Button closeButton;
  [SerializeField] private Button submitButton;
  [SerializeField] private Button rotateLeftBtn;
  [SerializeField] private Button rotateRightBtn;
  [SerializeField] private Image rotateImage;
  [SerializeField] private List<Sprite> availableImages;
    
  private OverloadSystem _overload;
  private int _correctRotation;
  private int _currentRotation;
    
  public void Init(OverloadSystem overloadSystem)
  {
    _overload = overloadSystem;

    rotateImage.sprite = availableImages[Random.Range(0, availableImages.Count)];

    _currentRotation = Random.Range(1, 4) * 90;
    _correctRotation = 0;
    rotateImage.transform.rotation = Quaternion.Euler(0, 0, _currentRotation);
        
    rotateLeftBtn.onClick.AddListener(() => Rotate(-90));
    rotateRightBtn.onClick.AddListener(() => Rotate(90));
    submitButton.onClick.AddListener(Check);
    closeButton.onClick.AddListener(Close);
    closeButton.interactable = false;
  }
    
  private void Rotate(int delta)
  {
    _currentRotation = (_currentRotation + delta) % 360;
    rotateImage.transform.rotation = Quaternion.Euler(0, 0, _currentRotation);
  }
    
  private void Check()
  {
    if (Mathf.Abs(_currentRotation - _correctRotation) < 1f)
    {
      Close();
      closeButton.interactable = true;
    }
  }
    
  private void Close()
  {
    Destroy(gameObject);
    _overload.Remove(5);
  }
}