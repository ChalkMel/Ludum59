using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PasswordWindow : BaseWindow
{
  [SerializeField] private TMP_InputField input;
  [SerializeField] private Button submitBtn;
  [SerializeField] private TextMeshProUGUI feedback;
  [SerializeField] private string correctPassword;
    
  private void Start()
  {
    closeBtn.interactable = false;
    submitBtn.onClick.AddListener(Check);
  }
    
  private void Check()
  {
    if (input.text == correctPassword)
    {
      feedback.text = "Right!";
      closeBtn.interactable = true;
      overload?.Add(-5);
    }
    else
    {
      feedback.text = "Wrong!";
      overload?.Add(10);
    }
  }
}