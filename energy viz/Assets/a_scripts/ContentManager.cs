using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentManager : MonoBehaviour
{
    public GameObject PathPoints;
    public GameObject point;
    public Camera ARCamera;
    private GameObject addedPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("click registered");
            Ray ray = ARCamera.ScreenPointToRay(Input.mousePosition);
            Debug.Log(ray);
            addedPoint = Instantiate(point, ray.origin, Quaternion.identity);
            addedPoint.transform.parent = PathPoints.transform;

        }
    }
}
