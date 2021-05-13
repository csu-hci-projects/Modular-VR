using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//responsible for telling the oscillator which wave to produce according to the most recently hit button

public class MasterSwitcher : MonoBehaviour
{


    public ChangeOcillator sinButton;
    public ChangeOcillator triButton;
    public ChangeOcillator squareButton;

    public string currentOSC;

    // Start is called before the first frame update
    void Start()
    {
        currentOSC = "sin";   
    }

    // Update is called once per frame
    void Update()
    {
        if (sinButton.hasCollided)
        {
            Debug.Log("pressed the sin button");
            currentOSC = "sin";
            //immediately set to false to clear the value for later changes
            sinButton.hasCollided = false; 
        }
        else if (triButton.hasCollided)
        {
            currentOSC = "triangle";
            triButton.hasCollided = false;
        }
        else if (squareButton.hasCollided)
        {
            currentOSC = "square";
            squareButton.hasCollided = false;
        }
    }
}
