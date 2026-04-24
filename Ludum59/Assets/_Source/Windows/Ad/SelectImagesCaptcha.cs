using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    
    private OverloadSystem _overload;
    private int _correctSelections;
    private int _totalCorrect;
    private List<Button> _createdButtons = new List<Button>();
    
    [System.Serializable]
    public class ImageData
    {
        public Sprite sprite;
        public bool isCorrect;
    }
    
    public void Init(OverloadSystem overloadSystem)
    {
        _overload = overloadSystem;
        taskText.text = "Choose right pictures";
        
        _totalCorrect = 0;
        foreach (var imgData in availableImages)
        {
            if (imgData.isCorrect)
                _totalCorrect++;
        }
        
        _correctSelections = 0;
        closeButton.interactable = false;
        
        foreach (var imgData in availableImages)
        {
            var btn = Instantiate(imageButtonPrefab, imagesContainer);
            btn.GetComponent<Image>().sprite = imgData.sprite;
            
            var buttonComponent = btn.GetComponent<Button>();
            var imgDataCopy = imgData;
            
            buttonComponent.onClick.AddListener(() => OnImageSelected(buttonComponent, imgDataCopy));
            _createdButtons.Add(buttonComponent);
        }
        
        closeButton.onClick.AddListener(Close);
    }
    
    private void OnImageSelected(Button btn, ImageData imgData)
    {
        btn.interactable = false;
        
        if (imgData.isCorrect)
        {
            _correctSelections++;
            resultText.text = $"Chosen: {_correctSelections}/{_totalCorrect}";
             var sprite = btn.GetComponentInParent<Image>(); 
             sprite.color = Color.yellow;
            
            if (_correctSelections >= _totalCorrect)
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
        Close();
        closeButton.interactable = true;
        
        foreach (var btn in _createdButtons)
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
        
        _createdButtons.Clear();
        
        _correctSelections = 0;
        closeButton.interactable = false;

        foreach (var imgData in availableImages)
        {
            var btn = Instantiate(imageButtonPrefab, imagesContainer);
            btn.GetComponent<Image>().sprite = imgData.sprite;
            
            var buttonComponent = btn.GetComponent<Button>();
            var imgDataCopy = imgData;
            
            buttonComponent.onClick.AddListener(() => OnImageSelected(buttonComponent, imgDataCopy));
            _createdButtons.Add(buttonComponent);
        }
    }
    
    private void Close()
    {
        _overload?.Add(-5);
        Destroy(gameObject);
    }
}