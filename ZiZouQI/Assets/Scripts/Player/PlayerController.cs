using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private void Update()
    {
        if (GameRuleManager.Instance.IsCreativeModeEnd())
        {
            return;
        }
        if (TurnManager.Instance.GetIsPlayerTurn() && !LaunchManager.Instance.GetIsLaunching())
        {
            if (Input.GetMouseButton(0))
            {
                LaunchManager.Instance.UpdatePowerValue();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                LaunchManager.Instance.LaunchPawn();
                PawnManager.Instance.ReducePlayerPawn();
                StartCoroutine(LaunchManager.Instance.CheckPawnMovementRoutine());
            }
        }
    }

}
