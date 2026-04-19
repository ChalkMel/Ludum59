using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DMNotificationManager : MonoBehaviour
{
  [SerializeField] private GameObject notificationPrefab;
  [SerializeField] private GameObject windowPrefab;
  [SerializeField] private Transform notificationParent;
  [SerializeField] private Transform windowParent;
  [SerializeField] private float minInterval = 10f;
  [SerializeField] private float maxInterval = 25f;
  [SerializeField] private List<DMMessage> messages;
    
  private OverloadSystem overload;
    
  [System.Serializable]
  public class DMMessage
  {
    public string sender;
    public Sprite avatar;
    public string preview;
    [TextArea] public string fullMessage;
    public string option1;
    public string option2;
    public int overloadCost = 5;
  }
    
  public void StartSpawning(OverloadSystem overloadSystem)
  {
    overload = overloadSystem;
    StartCoroutine(SpawnRoutine());
  }
    
  private IEnumerator SpawnRoutine()
  {
    while (true)
    {
      yield return new WaitForSeconds(Random.Range(minInterval, maxInterval));
      var msg = messages[Random.Range(0, messages.Count)];
      ShowNotification(msg);
    }
  }
    
  private void ShowNotification(DMMessage msg)
  {
    var go = Instantiate(notificationPrefab, notificationParent);
    var notif = go.GetComponent<DMNotification>();
    notif.Setup(msg, this, overload);
    overload.Add(5);//TODO
  }
    
  public void OpenWindow(DMMessage msg)
  {
    var go = Instantiate(windowPrefab, windowParent);
    var win = go.GetComponent<DMMessageWindow>();
    win.Setup(msg, overload);
  }
}