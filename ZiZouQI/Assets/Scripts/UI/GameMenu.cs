using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private GameObject gameMenu;
    [SerializeField] private TextMeshProUGUI gameResult;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private Button startMenuButton;
    [SerializeField] private Button classicModeButton;
    [SerializeField] private Button creativeModeButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button gameRuleButton;
    [SerializeField] private GameObject gameRule;
    [SerializeField] private Button closeGameRuleButton;
    [SerializeField] private bool isClassicMode;

    private bool displayMenu;

    private void Start()
    {
        Time.timeScale = 1f;
        InitMenu();
        gameRule.SetActive(false);
        gameMenu.SetActive(false);
        displayMenu = false;
    }

    private void OnEnable()
    {
        if (isClassicMode)
        {
            TurnManager.Instance.OnTurnChanged += DetectClassicModeEnd;
        }
        else
        {
            TurnManager.Instance.OnTurnChanged += DetectCreativeModeEnd;
        }
        UIManager.OnMenuCalled += CallMenuInGame;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CallMenuInGame();
        }
    }

    private void OnDisable()
    {
        if (isClassicMode)
        {
            TurnManager.Instance.OnTurnChanged -= DetectClassicModeEnd;
        }
        else
        {
            TurnManager.Instance.OnTurnChanged -= DetectCreativeModeEnd;
        }
        UIManager.OnMenuCalled -= CallMenuInGame;
    }
    private void WinMenu()
    {
        gameMenu.SetActive(true);
        continueButton.SetActive(false);
        gameResult.text = "!!! You WIN !!!";
        Time.timeScale = 0f;
    }

    private void LooseMenu()
    {
        gameMenu.SetActive(true);
        continueButton.SetActive(false);
        gameResult.text = "!!! You LOSE !!!";
        Time.timeScale = 0f;
    }

    private void ScoreDraw()
    {
        gameMenu.SetActive(true);
        continueButton.SetActive(false);
        gameResult.text = "!!! Score Draw !!!";
        Time.timeScale = 0f;
    }

    private void InitMenu()
    {
        SetContinueButton();
        SetStartMenuButton();
        SetClassicModeButton();
        SetCreativeModeMenu();
        SetGameRuleButton();
        SetCloseGameRuleButton();
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

    private void SetContinueButton()
    {
        continueButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            displayMenu = false;
            gameMenu.SetActive(false);
            Time.timeScale = 1f;
        });
    }

    private void SetClassicModeButton()
    {
        classicModeButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            displayMenu = false;
            SceneManager.LoadScene(1);
        });
    }

    private void SetCreativeModeMenu()
    {
        creativeModeButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            displayMenu = false;
            SceneManager.LoadScene(2);
        });
    }

    private void SetGameRuleButton()
    {
        gameRuleButton.onClick.AddListener(() =>
        {
            gameRule.SetActive(true);
        });
    }

    private void SetCloseGameRuleButton()
    {
        closeGameRuleButton.onClick.AddListener(() =>
        {
            gameRule.SetActive(false);
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

    private void DetectCreativeModeEnd()
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

    private void DetectClassicModeEnd()
    {
        if (GameRuleManager.Instance.IsClassicModeEnd())
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

    private void CallMenuInGame()
    {
        displayMenu = !displayMenu;
        gameMenu.SetActive(displayMenu);
        Time.timeScale = displayMenu ? 0.0f : 1f;
    }
}