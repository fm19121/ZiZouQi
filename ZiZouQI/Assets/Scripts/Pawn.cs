using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn
{
    private bool isPlayer;
    private Vector3 pawnPos;

    public Pawn(bool isPlayer)
    {
        this.isPlayer = isPlayer;
    }

    public Pawn(bool isPlayer, Vector3 pawnPos)
    {
        this.isPlayer = isPlayer;
        this.pawnPos = pawnPos;
    }

    public bool GetIsPlayer()
    {
        return isPlayer;
    }
    public Vector3 GetWorldPosition()
    {
        return pawnPos;
    }
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        return obj is Pawn p && isPlayer == p.isPlayer;
    }

    public override int GetHashCode()
    {
        return isPlayer.GetHashCode();
    }
    public static bool operator ==(Pawn a, Pawn b)
    {
        if (ReferenceEquals(a, b)) 
        {
            return true; 
        }
        if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) 
        { 
            return false; 
        }
        return a.isPlayer == b.isPlayer;
    }

    public static bool operator !=(Pawn a, Pawn b)
    {
        if (ReferenceEquals(a, b))
        {
            return false;
        }
        if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
        {
            return true;
        }

        return a.isPlayer != b.isPlayer;
    }
}
