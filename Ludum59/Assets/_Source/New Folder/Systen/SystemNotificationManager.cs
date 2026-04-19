using System.Collections;
using UnityEngine;

public class SystemNotificationManager : MonoBehaviour
{
    [Header("Префабы")]
    [SerializeField] private GameObject updateNotificationPrefab;
    [SerializeField] private GameObject malwareNotificationPrefab;
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
    
    private OverloadSystem overload;
    private NotesManager notesManager;  // ← добавить ссылку
    private bool hasPendingUpdate = false;
    
    public void Initialize(OverloadSystem overloadSystem, NotesManager notesMgr)  // ← изменить
    {
        overload = overloadSystem;
        notesManager = notesMgr;  // ← сохранить
        overload.OnChanged += CheckMemoryWarning;
        
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
        if (hasPendingUpdate) return;
        
        var go = Instantiate(updateNotificationPrefab, notificationParent);
        var notif = go.GetComponent<UpdateNotification>();
        notif.Setup(this, overload);
    }
    
    public void ShowMalwareNotification()
    {
        var go = Instantiate(malwareNotificationPrefab, notificationParent);
        var notif = go.GetComponent<MalwareNotification>();
        notif.Setup(overload);
    }
    
    private void CheckMemoryWarning(int currentOverload)
    {
        if (currentOverload >= memoryThreshold && currentOverload < memoryThreshold + 5)
        {
            var go = Instantiate(memoryNotificationPrefab, notificationParent);
            var notif = go.GetComponent<MemoryNotification>();
            notif.Setup(overload);
        }
    }
    
    public void HandleUpdate(bool startNow)
    {
        if (startNow)
        {
            // ← передаём notesManager в окно обновления
            var window = Instantiate(updateWindowPrefab, windowsParent);
            var updateWin = window.GetComponent<SystemUpdateWindow>();
            updateWin.Init(overload, notesManager);  // ← изменить
            
            hasPendingUpdate = false;
        }
        else
        {
            hasPendingUpdate = true;
            StartCoroutine(ReturnUpdateLater());
        }
    }
    
    private IEnumerator ReturnUpdateLater()
    {
        yield return new WaitForSeconds(15f);
        hasPendingUpdate = false;
        ShowUpdateNotification();
    }
}