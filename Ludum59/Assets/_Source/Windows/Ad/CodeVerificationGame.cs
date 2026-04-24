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
    
    [Header("Notification")]
    [SerializeField] private GameObject codeNotificationPrefab;
    private Transform _notificationParent;
    
    private OverloadSystem _overload;
    private string _currentCode;
    private GameObject _currentNotification;

    private void Awake()
    {
        _notificationParent = gameObject.transform.parent;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
        {
            CheckCode();
        }
    }

    public void Init(OverloadSystem overloadSystem)
    {
        _overload = overloadSystem;
        
        _currentCode = Random.Range(1000, 9999).ToString();
        
        ShowCodeNotification();
        
        submitButton.onClick.AddListener(CheckCode);
    }

    private void ShowCodeNotification()
    {
        _currentNotification = Instantiate(codeNotificationPrefab, _notificationParent);
        
        var rect = _currentNotification.GetComponent<RectTransform>();
        if (rect != null)
        {
            //rect.sizeDelta = new Vector2(600, 400);
            
            var parentRect = _notificationParent.GetComponent<RectTransform>().rect;
            rect.anchoredPosition = new Vector2(
                Random.Range(-parentRect.width / 2 + 200, parentRect.width / 2 - 200),
                Random.Range(-parentRect.height / 2 + 150, parentRect.height / 2 - 150)
            );
        }
        
        var notif = _currentNotification.GetComponent<CodeNotification>();
        notif.Setup(_currentCode, _overload);
    }
    
    private void CheckCode()
    {
        if (codeInput.text == _currentCode)
        {
            _overload?.Remove(5);
            if (_currentNotification != null)
                Destroy(_currentNotification);
            Destroy(gameObject);
        }
        else
        {
            codeInput.text = "";
        }
    }
}