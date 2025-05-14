using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridDisplay : MonoBehaviour
{
    public static GridDisplay Instance { get; private set; }

    [SerializeField] private GameObject singleGridUI;
    [SerializeField] private bool isClassicMode;
    private SingleGridDisplay[,] gridsUI;

    private GridObject selectedGrid;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("more than one GridDisplay");
            Destroy(Instance.gameObject);
            return;
        }
        Instance = this;
    }
    void Start()
    {
        selectedGrid = null;
        InitGridDispaly();
        //TurnManager.Instance.OnTurnChanged += UpdatePawnOnGrid;
    }

    // Update is called once per frame
    void Update()
    {
        if (isClassicMode)
        {
            UpdateSelectedGridRender();
        }
        else
        {
            UpdatePawnOnGrid();
        }
        
    }

    private void InitGridDispaly()
    {
        gridsUI = new SingleGridDisplay[3, 3];
        for (int x = 0; x < 3; x++)
        {
            for (int z = 0; z < 3; z++)
            {
                GridPosition gridPos = new GridPosition(x, z);
                GameObject region = Instantiate(singleGridUI, CheeseBoard.Instance.GetWorldPosition(gridPos), Quaternion.identity);
                gridsUI[x, z] = region.GetComponent<SingleGridDisplay>();
            }
        }
    }

    private void UpdateSelectedGridRender()
    {
        Vector3 curosrPos = Cursor.GetPosition();
        if (CheeseBoard.Instance.IsInsideCheeseBoard(curosrPos))
        {
            GridObject newSelectedGrid = CheeseBoard.Instance.GetGridObject(curosrPos);
            if (newSelectedGrid.GetPawn() == null && newSelectedGrid != selectedGrid)
            {
                if (selectedGrid != null)
                {
                    GetSingleGridUI(selectedGrid).NotSelected();
                }
                GetSingleGridUI(newSelectedGrid).BeSelected();
                selectedGrid = newSelectedGrid;
            }
        }
    }

    private void UpdatePawnOnGrid()
    {
        GridObject[,] gridObjects = CheeseBoard.Instance.GetGridObjects();
        for (int x = 0; x < 3; x++)
        {
            for (int z = 0; z < 3; z++)
            {
                if (gridObjects[x, z].GetPawn() != null)
                {
                    if (gridObjects[x, z].GetPawn().GetIsPlayer())
                    {
                        GetSingleGridUI(gridObjects[x, z]).SetIsPlayerPawnOnGrid();
                    }
                    else
                    {
                        GetSingleGridUI(gridObjects[x, z]).SetIsAIPawnOnGrid();
                    }
                }
                else
                {
                    GetSingleGridUI(gridObjects[x, z]).Default();
                }
            }
            
        }
    }
    private SingleGridDisplay GetSingleGridUI(GridObject gridObj)
    {
        GridPosition gridPos = gridObj.GetGridPosition();
        return gridsUI[gridPos.x, gridPos.z];
    }
}
