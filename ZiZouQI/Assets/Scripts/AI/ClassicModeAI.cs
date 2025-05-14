using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassicModeAI : MonoBehaviour
{
    // Start is called before the first frame update
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
        if (!TurnManager.Instance.GetIsPlayerTurn())
        {
            ScoredGrid grid = AI.MinMax(-1, 1, false);
            GridPosition gridPos = grid.GetGridPosition();
            Vector3 worldPos = CheeseBoard.Instance.GetWorldPosition(gridPos);
            ModelGenerator.Instance.CreateAIPawn(worldPos);
            CheeseBoard.Instance.GetGridObject(grid.GetGridPosition()).AddPawn(new Pawn(false));
            TurnManager.Instance.NextTurn();
        }
    }
}
