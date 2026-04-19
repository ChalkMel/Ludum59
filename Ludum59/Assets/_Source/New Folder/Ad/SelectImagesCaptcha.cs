using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Rendering;

public class SelectImagesCaptcha : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI taskText;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private Transform imagesContainer;
    [SerializeField] private GameObject imageButtonPrefab;
    
    [Header("Настройки")]
    [SerializeField] private float successDelay = 1f;
    
    [Header("Картинки")]
    [SerializeField] private List<ImageData> availableImages;
    
    private OverloadSystem overload;
    private int correctSelections;
    private int totalCorrect;
    private List<Button> createdButtons = new List<Button>();
    
    [System.Serializable]
    public class ImageData
    {
        public Sprite sprite;
        public bool isCorrect;
    }
    
    public void Init(OverloadSystem overloadSystem)
    {
        overload = overloadSystem;
        taskText.text = "Choose right pictures";
        
        totalCorrect = 0;
        foreach (var imgData in availableImages)
        {
            if (imgData.isCorrect)
                totalCorrect++;
        }
        
        correctSelections = 0;
        closeButton.interactable = false;
        
        foreach (var imgData in availableImages)
        {
            var btn = Instantiate(imageButtonPrefab, imagesContainer);
            btn.GetComponent<Image>().sprite = imgData.sprite;
            
            var buttonComponent = btn.GetComponent<Button>();
            var imgDataCopy = imgData; // важно для замыкания
            
            buttonComponent.onClick.AddListener(() => OnImageSelected(buttonComponent, imgDataCopy));
            createdButtons.Add(buttonComponent);
        }
        
        closeButton.onClick.AddListener(Close);
    }
    
    private void OnImageSelected(Button btn, ImageData imgData)
    {
        btn.interactable = false;
        
        if (imgData.isCorrect)
        {
            correctSelections++;
            resultText.text = $"Chosen: {correctSelections}/{totalCorrect}";
             var sprite = btn.GetComponentInParent<Image>(); 
             sprite.color = Color.yellow;
            
            if (correctSelections >= totalCorrect)
            {
                Success();
            }
        }
        else
        {
            Fail();
        }
    }
    
    private void Success()
    {
        resultText.text = "Right";
        resultText.color = Color.green;
        overload?.Add(-5);
        closeButton.interactable = true;
        
        foreach (var btn in createdButtons)
        {
            btn.interactable = false;
        }
    }
    
    private void Fail()
    {
        resultText.text =  "Wrong! Now you must wait for next try";
        ResetGame();
    }
    
    private void ResetGame()
    {
        foreach (Transform child in imagesContainer)
            Destroy(child.gameObject);
        
        createdButtons.Clear();
        
        correctSelections = 0;
        closeButton.interactable = false;

        foreach (var imgData in availableImages)
        {
            var btn = Instantiate(imageButtonPrefab, imagesContainer);
            btn.GetComponent<Image>().sprite = imgData.sprite;
            
            var buttonComponent = btn.GetComponent<Button>();
            var imgDataCopy = imgData;
            
            buttonComponent.onClick.AddListener(() => OnImageSelected(buttonComponent, imgDataCopy));
            createdButtons.Add(buttonComponent);
        }
    }
    
    private void Close()
    {
        Destroy(gameObject);
        overload?.Add(-5);
    }
}