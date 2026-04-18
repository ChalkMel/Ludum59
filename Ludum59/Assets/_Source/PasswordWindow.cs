using UnityEngine;
using UnityEngine.UI;
using TMPro;  
namespace _Source
{
  public class PasswordWindow : Window
  {
    [SerializeField] private TMP_InputField _input;
    [SerializeField] private Button _inputBtn;
    [SerializeField] private string _password;
    public override void Awake()
    {
      base.Awake();
      _inputBtn.onClick.AddListener(TryOpen);
      _closeBtn.interactable = false;
    }

    public void TryOpen()
    {
      if (_input.text.Equals(_password))
      {
        _isUnlocked = true;
        _closeBtn.interactable = true;
        _closeBtn.onClick.AddListener(Close);
      }
    }
  }
}