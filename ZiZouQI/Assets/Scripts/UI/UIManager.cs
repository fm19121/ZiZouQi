using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image launchPowerBar;
    [SerializeField] private TextMeshProUGUI turnText;
    [SerializeField] private TextMeshProUGUI PlayerPawnNum;
    [SerializeField] private TextMeshProUGUI AIPawnNum;
    [SerializeField] private Button menuButton;

    public static Action OnMenuCalled;
    void Start()
    {
        InitPowerUI();
        UpdateTurnText();
        UpdatePawnNumUI();
        SetMenuButton();
        TurnManager.Instance.OnTurnChanged += UpdateTurnText;
        TurnManager.Instance.OnTurnChanged += UpdatePawnNumUI;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLaunchPowerBar();
    }
    private void UpdateLaunchPowerBar()
    {
        float maxPower = LaunchManager.Instance.GetMaxPower();
        float power = LaunchManager.Instance.GetCurrentPower();
        launchPowerBar.fillAmount = power / maxPower;
    }

    private void InitPowerUI()
    {
        launchPowerBar.fillAmount = 0.0f;
    }

    private void UpdatePawnNumUI()
    {
        PlayerPawnNum.text = "Player Pawn Remain: " + PawnManager.Instance.GetPlayerPawnNum();
        AIPawnNum.text = "AI Pawn Remain: " + PawnManager.Instance.GetAIPawnNum();
    }

    private void UpdateTurnText()
    {
        if (TurnManager.Instance.GetIsPlayerTurn())
        {
            turnText.text = " Your Turn ";
        }
        else
        {
            turnText.text = " Opponent's Turn ";
        }
    }

    private void SetMenuButton()
    {
        menuButton.onClick.AddListener(() =>
        {
            OnMenuCalled?.Invoke();
        });
    }
}
