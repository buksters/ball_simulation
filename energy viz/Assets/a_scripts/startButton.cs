using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startButton : MonoBehaviour
{
    public Button playButton;
    public Button slowmoButton;
    public Button redoButton;
    public Text m_Text;
    public GameObject playIcon;
    public GameObject pauseIcon;
    [SerializeField] private GameObject Sphere_here;
    Rigidbody rb;
    public GameObject currentCylinder;
    private MeshCollider cylinder;
    private bool isRunning;
    private bool isSlowmo;
    private Renderer cylRenderer;
    private Vector3 initialPos;
    private Quaternion initialRot;
    // private GameObject redoSphere;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = Sphere_here.transform.position;
        initialRot = Sphere_here.transform.rotation;
        Button playbtn = playButton.GetComponent<Button>();
        playbtn.onClick.AddListener(TaskOnClick);
        Button slowmobtn = slowmoButton.GetComponent<Button>();
        slowmobtn.onClick.AddListener(SlowmoTask);
        Button redobtn = redoButton.GetComponent<Button>();
        redobtn.onClick.AddListener(RedoTask);
        // playButton = GetComponent<Button>();
        // m_Text = btn.GetComponentInChildren<Text>();
        m_Text.text = "Run";
        playIcon.SetActive(true);
        pauseIcon.SetActive(false);
        isRunning = false;
        isSlowmo = false;
        cylinder = currentCylinder.GetComponent<MeshCollider>();
        cylinder.enabled = true;
        rb = Sphere_here.GetComponent<Rigidbody>();
        rb.Sleep();
        cylRenderer = currentCylinder.GetComponent<Renderer>();
    }

    void RedoTask() 
    {
        Debug.Log("should redo");
        // Vector3 spherePos = Sphere_here.transform.position;
        // Quaternion sphereRot = Sphere_here.transform.rotation;
        Sphere_here.transform.position = initialPos;
        Sphere_here.transform.rotation = initialRot;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        
        Paused();
    }

    void SlowmoTask() 
    {
        if (Time.timeScale == 1) {
            Time.timeScale = 0.25f;
            isSlowmo = true;
            slowmoButton.GetComponent<Image>().color = new Color (1f,1f,1f,0.8f);
        }
        else if (Time.timeScale == 0.25f)
        {
            Time.timeScale = 1;
            isSlowmo = false;
            slowmoButton.GetComponent<Image>().color = new Color (1f,1f,1f,1f);
        }
    }

    void TaskOnClick()
    {

        if (!isRunning) {
            Debug.Log("should be running");
            m_Text.text = "Pause";
            Moving();
            // GameObject[] pathpoints = GameObject.FindGameObjectsWithTag("PathPoints");  //check for all objects tagged collidable in scene. More optimal routes but this is most user friendly
            // foreach (GameObject point in pathpoints)   //duplicate all collidables and move them to the simulation
            // {
            //     var collider = point.GetComponent<SphereCollider>();
            //     collider.enabled = false;
            // }
            // rb.WakeUp();
            // cylinder.enabled = false;
            // cylRenderer.material.SetColor("_Color", new Color(1f, 1f, 1f, .1f));
            // isRunning = true;
        }

        else {
            Debug.Log("should be paused");
            m_Text.text = "Run";
            Paused();
        }

    }

    void Moving() {
        playIcon.SetActive(false);
        pauseIcon.SetActive(true);
        GameObject[] pathpoints = GameObject.FindGameObjectsWithTag("PathPoints");  //check for all objects tagged collidable in scene. More optimal routes but this is most user friendly
        foreach (GameObject point in pathpoints)   //duplicate all collidables and move them to the simulation
        {
            var collider = point.GetComponent<SphereCollider>();
            collider.enabled = false;
        }
        if (Time.timeScale == 0) {
            if (isSlowmo) {
                Time.timeScale = .25f;
            }
            else {
                Time.timeScale = 1;
            }
        }
        else {
            rb.WakeUp();
        }
        cylinder.enabled = false;
        cylRenderer.material.SetColor("_Color", new Color(1f, 1f, 1f, .1f));
        isRunning = true;
    }

    void Paused() {
        playIcon.SetActive(true);
        pauseIcon.SetActive(false);
        Time.timeScale = 0;
        isRunning = false;
        GameObject[] pathpoints = GameObject.FindGameObjectsWithTag("PathPoints");  //check for all objects tagged collidable in scene. More optimal routes but this is most user friendly
        foreach (GameObject point in pathpoints)   //duplicate all collidables and move them to the simulation
        {
            var collider = point.GetComponent<SphereCollider>();
            collider.enabled = true;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
