using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance { get; private set; }

    public event Action OnTurnChanged;
    private bool isPlayerTurn;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one TurnManager");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    void Start()
    {
        isPlayerTurn = Random.Range(0, 2) == 0;
        if(LaunchManager.Instance != null)
        {
            LaunchManager.Instance.OnPawnStopMove += NextTurn;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool GetIsPlayerTurn()
    {
        return isPlayerTurn;
    }

    public void NextTurn()
    {
        isPlayerTurn = !isPlayerTurn;
        OnTurnChanged?.Invoke();
    }
}
