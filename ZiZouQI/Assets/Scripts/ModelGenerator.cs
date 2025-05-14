using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelGenerator : MonoBehaviour
{
    [SerializeField] GameObject playerPawn;
    [SerializeField] GameObject AIPawn;

    public static ModelGenerator Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one InstantiatePawn");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public GameObject CreatePlayerPawn(Vector3 pos)
    {
        return Instantiate(playerPawn, pos, Quaternion.identity);
    }

    public GameObject CreateAIPawn(Vector3 pos)
    {
        return Instantiate(AIPawn, pos, Quaternion.identity);
    }
}
