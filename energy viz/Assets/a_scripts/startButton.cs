using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startButton : MonoBehaviour
{
    public Button playButton;
    public Text m_Text;
    [SerializeField] private GameObject Sphere_here;
    Rigidbody rb;
    public GameObject currentCylinder;
    private MeshCollider cylinder;
    private bool isRunning;
    private Renderer cylRenderer;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = playButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        // playButton = GetComponent<Button>();
        // m_Text = btn.GetComponentInChildren<Text>();
        m_Text.text = "Run";
        isRunning = false;
        cylinder = currentCylinder.GetComponent<MeshCollider>();
        cylinder.enabled = true;
        rb = Sphere_here.GetComponent<Rigidbody>();
        rb.Sleep();
        cylRenderer = currentCylinder.GetComponent<Renderer>();
    }

    void TaskOnClick()
    {

        // if (!isRunning) {
            Debug.Log("should be running");
            m_Text.text = "Pause";
            GameObject[] pathpoints = GameObject.FindGameObjectsWithTag("PathPoints");  //check for all objects tagged collidable in scene. More optimal routes but this is most user friendly
            foreach (GameObject point in pathpoints)   //duplicate all collidables and move them to the simulation
            {
                var collider = point.GetComponent<SphereCollider>();
                collider.enabled = false;
            }
            rb.WakeUp();
            cylinder.enabled = false;
            cylRenderer.material.SetColor("_Color", new Color(1f, 1f, 1f, .1f));
            isRunning = true;
        // }

        // else {
        //     rb.Sleep();
        //     isRunning = false;
        // }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
