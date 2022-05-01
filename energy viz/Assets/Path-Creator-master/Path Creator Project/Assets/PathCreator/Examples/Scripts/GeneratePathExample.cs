using UnityEngine;

namespace PathCreation.Examples {
    // Example of creating a path at runtime from a set of points.

    [RequireComponent(typeof(PathCreator))]
    public class GeneratePathExample : MonoBehaviour {

        public bool closedLoop = true;
        public Transform[] waypoints;
        BezierPath bezierPath;

        void Start () {
            if (waypoints.Length > 0) {
                // Create a new bezier path from the waypoints.
                // bezierPath = new BezierPath(waypoints, closedLoop, PathSpace.xy);
                // GetComponent<PathCreator>().bezierPath = bezierPath;

                UpdatePath();
            }
            
        }

        // void Update () {
        //     bezierPath = new BezierPath(waypoints, closedLoop, PathSpace.xyz);
        //     GetComponent<PathCreator>().bezierPath = bezierPath;

        // }

        // void OnMouseDrag() {
        //     UpdatePath();
        // }


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