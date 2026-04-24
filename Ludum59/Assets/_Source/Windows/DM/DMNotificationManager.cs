using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DMNotificationManager : MonoBehaviour
{
    [Header("Префабы")]
    [SerializeField] private GameObject notificationPrefab;
    [SerializeField] private GameObject chatWindowPrefab;
    
    [Header("Родители")]
    [SerializeField] private Transform notificationParent;
    [SerializeField] private Transform windowParent;
    
    [Header("Интервалы")]
    [SerializeField] private float firstInterval = 5f;
    [SerializeField] private float nextInterval = 30f;
    [SerializeField] private float endGameDelay = 2f;
    
    [Header("Персонажи (3 ЛС)")]
    [SerializeField] private List<DMCharacter> characters;
    
    [Header("Game Manager")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private ColorChange colorChange;
    private OverloadSystem overload;
    private bool waitingForDialog = false;
    private bool waitingForLastClose = false;
    
    [System.Serializable]
    public class DMCharacter
    {
        public string characterName;
        public Sprite avatar;
        public DMMessage message;
    }
    
    [System.Serializable]
    public class DMMessage
    {
        public Sprite notificationImage;
        public Sprite firstMessage;
        public Sprite playerCalmResponse;
        public Sprite playerAnnoyedResponse;
        public Sprite characterCalmReply;
        public Sprite characterAnnoyedReply;
    }
    
    public void StartSpawning(OverloadSystem overloadSystem)
    {
        overload = overloadSystem;
        StartCoroutine(SpawnRoutine());
    }
    
    private int currentDialogIndex;

    private IEnumerator SpawnRoutine()
    {
    
        for (int i = 0; i < characters.Count; i++)
        {
            currentDialogIndex = i;
            var character = characters[i];
        
            ShowNotification(character, character.message);
            
            yield return new WaitUntil(() => waitingForDialog);
            yield return new WaitWhile(() => waitingForDialog);

            if (i == 1)
            {
                colorChange.Change();
            }
            
            if (i < characters.Count - 1)
            {
                float waitTime = (i == 0) ? firstInterval : nextInterval;
                yield return new WaitForSeconds(waitTime);
            }
        }
        yield return new WaitForSeconds(endGameDelay);
        gameManager?.EndGame();
    }
    
    private void ShowNotification(DMCharacter character, DMMessage message)
    {
        var go = Instantiate(notificationPrefab, notificationParent);
        var notif = go.GetComponent<DMNotification>();
        notif.Setup(character, message, this, overload);
        overload?.Add(5);
    }
    
    public void OpenChat(DMCharacter character, DMMessage message)
    {
        var go = Instantiate(chatWindowPrefab, windowParent);
        var chat = go.GetComponent<DMChatWindow>();
        bool isLast = (currentDialogIndex == characters.Count - 1);
        chat.StartChat(character, message, overload, this, isLast);
    
        waitingForDialog = true;
    }
    
    public void OnDialogClosed(bool isLast)
    {
        waitingForDialog = false;
        if (isLast)
        {
            waitingForLastClose = false;
        }
    }
}