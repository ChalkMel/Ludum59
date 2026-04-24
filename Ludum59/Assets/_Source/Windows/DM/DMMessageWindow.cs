using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DMChatWindow : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image avatar;
    [SerializeField] private TextMeshProUGUI senderText;
    [SerializeField] private Transform messagesContainer;
    [SerializeField] private GameObject messagePrefab;
    [SerializeField] private Button calmButton;
    [SerializeField] private Button annoyedButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private ScrollRect scrollRect;
    
    private DMNotificationManager.DMCharacter _character;
    private DMNotificationManager.DMMessage _message;
    private OverloadSystem _overload;
    private DMNotificationManager _manager;
    
    private bool _waitingForPlayerResponse = false;
    private bool _isCalmChosen = false;
    private bool _isDialogComplete = false;
    private bool _isLastDialog = false;
    
    public void StartChat(DMNotificationManager.DMCharacter chr,
                          DMNotificationManager.DMMessage msg,
                          OverloadSystem ovld,
                          DMNotificationManager mgr,
                          bool lastDialog)
    {
        _character = chr;
        _message = msg;
        _overload = ovld;
        _manager = mgr;
        _isLastDialog = lastDialog;
        
        avatar.sprite = chr.avatar;
        senderText.text = chr.characterName;
        
        AddMessage(_message.firstMessage, false);
        
        calmButton.onClick.AddListener(() => OnPlayerResponse(true));
        annoyedButton.onClick.AddListener(() => OnPlayerResponse(false));
        closeButton.onClick.AddListener(Close);
        
        calmButton.interactable = true;
        annoyedButton.interactable = true;
        _waitingForPlayerResponse = true;
    }
    
    private void AddMessage(Sprite image, bool isPlayer)
    {
        var msgObj = Instantiate(messagePrefab, messagesContainer);
        var msgImage = msgObj.GetComponent<Image>();
        msgImage.sprite = image;
        
        var rect = msgObj.GetComponent<RectTransform>();
        if (isPlayer)
        {
            rect.anchorMin = new Vector2(1, 0);
            rect.anchorMax = new Vector2(1, 0);
            rect.pivot = new Vector2(1, 0);
            rect.anchoredPosition = new Vector2(-20, 0);
        }
        else
        {
            rect.anchorMin = new Vector2(0, 0);
            rect.anchorMax = new Vector2(0, 0);
            rect.pivot = new Vector2(0, 0);
            rect.anchoredPosition = new Vector2(20, 0);
        }
        
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0;
    }
    
    private void OnPlayerResponse(bool isCalm)
    {
        if (!_waitingForPlayerResponse) return;
        
        _waitingForPlayerResponse = false;
        _isCalmChosen = isCalm;
        
        calmButton.interactable = false;
        annoyedButton.interactable = false;
        
        Sprite playerResponse = isCalm ? _message.playerCalmResponse : _message.playerAnnoyedResponse;
        AddMessage(playerResponse, true);
        
        StartCoroutine(ShowCharacterReplyAfterDelay(1f));
    }
    
    private IEnumerator ShowCharacterReplyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Sprite characterReply = _isCalmChosen ? _message.characterCalmReply : _message.characterAnnoyedReply;
        AddMessage(characterReply, false);
        calmButton.gameObject.SetActive(false);
        annoyedButton.gameObject.SetActive(false);
        _isDialogComplete = true;
    }
    
    private void Close()
    {
        _overload?.Add(-5);
        _manager.OnDialogClosed(_isLastDialog);
        Destroy(gameObject);
    }
}