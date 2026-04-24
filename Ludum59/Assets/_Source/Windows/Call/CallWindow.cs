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
    [SerializeField] private CircleClickGame circleClickGame;
    
    private void Start()
    {
        acceptButton.onClick.AddListener(AcceptCall);
        declineButton.onClick.AddListener(DeclineCall);
        
        if (circleClickGame != null)
            circleClickGame.OnGameFinished += OnGameFinished;
        overload = FindFirstObjectByType<OverloadSystem>();
    }
    
    private void AcceptCall()
    {
        acceptButton.gameObject.SetActive(false);
        declineButton.gameObject.SetActive(false);

        circleClickGame.StartGame();
        
    }
    
    private void OnGameFinished(bool success)
    {
        if (success)
        {
            overload.Remove(5);
        }

        Destroy(gameObject);
    }
    
    private void DeclineCall()
    {
        overload.Remove(5);
        Destroy(gameObject);
    }
    
    private void OnDestroy()
    {
        if (circleClickGame != null)
            circleClickGame.OnGameFinished -= OnGameFinished;
    }
}