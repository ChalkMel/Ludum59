using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResetButton : MonoBehaviour
{
    private Button _btn;

    private void Start()
    {
        _btn = GetComponent<Button>();
        _btn.onClick.AddListener(ResetGame);
    }

    private void ResetGame()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index - 1);
    }
}
