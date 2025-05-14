using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button classicModeButton;
    [SerializeField] private Button creativeModeButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button gameRuleButton;
    [SerializeField] private GameObject gameRule;
    [SerializeField] private Button closeGameRuleButton;
    private void Start()
    {
        SetClassicModeButton();
        SetCreativeModeMenu();
        SetQuitButton();
        SetGameRuleButton();
        SetCloseGameRuleButton();
        gameRule.SetActive(false);
    }
    private void SetClassicModeButton()
    {
        classicModeButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(1);
        });
    }

    private void SetCreativeModeMenu()
    {
        creativeModeButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(2);
        });
    }

    private void SetQuitButton()
    {
        quitButton.onClick.AddListener(() => QuitGame());
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

    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
