using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SystemUpdateWindow : MonoBehaviour
{
    [SerializeField] private Slider progressSlider;
    [SerializeField] private TextMeshProUGUI progressText;
    [SerializeField] private GameObject passwordPanel;
    [SerializeField] private GameObject progressPanel;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private Button submitButton;
    [SerializeField] private TextMeshProUGUI errorText;
    
    [SerializeField] private float updateDuration = 5f;
    
    private OverloadSystem overload;
    private NotesManager notesManager;
    private bool isUpdating;
    
    public void Init(OverloadSystem overloadSystem, NotesManager notesMgr)
    {
        overload = overloadSystem;
        notesManager = notesMgr;
        
        passwordPanel.SetActive(true);
        progressPanel.SetActive(false);
        errorText.gameObject.SetActive(false);
        
        submitButton.onClick.AddListener(CheckPassword);
    }
    
    private void CheckPassword()
    {
        if (notesManager.CheckPassword(passwordInput.text))
        {
            passwordPanel.SetActive(false);
            progressPanel.SetActive(true);
            StartCoroutine(UpdateProcess());
        }
        else
        {
            errorText.text = "Неверный пароль!";
            errorText.gameObject.SetActive(true);
            passwordInput.text = "";
        }
    }
    
    private IEnumerator UpdateProcess()
    {
        overload?.Add(25);
        isUpdating = true;
        float currentTime = 0f;
        progressSlider.maxValue = updateDuration;
        progressSlider.value = 0f;
        
        while (currentTime < updateDuration)
        {
            currentTime += Time.deltaTime;
            progressSlider.value = currentTime;
            if (progressText != null)
                progressText.text = $"Updating... {Mathf.RoundToInt(currentTime / updateDuration * 100)}%";
            yield return null;
        }
        
        overload?.Remove(25);
        Destroy(gameObject);
    }
}