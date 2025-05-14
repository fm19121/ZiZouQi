/*using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhysicsSimulation : MonoBehaviour
{
    [SerializeField] private Transform obstacles;
    [SerializeField] private LineRenderer line;
    [SerializeField] private int linePoints;
    private Scene simulationScene;
    private PhysicsScene physicsScene;
    void Start()
    {
        CreateSimluationScene();
        line.positionCount = linePoints;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateSimluationScene()
    {
        simulationScene = SceneManager.CreateScene("Simulation", new CreateSceneParameters(LocalPhysicsMode.Physics3D));
        physicsScene = simulationScene.GetPhysicsScene();

        foreach(Transform obj in obstacles) 
        {
            GameObject gostObj = Instantiate(obj.gameObject, obj.position, obj.rotation);
            gostObj.GetComponent<Renderer>().enabled = false;
            SceneManager.MoveGameObjectToScene(gostObj, simulationScene);
        }
    }

    public GameObject AddObjectToSimulationScene(GameObject obj)
    {
        *//*GameObject gostObj = Instantiate(obj.gameObject, obj.transform.position, Quaternion.identity);
        gostObj.GetComponentInChildren<Renderer>().enabled = false;*//*
        //SceneManager.MoveGameObjectToScene(obj, simulationScene);
       // var gostObj = Instantiate(obj.gameObject, obj.transform.position, Quaternion.identity);
        SceneManager.MoveGameObjectToScene(obj, simulationScene);
        return obj;
    }

    public void SimulateTrajectory(GameObject obj)
    {

        GameObject gostObj = Instantiate(obj.gameObject, obj.transform.position, Quaternion.identity);
        SceneManager.MoveGameObjectToScene(gostObj, simulationScene);
        gostObj.GetComponentInChildren<Rigidbody>().AddForce(new Vector3(0.0f, 0.0f, 30), ForceMode.Impulse);
        line.positionCount = linePoints;
        for (int i = 0; i< linePoints; i++)
        {
            physicsScene.Simulate(Time.fixedDeltaTime);
            Debug.Log(gostObj.transform.position);
            line.SetPosition(i, gostObj.transform.position);
        }
    }
}*/
