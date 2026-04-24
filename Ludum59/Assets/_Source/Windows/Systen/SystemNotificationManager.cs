using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class SystemNotificationManager : MonoBehaviour
{
    [Header("Префабы")]
    [SerializeField] private GameObject updateNotificationPrefab;
    [SerializeField] private GameObject memoryNotificationPrefab;
    
    [Header("Родители")]
    [SerializeField] private Transform notificationParent;
    [SerializeField] private Transform windowsParent;
    
    [Header("Настройки")]
    [SerializeField] private float memoryThreshold = 70f;
    [SerializeField] private GameObject updateWindowPrefab;
    
    [Header("Спавн обновлений")]
    [SerializeField] private float minUpdateInterval = 20f;
    [SerializeField] private float maxUpdateInterval = 45f;
    
    private OverloadSystem _overload;
    private NotesManager _notesManager;
    private bool _hasPendingUpdate;
    
    public void Initialize(OverloadSystem overloadSystem, NotesManager notesMgr) 
    {
        _overload = overloadSystem;
        _notesManager = notesMgr;
        _overload.OnChanged += CheckMemoryWarning;
        
        StartCoroutine(RandomUpdateSpawner());
    }
    
    private IEnumerator RandomUpdateSpawner()
    {
        while (true)
        {
            float waitTime = Random.Range(minUpdateInterval, maxUpdateInterval);
            yield return new WaitForSeconds(waitTime);
            ShowUpdateNotification();
        }
    }
    
    public void ShowUpdateNotification()
    {
        if (_hasPendingUpdate) return;
        
        var go = Instantiate(updateNotificationPrefab, notificationParent);
        var notif = go.GetComponent<UpdateNotification>();
        notif.Setup(this, _overload);
    }
    
    private void CheckMemoryWarning(int currentOverload)
    {
        if (currentOverload >= memoryThreshold && currentOverload < memoryThreshold + 5)
        {
            var go = Instantiate(memoryNotificationPrefab, notificationParent);
            var notif = go.GetComponent<MemoryNotification>();
            notif.Setup(_overload);
        }
    }
    
    public void HandleUpdate(bool startNow)
    {
        if (startNow)
        {
            var window = Instantiate(updateWindowPrefab, windowsParent);
            var updateWin = window.GetComponent<SystemUpdateWindow>();
            updateWin.Init(_overload, _notesManager);
            
            _hasPendingUpdate = false;
        }
        else
        {
            _hasPendingUpdate = true;
            StartCoroutine(ReturnUpdateLater());
        }
    }
    
    private IEnumerator ReturnUpdateLater()
    {
        yield return new WaitForSeconds(15f);
        _hasPendingUpdate = false;
        ShowUpdateNotification();
    }
}