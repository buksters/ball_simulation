using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation; // You'll need to include this namespace

[RequireComponent(typeof(PathCreator))]
public class GeneratePathExample : MonoBehaviour {

    public bool closedLoop = true;
    public Transform[] waypoints;

    void Start () {
        if (waypoints.Length > 0) {
            // Create a new bezier path from the waypoints.
            BezierPath bezierPath = new BezierPath (waypoints, closedLoop, PathSpace.xyz);
            GetComponent<PathCreator> ().bezierPath = bezierPath;
        }
    }
}

