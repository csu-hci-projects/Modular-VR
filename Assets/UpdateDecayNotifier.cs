using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateDecayNotifier : MonoBehaviour
{
    // knob object for checking the frequency 
    // to be passed in via the inspector
    public DecayAdjust knob;
    private string currentText;


    private void replaceText(){
        currentText = knob.currentDecay.ToString();
        GetComponent<TextMeshPro>().text = currentText;
    }
//
    // Start is called before the first frame update
    void Start()
    {
        replaceText();

    }

    // Update is called once per frame
    void Update()
    {
        replaceText();
    }
}
