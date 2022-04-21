using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MovementSimulator1 : MonoBehaviour
{
    private Scene parallelScene;
    private PhysicsScene parallelPhysicsScene;

    public Vector3 initalVelocity;
    public GameObject mainObject;
    public GameObject plane;
    public Toggle simulate;
    public LineRenderer lineRenderer;

    private bool mainPhysics = true;
    

    void Start()
    {
        Physics.autoSimulation = false;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 1000;
        simulate.isOn = false;

        CreateSceneParameters createSceneParameters = new CreateSceneParameters(LocalPhysicsMode.Physics3D);
        parallelScene = SceneManager.CreateScene("ParallelScene", createSceneParameters);
        parallelPhysicsScene = parallelScene.GetPhysicsScene();

        GameObject[] _collidables = GameObject.FindGameObjectsWithTag("Collidable");      //check for all objects tagged collidable in scene. More optimal routes but this is most user friendly
        foreach (GameObject GO in _collidables)   //duplicate all collidables and move them to the simulation
        {
            var newGO = Instantiate(GO, GO.transform.position, GO.transform.rotation);  
            SceneManager.MoveGameObjectToScene(newGO, parallelScene);
        }
    }

    void Update()
    {
        if (simulate.isOn) {
            SimulatePhysics();
            // mainPhysics = false; 
        }

        // if (Input.GetMouseButtonDown(0)){
        //     SimulatePhysics();
        //     mainPhysics = false;
        // }

        // if (Input.GetMouseButtonUp(0)){
        //     mainPhysics = true;
        //     // Shoot();
        // }
    }

    void FixedUpdate(){

        if (mainPhysics)
            SceneManager.GetActiveScene().GetPhysicsScene().Simulate(Time.fixedDeltaTime);
    }

    void SimulatePhysics()
    {
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
        Destroy(simulationObject);
        Destroy(simulationPlane);
    }

    // void Shoot()
    // {
    //     mainObject.GetComponent<Rigidbody>().velocity += initalVelocity;
    // }

}
