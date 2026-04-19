using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CallWindow : BaseWindow
{
  [Header("UI")]
  [SerializeField] private TextMeshProUGUI callerNameText;
  [SerializeField] private Button acceptButton;
  [SerializeField] private Button declineButton;
    
  [Header("Мини-игра")]
  [SerializeField] private GameObject signalGamePrefab;
    
  private void Start()
  {
    acceptButton.onClick.AddListener(AcceptCall);
    declineButton.onClick.AddListener(DeclineCall);
  }
    
  private void AcceptCall()
  {
    overload.Remove(5);
    Destroy(gameObject);
  }
    
  private void DeclineCall()
  {
    Destroy(gameObject);
    overload.Remove(5);
  }
}