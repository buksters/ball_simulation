using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class ColorViz : MonoBehaviour
{
    public Text txt;
    Renderer sphereRenderer;
    private Color newSphereColor;
    private Color newHaloColor;
    private float potentialEnergy, kineticEnergy;
    private float green, blue;
    private float totalEnergy;
    private float originalEnergy;
    private float mass;
    


    //map(value, oldlow, oldhigh, newlow, newhigh)
    float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s-a1)*(b2-b1)/(a2-a1);
    }

    void Start()
    {
        sphereRenderer = GetComponent<Renderer>();
        txt = GetComponent<UnityEngine.UI.Text>();
        mass =  GetComponent<Rigidbody>().mass;
        originalEnergy = -mass*Physics.gravity.y*transform.position.y;
    }

    void FixedUpdate()
    {

        // float mass =  GetComponent<Rigidbody>().mass;
        Vector3 vel = GetComponent<Rigidbody>().velocity;
        float Height = transform.position.y;
        potentialEnergy = -mass*Physics.gravity.y*transform.position.y;
        kineticEnergy = 0.5f*mass*(vel.y*vel.y + vel.x*vel.x);
        totalEnergy = potentialEnergy+kineticEnergy;
        green = map(potentialEnergy, 0f, totalEnergy, 0f, 1f);
        blue = map(kineticEnergy, 0f, totalEnergy, 0f, 1f);
        // txt.text = totalEnergy.ToString();

        newSphereColor = new Color(0f, green, blue, .5f);
        newHaloColor = new Color(0f, green, blue, 1f);

        sphereRenderer.material.SetColor("_Color", newSphereColor);
        
        // transform.localScale = new Vector3 (1.0001f, 1.0001f, 1.0001f); //changes size of ball

        // to represent energy loss
        float energyDelta = totalEnergy/originalEnergy;

        // change halo properties:
        SerializedObject halo = new SerializedObject(GetComponent("Halo"));
        halo.FindProperty("m_Size").floatValue = 2f*energyDelta;
        halo.FindProperty("m_Enabled").boolValue = true;
        halo.FindProperty("m_Color").colorValue = newHaloColor;
        halo.ApplyModifiedProperties();
    }
}
