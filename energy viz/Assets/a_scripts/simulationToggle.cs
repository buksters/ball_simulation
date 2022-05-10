using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class simulationToggle : MonoBehaviour
{
    private Scene parallelScene;
    private PhysicsScene parallelPhysicsScene;

    public GameObject mainObject;
    public GameObject plane;
    Toggle simulate;
    public LineRenderer lineRenderer;

    private bool mainPhysics = true;
    

    void Start()
    {
        simulate = GetComponent<Toggle>();
        simulate.isOn = false;
        simulate.onValueChanged.AddListener(delegate {
            ToggleValueChanged(simulate);
        });
        // Physics.autoSimulation = false;
        lineRenderer = GetComponent<LineRenderer>();
        // lineRenderer.positionCount = 1000;
        // CreateSceneParameters createSceneParameters = new CreateSceneParameters(LocalPhysicsMode.Physics3D);
        // parallelScene = SceneManager.CreateScene("ParallelScene", createSceneParameters);
        // parallelPhysicsScene = parallelScene.GetPhysicsScene();

    }

    // public void OnChangeValue() 
    // {
    //     if (simulate.isOn == false) {
    //         simulate.isOn = true;
    //         Debug.Log("turned on");
    //     }
    //     else
    //     {
    //         simulate.isOn = false;
    //         Debug.Log("turned off");
    //     }
    // }

    public void ToggleValueChanged(Toggle change)
    {
        if (simulate.isOn) {
            Debug.Log("turned on");
            lineRenderer.positionCount = 1000;
            CreateSceneParameters createSceneParameters = new CreateSceneParameters(LocalPhysicsMode.Physics3D);
            parallelScene = SceneManager.CreateScene("ParallelScene", createSceneParameters);
            parallelPhysicsScene = parallelScene.GetPhysicsScene();
            SimulatePhysics();
        }
        else {
            Debug.Log("turned off");
            lineRenderer.positionCount = 0;
            // SceneManager.UnloadSceneAsync(parallelScene);
        }
    }

    void SimulatePhysics()
    {
        GameObject[] _collidables = GameObject.FindGameObjectsWithTag("Collidable");      //check for all objects tagged collidable in scene. More optimal routes but this is most user friendly
            foreach (GameObject GO in _collidables)   //duplicate all collidables and move them to the simulation
            {
                var newGO = Instantiate(GO, GO.transform.position, GO.transform.rotation);  
                SceneManager.MoveGameObjectToScene(newGO, parallelScene);
            }
        GameObject simulationObject = Instantiate(mainObject);
        GameObject simulationPlane = Instantiate(plane);

        SceneManager.MoveGameObjectToScene(simulationObject, parallelScene);
        SceneManager.MoveGameObjectToScene(simulationPlane, parallelScene);


        simulationObject.GetComponent<Rigidbody>().velocity = mainObject.GetComponent<Rigidbody>().velocity;
        simulationObject.GetComponent<Rigidbody>().angularVelocity = mainObject.GetComponent<Rigidbody>().angularVelocity;
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            parallelPhysicsScene.Simulate(Time.fixedDeltaTime);
            lineRenderer.SetPosition(i, simulationObject.transform.position);
        }

        SceneManager.UnloadSceneAsync(parallelScene);
        // Destroy(simulationObject);
        // Destroy(simulationPlane);
    }

}
