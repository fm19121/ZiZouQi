using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public static PlayerAction Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one PlayerAction");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameRuleManager.Instance.IsGameEnd())
        {
            return;
        }
        if (TurnManager.Instance.GetIsPlayerTurn()) 
        {
            if(Input.GetMouseButtonDown(0))
            {
                GridPosition gridPos = CheeseBoard.Instance.GetGridPosition(Cursor.GetPosition());
                if (CheeseBoard.Instance.IsInsideCheeseBoard(gridPos))
                {
                    GridObject selectedGrid = CheeseBoard.Instance.GetGridObject(gridPos);
                    if (selectedGrid.GetPawn() == null)
                    {
                        Vector3 worldPos = CheeseBoard.Instance.GetWorldPosition(gridPos);
                        ModelGenerator.Instance.CreatePlayerPawn(worldPos);
                        selectedGrid.AddPawn(new Pawn(true));
                        TurnManager.Instance.NextTurn();
                    }
                }
            }
        }
    }
}
