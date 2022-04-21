using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startButton : MonoBehaviour
{
    public Button playButton;
    public Rigidbody rb;
    private bool isRunning = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.Sleep();
        Button btn = playButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        if (isRunning == false) {
            rb.WakeUp();
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
