using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SystemUpdateWindow : MonoBehaviour
{
    [SerializeField] private Slider progressSlider;
    [SerializeField] private TextMeshProUGUI progressText;
    [SerializeField] private GameObject passwordPanel;    // панель с полем ввода
    [SerializeField] private GameObject progressPanel;    // панель с прогрессом
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private Button submitButton;
    [SerializeField] private TextMeshProUGUI errorText;
    
    [SerializeField] private float updateDuration = 5f;
    
    private OverloadSystem overload;
    private NotesManager notesManager;
    private bool isUpdating = false;
    
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
            // Пароль верный - начинаем обновление
            passwordPanel.SetActive(false);
            progressPanel.SetActive(true);
            StartCoroutine(UpdateProcess());
        }
        else
        {
            // Пароль неверный
            errorText.text = "Неверный пароль!";
            errorText.gameObject.SetActive(true);
            passwordInput.text = "";
            
            // Штраф за неверный пароль
            overload?.Add(10);
        }
    }
    
    private IEnumerator UpdateProcess()
    {
        isUpdating = true;
        float currentTime = 0f;
        progressSlider.maxValue = updateDuration;
        progressSlider.value = 0f;
        
        while (currentTime < updateDuration)
        {
            currentTime += Time.deltaTime;
            progressSlider.value = currentTime;
            if (progressText != null)
                progressText.text = $"Обновление... {Mathf.RoundToInt(currentTime / updateDuration * 100)}%";
            yield return null;
        }
        
        // Обновление завершено
        overload?.Add(25);
        Destroy(gameObject);
    }
}