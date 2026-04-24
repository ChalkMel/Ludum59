using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AdWindow : BaseWindow
{
  [SerializeField] private Button clickArea;
  [SerializeField] private List<GameObject> selectImagesCaptchaPrefab =  new List<GameObject>();
  [SerializeField] private GameObject rotateImageCaptchaPrefab;
  [SerializeField] private GameObject codeVerificationGamePrefab;
  [SerializeField] private GameObject malwareGamePrefab; 
    
  private Transform _parentTransform;
    
  private void Start()
  {
    clickArea.onClick.AddListener(Open);
  }
    
  private void Open()
  {
    int gameType = Random.Range(0, 8);
        
    _parentTransform = gameObject.transform.parent;
        
    if (gameType <= 2) 
    {
      SetupSelectCaptcha();
    }
    else if (gameType == 3)
    {
      SetupCodeVerificationGame();
    }
    else if (gameType >=4 && gameType <= 6) 
    {
      SetupRotateImageCaptcha();
    }
    else if(gameType == 7)
    {
      SetupMalwareGame();
    }
        
    Destroy(gameObject);
  }
    
  private void SetupSelectCaptcha()
  {
    var chooseImage = selectImagesCaptchaPrefab[Random.Range(0, selectImagesCaptchaPrefab.Count)];
    var game = Instantiate(chooseImage, _parentTransform);
    game.GetComponent<SelectImagesCaptcha>().Init(overload);
  }
    
  private void SetupRotateImageCaptcha()
  {
    var game = Instantiate(rotateImageCaptchaPrefab, _parentTransform);
    game.GetComponent<RotateImageCaptcha>().Init(overload);
  }
    
  private void SetupCodeVerificationGame()
  {
    var game = Instantiate(codeVerificationGamePrefab, _parentTransform);
    game.GetComponent<CodeVerificationGame>().Init(overload);
  }
    
  private void SetupMalwareGame()
  {
    var game = Instantiate(malwareGamePrefab, _parentTransform);
    game.GetComponent<MalwareGame>().Init(overload, FindObjectOfType<GameManager>());
  }
}