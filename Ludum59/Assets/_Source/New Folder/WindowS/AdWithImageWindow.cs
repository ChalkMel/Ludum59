using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AdWithImageWindow : BaseWindow
{
  [SerializeField] private Image adImage;
  [SerializeField] private TextMeshProUGUI adText;
  [SerializeField] private Button actionBtn;
  [SerializeField] private TextMeshProUGUI actionBtnText;
    
  [SerializeField] private Sprite image;
  [SerializeField] private string text;
  [SerializeField] private string buttonText;
  [SerializeField] private int overloadOnClick = 10;
    
  private void Start()
  {
    adImage.sprite = image;
    adText.text = text;
    actionBtnText.text = buttonText;
        
    actionBtn.onClick.AddListener(() => {
      overload?.Add(overloadOnClick);
      //TODO Мини-игра
      Close();
    });
  }
}