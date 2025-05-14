using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    private GridPosition gridPos;
    private Pawn pawn;

    public GridObject(GridPosition gridPos)
    {
        this.gridPos = gridPos;
        this.pawn = null;
    }

    public void AddPawn(Pawn p)
    {
        pawn = p;
    }

    public Pawn GetPawn()
    {
        return pawn;
    }

    public void RemovePawn()
    {
        pawn = null;
    }
    
    public GridPosition GetGridPosition() 
    {
        return gridPos;
    }
}