using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class CircleClickGame : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] private GameObject circlePrefab;
    [SerializeField] private Transform gameArea;
    [SerializeField] private float gameDuration = 4f;
    [SerializeField] private float spawnInterval = 0.5f;
    [SerializeField] private int circlesToClick = 4;
    
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI circlesLeftText;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private Button closeButton;
    
    [Header("Визуал")]
    [SerializeField] private float circleRadius = 50f;
    
    private List<GameObject> _activeCircles = new List<GameObject>();
    private int _circlesClicked;
    private float _remainingTime;
    private bool _gameActive;
    private bool _gameCompleted;
    
    public delegate void GameResultHandler(bool success);
    public event GameResultHandler OnGameFinished;
    
    private void Start()
    {
        if (closeButton != null)
            closeButton.onClick.AddListener(CloseGame);
    }
    
    public void StartGame()
    {
        gamePanel.SetActive(true);
        _gameActive = true;
        _gameCompleted = false;
        _circlesClicked = 0;
        _remainingTime = gameDuration;
        
        UpdateUI();
        StartCoroutine(SpawnCircles());
        StartCoroutine(GameTimer());
    }
    
    private IEnumerator SpawnCircles()
    {
        int spawnedCount = 0;
        
        while (spawnedCount < circlesToClick && _gameActive && !_gameCompleted)
        {
            SpawnCircle();
            spawnedCount++;
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    
    private void SpawnCircle()
    {
        if (!_gameActive || _gameCompleted) return;
        
        GameObject circle = Instantiate(circlePrefab, gameArea);
        var image = circle.GetComponent<Image>();
        image.DOColor(Color.black, 10f);
        circle.transform.DOScale(0, 0f).onComplete += () => { circle.transform.DOScale(2f, .25f); };
        RectTransform rectTransform = circle.GetComponent<RectTransform>();
        if (rectTransform != null && gameArea is RectTransform areaRect)
        {
            float maxX = areaRect.rect.width / 2 - circleRadius;
            float maxY = areaRect.rect.height / 2 - circleRadius;
            
            float randomX = Random.Range(-maxX, maxX);
            float randomY = Random.Range(-maxY, maxY);
            
            rectTransform.anchoredPosition = new Vector2(randomX, randomY);
            rectTransform.sizeDelta = new Vector2(circleRadius * 2, circleRadius * 2);
        }
        
        Button circleButton = circle.GetComponent<Button>();
        if (circleButton == null)
            circleButton = circle.AddComponent<Button>();
            
        int circleId = _activeCircles.Count;
        circleButton.onClick.AddListener(() => OnCircleClicked(circle));
        
        _activeCircles.Add(circle);
    }
    
    private void OnCircleClicked(GameObject circle)
    {
        if (!_gameActive || _gameCompleted) return;
        
        if (_activeCircles.Contains(circle))
        {
            circle.transform.DOScale(0f, 0.25f).onComplete += () =>
            {
                _activeCircles.Remove(circle);
                Destroy(circle);
                _circlesClicked++;
                UpdateUI();

                if (_circlesClicked >= circlesToClick)
                {
                    WinGame();
                }
            };
        }
    }
    
    private IEnumerator GameTimer()
    {
        while (_remainingTime > 0 && _gameActive && !_gameCompleted)
        {
            _remainingTime -= Time.deltaTime;
            UpdateTimerDisplay();
            yield return null;
        }
        
        if (_gameActive && !_gameCompleted && _remainingTime <= 0)
        {
            LoseGame();
        }
    }
    
    private void WinGame()
    {
        if (!_gameActive || _gameCompleted) return;
        
        _gameCompleted = true;
        _gameActive = false;
        UpdateUI();
        

        ClearAllCircles();
        

        OnGameFinished?.Invoke(true);
     
        StartCoroutine(CloseAfterDelay(0.5f));
    }
    
    private void LoseGame()
    {
        if (!_gameActive || _gameCompleted) return;
        
        _gameCompleted = true;
        _gameActive = false;
        
  
        ClearAllCircles();
        

        OnGameFinished?.Invoke(false);

        StartCoroutine(CloseAfterDelay(0.5f));
    }
    
    private void ClearAllCircles()
    {
        foreach (GameObject circle in _activeCircles)
        {
            if (circle != null)
                Destroy(circle);
        }
        _activeCircles.Clear();
    }
    
    private void UpdateUI()
    {
        if (circlesLeftText != null)
            circlesLeftText.text = $"Left: {circlesToClick - _circlesClicked}";
    }
    
    private void UpdateTimerDisplay()
    {
        if (timerText != null)
            timerText.text = $"Time: {_remainingTime:F1}";
    }
    
    private void CloseGame()
    {
        if (_gameActive && !_gameCompleted)
        {
            LoseGame();
        }
        else
        {
            gamePanel.SetActive(false);
        }
    }
    
    private IEnumerator CloseAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gamePanel.SetActive(false);
    }
    
    private void OnDestroy()
    {
        StopAllCoroutines();
        ClearAllCircles();
    }
}