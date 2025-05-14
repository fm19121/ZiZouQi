using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEndMenu : MonoBehaviour
{
    private GridPosition winCondition = new GridPosition(15, 20);
    [SerializeField] private GameObject gameEnd;
    [SerializeField] private TextMeshProUGUI gameResult;
    [SerializeField] private Button startMenuButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button quitButton;

    private void Start()
    {
        Time.timeScale = 1f;
        InitMenu();
        gameEnd.SetActive(false);
        TurnManager.Instance.OnTurnChanged += DetectGameEnd;
    }

    private void Update()
    {
    }
    private void WinMenu()
    {
        gameEnd.SetActive(true);
        gameResult.text = "!!! You WIN !!!";
        Time.timeScale = 0f;
    }

    private void LooseMenu()
    {
        gameEnd.SetActive(true);
        gameResult.text = "!!! You LOSE !!!";
        Time.timeScale = 0f;
    }

    private void ScoreDraw()
    {
        gameEnd.SetActive(true);
        gameResult.text = "!!! Score Draw !!!";
        Time.timeScale = 0f;
    }

    private void InitMenu()
    {
        SetStartMenuButton();
        SetRestartButton();
        SetQuitButton();
    }
    private void SetStartMenuButton()
    {
        startMenuButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene(0);
        });
    }

    private void SetRestartButton()
    {
        restartButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
    }

    private void SetQuitButton()
    {
        quitButton.onClick.AddListener(() =>
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        });
    }

    private void DetectGameEnd()
    {
        if (GameRuleManager.Instance.IsCreativeModeEnd())
        {
            int score = GameRuleManager.Instance.EvaluateScore();
            if (score == 1)
            {
                WinMenu();
            }
            else if (score == -1)
            {
                LooseMenu();
            }
            else
            {
                ScoreDraw();
            }
        }
    }
}