using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startButton : MonoBehaviour
{
    public Button playButton;
    public Rigidbody rb;
    public GameObject currentCylinder;
    private MeshCollider cylinder;
    private bool isRunning = false;
    private Renderer cylRenderer;
    // Start is called before the first frame update
    void Start()
    {
        cylinder = currentCylinder.GetComponent<MeshCollider>();
        cylinder.enabled = true;
        rb = GetComponent<Rigidbody>();
        rb.Sleep();
        Button btn = playButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        cylRenderer = currentCylinder.GetComponent<Renderer>();
    }

    void TaskOnClick()
    {
        if (isRunning == false) {
            rb.WakeUp();
            cylinder.enabled = false;
            cylRenderer.material.SetColor("_Color", new Color(1f, 1f, 1f, .1f));
            isRunning = true;
        }
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
