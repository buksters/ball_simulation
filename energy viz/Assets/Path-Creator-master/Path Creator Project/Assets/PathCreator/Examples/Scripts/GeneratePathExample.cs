using System;
using UnityEngine;
using PathCreation;

namespace PathCreation.Examples {
    // Example of creating a path at runtime from a set of points.

    [RequireComponent(typeof(PathCreator))]
    public class GeneratePathExample : MonoBehaviour {

        public bool closedLoop = true;
        public Transform[] waypoints;
        BezierPath bezierPath;
        Transform[] initial_waypoints;

        void Start () {

            if (waypoints.Length > 0) {

                // waypoints.CopyTo(initial_waypoints, 0);
                // initial_waypoints = (Transform[]) waypoints.Clone(); //only creates a shallow copy
                // Create a new bezier path from the waypoints.
                for (int i = 0; i < waypoints.Length; i++)
                    waypoints[i].hasChanged = false;

                bezierPath = new BezierPath(waypoints, closedLoop, PathSpace.xy);
                GetComponent<PathCreator>().bezierPath = bezierPath;

                // UpdatePath();
            }
            
        }

        void Update () {
            if (waypoints.Length > 0) {
                
                foreach(Transform point in waypoints)
                {
                    if(point.hasChanged)
                    {
                        Debug.Log("CHANGED");
                        bezierPath = new BezierPath(waypoints, closedLoop, PathSpace.xy);
                        GetComponent<PathCreator>().bezierPath = bezierPath;
                        point.hasChanged = false;
                        // Exit the loop early
                        break;
                    }
                }
                
                
                // if (pointsChanged()) {
                //     Debug.Log("CHANGED");
                //     bezierPath = new BezierPath(waypoints, closedLoop, PathSpace.xy);
                //     GetComponent<PathCreator>().bezierPath = bezierPath;
                // }
            }
        }

        // void OnMouseDrag() {
        //     UpdatePath();
        // }

        // void Update () {
        //     if (waypoints.Length > 0) {
            
        //         // bezierPath = new BezierPath(waypoints, closedLoop, PathSpace.xy);
        //         // GetComponent<PathCreator>().bezierPath = bezierPath;
        //     }
        // }

        bool pointsChanged () {
            if (waypoints.Length != initial_waypoints.Length) {
                return true;
            }
            for (int i = 0; i < waypoints.Length; i++) {
                if (waypoints[i] != initial_waypoints[i]) {
                    return true;
                }
            }
            return false;
        }


        void UpdatePath() {
            BezierPath bezierPath = new BezierPath(waypoints, closedLoop, PathSpace.xy);
            GetComponent<PathCreator>().bezierPath = bezierPath;

            // if (waypoints.Length > 0) {
            //     // Create a new bezier path from the waypoints.
            //     BezierPath bezierPath = new BezierPath(waypoints, closedLoop, PathSpace.xy);
            //     GetComponent<PathCreator>().bezierPath = bezierPath;
            // }

        }
    }
}