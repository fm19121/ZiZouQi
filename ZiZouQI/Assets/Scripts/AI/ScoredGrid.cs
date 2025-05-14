using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoredGrid
{
    private GridPosition gridPos;
    private int score;

    public ScoredGrid(GridPosition gridPosition, int score)
    {
        this.gridPos = gridPosition;
        this.score = score;
    }

    public ScoredGrid(int score)
    {
        this.score = score;
    }
    public int GetScore()
    {
        return score;
    }
    public void SetScore(int score) 
    {
        this.score = score;
    }

    public void SetGridPostion(GridPosition gridPosition)
    { 
        this.gridPos = gridPosition;
    }

    public GridPosition GetGridPosition()
    {
        return gridPos;
    }
}
