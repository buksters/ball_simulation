using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementSimulator1 : MonoBehaviour
{
    private Scene parallelScene;
    private PhysicsScene parallelPhysicsScene;

    public Vector3 initalVelocity;
    public GameObject mainObject;
    public GameObject plane;
    private LineRenderer lineRenderer;

    private bool mainPhysics = true;

    void Start()
    {
        Physics.autoSimulation = false;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 1000;

        CreateSceneParameters createSceneParameters = new CreateSceneParameters(LocalPhysicsMode.Physics3D);
        parallelScene = SceneManager.CreateScene("ParallelScene", createSceneParameters);
        parallelPhysicsScene = parallelScene.GetPhysicsScene();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            SimulatePhysics();
            mainPhysics = false;
        }

        if (Input.GetMouseButtonUp(0)){
            mainPhysics = true;
            Shoot();
        }
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

        simulationObject.GetComponent<Rigidbody>().velocity = mainObject.GetComponent<Rigidbody>().velocity + initalVelocity;
        simulationObject.GetComponent<Rigidbody>().angularVelocity = mainObject.GetComponent<Rigidbody>().angularVelocity;
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            parallelPhysicsScene.Simulate(Time.fixedDeltaTime);
            lineRenderer.SetPosition(i, simulationObject.transform.position);
        }
        Destroy(simulationObject);
        Destroy(simulationPlane);
    }

    void Shoot()
    {
        mainObject.GetComponent<Rigidbody>().velocity += initalVelocity;
    }

}
