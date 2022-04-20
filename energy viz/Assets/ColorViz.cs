using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorViz : MonoBehaviour
{
    public Text txt;
    Renderer sphereRenderer;
    private Color newSphereColor;
    private float potentialEnergy, kineticEnergy;
    private float green, blue;
    private float totalEnergy;

    //map(value, oldlow, oldhigh, newlow, newhigh)
    float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s-a1)*(b2-b1)/(a2-a1);
    }

    void Start()
    {
        sphereRenderer = GetComponent<Renderer>();
        txt = GetComponent<UnityEngine.UI.Text>();
    }

    void FixedUpdate()
    {
        float mass =  GetComponent<Rigidbody>().mass;
        Vector3 vel = GetComponent<Rigidbody>().velocity;
        potentialEnergy = -mass*Physics.gravity.y*transform.position.y;
        kineticEnergy = 0.5f*mass*(vel.y*vel.y + vel.x*vel.x);
        totalEnergy = potentialEnergy+kineticEnergy;
        green = map(potentialEnergy, 0f, totalEnergy, 0f, 1f);
        blue = map(kineticEnergy, 0f, totalEnergy, 0f, 1f);
        // txt.text = totalEnergy.ToString();

        newSphereColor = new Color(0f, green, blue, 1f);

        sphereRenderer.material.SetColor("_Color", newSphereColor);
    }
}
