using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

public class CodeVerificationGame : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_InputField codeInput;
    [SerializeField] private Button submitButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI resultText;
    
    [Header("Уведомление")]
    [SerializeField] private GameObject codeNotificationPrefab;
    private Transform notificationParent;
    
    private OverloadSystem overload;
    private string currentCode;
    private GameObject currentNotification;


    private void Awake()
    {
        closeButton.onClick.AddListener(Close);
        closeButton.interactable = false;
        notificationParent = gameObject.transform.parent;
    }

    public void Init(OverloadSystem overloadSystem)
    {
        overload = overloadSystem;
        
        currentCode = Random.Range(1000, 9999).ToString();
        
        ShowCodeNotification();
        
        submitButton.onClick.AddListener(CheckCode);
        closeButton.onClick.AddListener(Close);
        
    }
    
    private void ShowCodeNotification()
    {
        currentNotification = Instantiate(codeNotificationPrefab, notificationParent);
        
        var rect = currentNotification.GetComponent<RectTransform>();
        if (rect != null)
        {
            rect.sizeDelta = new Vector2(400, 200);
            
            var parentRect = notificationParent.GetComponent<RectTransform>().rect;
            rect.anchoredPosition = new Vector2(
                Random.Range(-parentRect.width / 2 + 200, parentRect.width / 2 - 200),
                Random.Range(-parentRect.height / 2 + 150, parentRect.height / 2 - 150)
            );
        }
        
        var notif = currentNotification.GetComponent<CodeNotification>();
        notif.Setup(currentCode, overload);
        
        overload?.Add(1);
    }
    
    private void CheckCode()
    {
        if (codeInput.text == currentCode)
        {
           
            resultText.text = "Right!";
            closeButton.interactable = true;
            overload?.Remove(5);
            if (currentNotification != null)
                Destroy(currentNotification);
        }
        else
        {
            resultText.text = "Wrong!";
            codeInput.text = "";
        }
    }
    
    private void Close()
    {
        if (currentNotification != null)
            Destroy(currentNotification);
        
        Destroy(gameObject);
    }
}