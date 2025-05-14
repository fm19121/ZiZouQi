using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnInfo : MonoBehaviour
{
    [SerializeField] bool isPlayer;

    public bool GetIsPlayer()
    {
        return isPlayer;
    }
}
