using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SystemUpdateWindow : MonoBehaviour
{
    [SerializeField] private List<Image> progressSegments = new List<Image>();
    [SerializeField] private TextMeshProUGUI progressText;
    [SerializeField] private GameObject passwordPanel;
    [SerializeField] private GameObject progressPanel;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private Button submitButton;
    
    [SerializeField] private float updateDuration = 5f;
    
    private OverloadSystem _overload;
    private NotesManager _notesManager;
    private bool isUpdating;
    
    public void Init(OverloadSystem overloadSystem, NotesManager notesMgr)
    {
        _overload = overloadSystem;
        _notesManager = notesMgr;
        
        passwordPanel.SetActive(true);
        progressPanel.SetActive(false);
        
        submitButton.onClick.AddListener(CheckPassword);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
        {
            CheckPassword();
        }
    }

    private void CheckPassword()
    {
        if (_notesManager.CheckPassword(passwordInput.text))
        {
            passwordPanel.SetActive(false);
            progressPanel.SetActive(true);
            StartCoroutine(UpdateProcess());
        }
        else
        {
            //TODO
            var block = passwordInput.colors;
            passwordInput.colors = block;
            passwordInput.text = "";
        }
    }
    
    private IEnumerator UpdateProcess()
    {
        _overload?.Add(25);
        isUpdating = true;
        foreach (var segment in progressSegments)
        {
            if (segment != null)
                segment.gameObject.SetActive(false);
        }
        
        float currentTime = 0f;
        int totalSegments = progressSegments.Count;
        float timePerSegment = updateDuration / totalSegments;
        
        for (int i = 0; i < totalSegments; i++)
        {
            if (progressSegments[i] != null)
                progressSegments[i].gameObject.SetActive(true);
            
            float segmentStartTime = Time.time;
            float segmentProgress = 0f;
            
            while (segmentProgress < 1f)
            {
                segmentProgress = (Time.time - segmentStartTime) / timePerSegment;
                currentTime = (i + segmentProgress) * timePerSegment;
                
                int percent = Mathf.FloorToInt(currentTime / updateDuration * 100);
                percent = Mathf.Min(percent, 100);
                
                if (progressText != null)
                    progressText.text = $"Updating... {percent}%";
                
                yield return null;
            }
        }
        
        _overload?.Remove(25);
        Destroy(gameObject);
    }
}