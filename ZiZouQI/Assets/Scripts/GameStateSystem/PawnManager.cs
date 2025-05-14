using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnManager : MonoBehaviour
{
    private List<GameObject> pawnsObj;
    [SerializeField] private int playerPawnNum;
    [SerializeField] private int AIPawnNum;
    public static PawnManager Instance { get; private set; }

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
        pawnsObj = new List<GameObject>();
        TurnManager.Instance.OnTurnChanged += CalcluateGridPawn;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPawn(GameObject p)
    {
        pawnsObj.Add(p);
    }

    private bool IsInsideGrid(Vector3 gridWorldPos, Vector3 pawnPos)
    {
        int halfGrid = CheeseBoard.Instance.GetUnitSize() / 2;
        if(gridWorldPos.x - halfGrid <= pawnPos.x && pawnPos.x <= gridWorldPos.x + halfGrid)
        {
            if(gridWorldPos.z - halfGrid <= pawnPos.z && pawnPos.z <= gridWorldPos.z + halfGrid)
            {
                return true;
            }
        }
        return false;
    }
    private void CalcluateGridPawn()
    {
        GridObject[,] gridObjects = CheeseBoard.Instance.GetGridObjects();
        for (int x = 0; x < 3; x++)
        {
            for(int z = 0; z < 3; z++) 
            {
                Vector3 gridWorldPos = CheeseBoard.Instance.GetWorldPosition(gridObjects[x, z].GetGridPosition());
                Pawn pawnInGrid = gridObjects[x, z].GetPawn();
                bool hasPawn = false;
                foreach (GameObject p in pawnsObj)
                {
                    Vector3 pawnPos = p.GetComponentInChildren<Rigidbody>().position;
                    bool isPlayer = p.GetComponentInChildren<PawnInfo>().GetIsPlayer();
                    if (IsInsideGrid(gridWorldPos, pawnPos))
                    {
                        hasPawn = true;
                        if (pawnInGrid == null)
                        {
                            pawnInGrid = new Pawn(isPlayer, pawnPos);
                            gridObjects[x, z].AddPawn(pawnInGrid);
                        }
                        else if (Vector3.Distance(pawnPos, gridWorldPos) < Vector3.Distance(pawnInGrid.GetWorldPosition(), gridWorldPos))
                        {
                            pawnInGrid = new Pawn(isPlayer, pawnPos);
                            gridObjects[x, z].AddPawn(pawnInGrid);
                        }
                    }
                }
                if(!hasPawn)
                {
                    gridObjects[x, z].RemovePawn();
                }
            }
        }
    }

    public List<GameObject> GetPawnObjects()
    {
        return pawnsObj;
    }

    public void ReducePlayerPawn()
    {
        if(playerPawnNum > 0)
        {
            playerPawnNum -= 1;
        }
    }

    public void ReduceAIPawn()
    {
        if (AIPawnNum > 0)
        {
            AIPawnNum -= 1;
        }
    }

    public bool IsNoPawnRemain()
    {
        return playerPawnNum == 0 && AIPawnNum == 0;
    }

    public int GetPlayerPawnNum()
    {
        return playerPawnNum;
    }

    public int GetAIPawnNum()
    {
        return AIPawnNum;
    }
}
