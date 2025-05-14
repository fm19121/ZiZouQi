using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class AI
{
    public static ScoredGrid MinMax(int alpha, int beta, bool isMaxPlayer)
    {
        List<GridPosition> emptyGrids = CheeseBoard.Instance.GetEmptyGrids();
        if (GameRuleManager.Instance.IsClassicModeEnd())
        {
            ScoredGrid scoredGrid = new ScoredGrid(GameRuleManager.Instance.EvaluateScore());
            return scoredGrid;
        }
        ScoredGrid bestMove = new ScoredGrid(new GridPosition(0, 0), 0);
        if(isMaxPlayer)
        {
            bestMove.SetScore(-1);
            for (int i = 0; i < emptyGrids.Count(); i++)
            {
                GridObject gridObj = CheeseBoard.Instance.GetGridObject(emptyGrids[i]);
                gridObj.AddPawn(new Pawn(true));
                ScoredGrid move = MinMax(alpha, beta, false);
                gridObj.RemovePawn();
                move.SetGridPostion(emptyGrids[i]);
                if(move.GetScore() > bestMove.GetScore())
                {
                    bestMove = move;
                }
                alpha = Mathf.Max(alpha, bestMove.GetScore());
                if(beta <= alpha)
                {
                    break;
                }
            }
        }
        else
        {
            bestMove.SetScore(1);
            for(int i = 0; i < emptyGrids.Count(); i++)
            {
                GridObject gridObj = CheeseBoard.Instance.GetGridObject(emptyGrids[i]);
                gridObj.AddPawn(new Pawn(false));
                ScoredGrid move = MinMax(alpha, beta, true);
                gridObj.RemovePawn();
                move.SetGridPostion(emptyGrids[i]);
                if (move.GetScore() < bestMove.GetScore())
                {
                    bestMove = move;
                }
                beta = Mathf.Min(beta, bestMove.GetScore());
                if (beta <= alpha)
                {
                    break;
                }

            }
        }
        return bestMove;
    }
}
