using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class RotateImageCaptcha : MonoBehaviour
{
  [SerializeField] private Button closeButton;
  [SerializeField] private Button submitButton;
  [SerializeField] private Button rotateLeftBtn;
  [SerializeField] private Button rotateRightBtn;
  [SerializeField] private Image rotateImage;
  [SerializeField] private TextMeshProUGUI resultText;
  [SerializeField] private List<Sprite> availableImages;
    
  private OverloadSystem overload;
  private int correctRotation;
  private int currentRotation;
    
  public void Init(OverloadSystem overloadSystem)
  {
    overload = overloadSystem;

    rotateImage.sprite = availableImages[Random.Range(0, availableImages.Count)];

    currentRotation = Random.Range(0, 4) * 90;
    correctRotation = 0;
    rotateImage.transform.rotation = Quaternion.Euler(0, 0, currentRotation);
        
    rotateLeftBtn.onClick.AddListener(() => Rotate(-90));
    rotateRightBtn.onClick.AddListener(() => Rotate(90));
    submitButton.onClick.AddListener(Check);
    closeButton.onClick.AddListener(Close);
    closeButton.interactable = false;
  }
    
  private void Rotate(int delta)
  {
    currentRotation = (currentRotation + delta) % 360;
    rotateImage.transform.rotation = Quaternion.Euler(0, 0, currentRotation);
  }
    
  private void Check()
  {
    if (Mathf.Abs(currentRotation - correctRotation) < 1f)
    {
      resultText.text = "Right!";
      closeButton.interactable = true;
    }
    else
    {
      resultText.text = "Wrong!";
    }
  }
    
  private void Close()
  {
    Destroy(gameObject);
    overload.Remove(5);
  }
}