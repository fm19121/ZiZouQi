using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreativeModeAI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameRuleManager.Instance.IsCreativeModeEnd())
        {
            return;
        }
        if (!TurnManager.Instance.GetIsPlayerTurn() && !LaunchManager.Instance.GetIsLaunching())
        {
            if(PawnManager.Instance.GetPawnObjects().Count > 5)
            {
                DisturbPlayer();
            }
            else
            {
                minMaxAlgorithm();
            }
        }
    }

    private Vector3 CalculateDirection(Vector3 target)
    {
        Vector3 initPos = LaunchManager.Instance.GetPawnPos();
        return (target - initPos).normalized;
    }
    private float CalculateSpeed(Vector3 target)
    {
        Vector3 initPos = LaunchManager.Instance.GetPawnPos();
        Vector3 direction = (target - initPos).normalized;
        float distance = Vector3.Distance(initPos, target);
        return LaunchManager.Instance.CalculateSpeed(direction, distance);
    }

    private void LaunchToTarget(Vector3 target)
    {
        float speed = CalculateSpeed(target);
        LaunchManager.Instance.UpdatePowerValue();
        float power = LaunchManager.Instance.GetCurrentPower();
        if (power >= speed - 0.1 && power <= speed + 0.5)
        {
            LaunchManager.Instance.LaunchPawn(CalculateDirection(target));
            PawnManager.Instance.ReduceAIPawn();
            StartCoroutine(LaunchManager.Instance.CheckPawnMovementRoutine());
        }
    }

    private void minMaxAlgorithm()
    {
        ScoredGrid grid = AI.MinMax(-1, 1, false);
        GridPosition gridPos = grid.GetGridPosition();
        Vector3 worldPos = CheeseBoard.Instance.GetWorldPosition(gridPos);
        LaunchToTarget(worldPos);
    }
    private void DisturbPlayer()
    {
        List<GameObject> playerPawns = PawnManager.Instance.GetPawnObjects();
        int size = playerPawns.Count;
        int i = Random.Range(0, size);
        while (!playerPawns[i].GetComponentInChildren<PawnInfo>().GetIsPlayer())
        {
            i= (i+ 1) % size;
        }
        Vector3 targetPos = playerPawns[i].GetComponentInChildren<Rigidbody>().position;
        LaunchToTarget(targetPos);
        /*foreach (GameObject playerPawn in playerPawns)
        {
            bool isPlayerPawn = playerPawn.GetComponentInChildren<PawnInfo>().GetIsPlayer();
            if (isPlayerPawn) 
            {
                Vector3 targetPos = playerPawn.GetComponentInChildren<Rigidbody>().position;
                LaunchToTarget(targetPos);
            }
        }*/
    }
}
