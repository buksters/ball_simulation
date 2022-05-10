//Attach this script to a Toggle GameObject. To do this, go to Create>UI>Toggle.
//Set your own Text in the Inspector window

using UnityEngine;
using UnityEngine.UI;

public class Example : MonoBehaviour
{
    Toggle m_Toggle;
    public Text m_Text;

    void Start()
    {
        //Fetch the Toggle GameObject
        m_Toggle = GetComponent<Toggle>();
        //Add listener for when the state of the Toggle changes, and output the state
        m_Toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(m_Toggle);
        });

        //Initialize the Text to say whether the Toggle is in a positive or negative state
        m_Text.text = "Toggle is : " + m_Toggle.isOn;
    }

    //Output the new state of the Toggle into Text when the user uses the Toggle
    void ToggleValueChanged(Toggle change)
    {
        m_Text.text =  "Toggle is : " + m_Toggle.isOn;
    }
}
