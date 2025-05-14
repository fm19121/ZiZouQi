using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchManager : MonoBehaviour
{
    public static LaunchManager Instance { get; private set; }

    [SerializeField] private Vector3 pawnPos;
    [SerializeField] private float maxPower;
    [SerializeField] private PhysicMaterial physicMaterial;
    [SerializeField] private LineRenderer lineRenderer;
    private float power;
    private Vector3 launchDir;
    private bool isLaunching;
    private bool isRisePower;
    private GameObject currentPawn;

    private Pawn p;

    public event Action OnPawnStopMove;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one LaunchManager");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        InitLaunch();
        isLaunching = false;
        isRisePower = true;
        TurnManager.Instance.OnTurnChanged += InitLaunch;
    }

    void Update()
    {
        if (TurnManager.Instance.GetIsPlayerTurn())
        {
            GetLinePositions();
        }
        if (isLaunching)
        {
            lineRenderer.enabled = false;
        }
    }

    public void UpdatePowerValue()
    {
        if(power > maxPower) 
        {
            power = maxPower;
            isRisePower = false;
        }
        else if(power < 0)
        {
            power = 0;
            isRisePower = true;
        }
        if (isRisePower)
        {
            power += Time.deltaTime * 20;
        }
        else
        {
            power -= Time.deltaTime * 20;
        }
    }

    private void InitLaunch()
    {
        power = 0.0f;
        if(TurnManager.Instance.GetIsPlayerTurn()) 
        {
            currentPawn = ModelGenerator.Instance.CreatePlayerPawn(pawnPos);
            //PawnManager.Instance.AddPawn(new Pawn(true, pawnPos));
            p = new Pawn(false, currentPawn.GetComponentInChildren<Rigidbody>().transform.position);
            PawnManager.Instance.AddPawn(currentPawn);
        }
        else
        {
            currentPawn = ModelGenerator.Instance.CreateAIPawn(pawnPos);
            p = new Pawn(false, currentPawn.GetComponentInChildren<Rigidbody>().transform.position);
            PawnManager.Instance.AddPawn(currentPawn);
        }
        launchDir = new Vector3(0.0f, 0.0f, 1.0f);
    }

    public IEnumerator CheckPawnMovementRoutine()
    {
        while(true)
        {
            if (IsPawnFinishMovement())
            {
                Rigidbody rigid = currentPawn.GetComponentInChildren<Rigidbody>();
                
                OnPawnStopMove?.Invoke();
                isLaunching = false;
                yield break;
            }
            yield return null;
        }
    }

    public float CalculateSpeed(Vector3 dir, float distance)
    {
        //S = V ^ 2 / 2 a
        //V = sqrt(2 a S)
        float a = (float)(8 + physicMaterial.dynamicFriction * 10);
        float v = (float)Math.Sqrt(2 * distance * a);
        return v;
    }
    private Vector3 GetVelocity(Vector3 dir)
    {
        return dir * power;
    }

    private Vector3 GetDirection()
    {
        Vector3 dir = (Cursor.GetPosition() - pawnPos).normalized;
        launchDir = new Vector3(dir.x, 0, dir.z);
        return launchDir;
    }

    public void LaunchPawn()
    {
        Rigidbody rigid = currentPawn.GetComponentInChildren<Rigidbody>();
        rigid.AddForce(GetVelocity(GetDirection()), ForceMode.Impulse);
        isLaunching = true;
        power = 0.0f;
    }

    public void LaunchPawn(Vector3 dir)
    {
        Rigidbody rigid = currentPawn.GetComponentInChildren<Rigidbody>();
        rigid.AddForce(GetVelocity(dir), ForceMode.Impulse);
        isLaunching = true;
        power = 0.0f;
    }

    private bool IsPawnFinishMovement()
    {
        Rigidbody rigid = currentPawn.GetComponentInChildren<Rigidbody>();
        // return rigid.velocity == Vector3.zero;
        return rigid.IsSleeping();
    }

    private void GetLinePositions()
    {
        Vector3 initPos = pawnPos + new Vector3(0.0f, 1.0f, 10.0f);
        for(int i = 0; i < 2; i++)
        {
            lineRenderer.SetPosition(i, (initPos + i * 20 * GetDirection()));
        }
        lineRenderer.enabled = true;
    }
    public float GetMaxPower()
    {
        return maxPower;
    }

    public float GetCurrentPower()
    {
        return power;
    }

    public bool GetIsLaunching()
    {
        return isLaunching;
    }

    public Vector3 GetPawnPos()
    {
        return pawnPos;
    }

    public GameObject GetCurrenPawn()
    {
        return currentPawn;
    }
}
