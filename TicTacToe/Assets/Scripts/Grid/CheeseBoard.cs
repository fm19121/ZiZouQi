using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheeseBoard : MonoBehaviour
{
    [SerializeField] private int unitSize;
    private GridObject[,] gridObjects;
    //private List<GridPosition> emptyGrids;
    public static CheeseBoard Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one CheeseBoard");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    void Start()
    {
        CreateCheeseBoard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateCheeseBoard()
    {
        gridObjects = new GridObject[3, 3];
       // emptyGrids = new List<GridPosition>(9);
        for(int x = 0; x < 3; x++)
        {
            for(int z = 0; z < 3; z++) 
            {
                gridObjects[x, z] = new GridObject(new GridPosition(x, z));
                //emptyGrids.Add(new GridPosition(x, z));
            }
        }
    }

    public GridObject[,] GetGridObjects()
    {
        return gridObjects;
    }

    public List<GridPosition> GetEmptyGrids()
    {
        List<GridPosition> emptyGrids = new List<GridPosition>(9);
        foreach(GridObject gridObj in gridObjects)
        {
            if(gridObj.GetPawn() == null)
            {
                emptyGrids.Add(gridObj.GetGridPosition());
            }
        }
        return emptyGrids;
    }
    public Vector3 GetWorldPosition(int x, int z)
    {
        return new Vector3(x, 0, z) * unitSize;
    }

    public Vector3 GetWorldPosition(GridPosition gridPos)
    {
        return new Vector3(gridPos.x, 0.0f, gridPos.z) * unitSize;
    }

    public GridPosition GetGridPosition(Vector3 worldPos)
    {
        return new GridPosition(Mathf.RoundToInt(worldPos.x / unitSize), Mathf.RoundToInt(worldPos.z / unitSize));
    }

    public GridObject GetGridObject(GridPosition gridPos)
    {
        return gridObjects[gridPos.x, gridPos.z];
    }

    public GridObject GetGridObject(Vector3 wordPos)
    {
       return GetGridObject(GetGridPosition(wordPos));
    }

    public bool IsPawnInGrid(GridPosition gridPos)
    {
        GridObject gridObject = gridObjects[gridPos.x, gridPos.z];
        if (gridObject.GetPawn() != null)
        {
            return true;
        }
        return false;
    }

    public bool IsInsideCheeseBoard(GridPosition gridPos)
    {
        return gridPos.x >= 0 && gridPos.z >= 0 && gridPos.x < 3 && gridPos.z < 3;
    }

    public bool IsInsideCheeseBoard(Vector3 worldPos)
    {
        GridPosition gridPos = GetGridPosition(worldPos);
        return IsInsideCheeseBoard(gridPos);
    }

    public int GetUnitSize()
    {
        return unitSize;
    }
}
