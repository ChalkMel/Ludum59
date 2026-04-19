using UnityEngine;
using UnityEngine.UI;
public class AdWindow : BaseWindow
{
  [SerializeField] private Button clickArea;
  [SerializeField] private GameObject selectImagesCaptchaPrefab;
  [SerializeField] private GameObject rotateImageCaptchaPrefab;
  [SerializeField] private GameObject codeVerificationGamePrefab;
  
  private Transform parentTransform;
  private  GameObject miniGame;
  private void Start()
  {
    clickArea.onClick.AddListener(Open);
  }

  private void Open()
  {
    int gameType = Random.Range(0, 7);
    
    parentTransform = gameObject.transform.parent;

    if (gameType <= 2)
    {
      SetupSelectCaptcha();
    }
    else if (gameType == 3)
    {
      SetupCodeVerificationGame();
    }
    else
    {
      SetupRotateImageCaptcha();
    }

    Destroy(gameObject);
  }

  private void SetupSelectCaptcha()
  {
    miniGame = Instantiate(selectImagesCaptchaPrefab,  parentTransform);
    miniGame.GetComponent<SelectImagesCaptcha>().Init(overload);
  }

  private void SetupRotateImageCaptcha()
  {
    miniGame = Instantiate(rotateImageCaptchaPrefab,  parentTransform);
    miniGame.GetComponent<RotateImageCaptcha>().Init(overload);
  }

  private void SetupCodeVerificationGame()
  {
    miniGame = Instantiate(codeVerificationGamePrefab, parentTransform);
    miniGame.GetComponent<CodeVerificationGame>().Init(overload);
  }
}