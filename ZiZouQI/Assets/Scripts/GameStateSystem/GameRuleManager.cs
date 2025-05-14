using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameRuleManager : MonoBehaviour
{
    private GridObject[,] gridObjects;

    public static GameRuleManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one GameRuleManager");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        gridObjects = CheeseBoard.Instance.GetGridObjects();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool isPlayerWin()
    {
        return GetWinPawn() == new Pawn(true);
    }

    private bool isAIWin()
    {
        return GetWinPawn() == new Pawn(false);
    }

    public Pawn GetWinPawn()
    {
        for (int i = 0; i < 3; i++)
        {
            if (gridObjects[i, 0].GetPawn() != null &&
                gridObjects[i, 0].GetPawn() == gridObjects[i, 1].GetPawn() &&
                gridObjects[i, 0].GetPawn() == gridObjects[i, 2].GetPawn())
            {
                return gridObjects[i, 0].GetPawn();
            }
            if (gridObjects[0, i].GetPawn() != null &&
                gridObjects[0, i].GetPawn() == gridObjects[1, i].GetPawn() &&
                gridObjects[0, i].GetPawn() == gridObjects[2, i].GetPawn())
            {
                return gridObjects[0, i].GetPawn();
            }
        }
        if (gridObjects[0, 0].GetPawn() != null &&
           gridObjects[0, 0].GetPawn() == gridObjects[1, 1].GetPawn() &&
           gridObjects[0, 0].GetPawn() == gridObjects[2, 2].GetPawn())
        {
            return gridObjects[0, 0].GetPawn();
        }
        if (gridObjects[0, 2].GetPawn() != null &&
           gridObjects[0, 2].GetPawn() == gridObjects[1, 1].GetPawn() &&
           gridObjects[0, 2].GetPawn() == gridObjects[2, 0].GetPawn())
        {
            return gridObjects[0, 2].GetPawn();
        }
        return null;
    }

    public int EvaluateScore()
    {
        if (isPlayerWin())
        {
            return 1;
        }
        else if (isAIWin())
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }

    public bool IsGameEnd()
    {
        if(GetWinPawn() != null || CheeseBoard.Instance.GetEmptyGrids().Count() < 1)
        {
            return true;
        }
        return false;
    }

    public bool IsCreativeModeEnd()
    {
        if(GetWinPawn() != null || PawnManager.Instance.IsNoPawnRemain())
        {
            return true;
        }
        return false;
    }
}
