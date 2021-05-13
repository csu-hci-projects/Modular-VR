using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UpdateFreqNotifier : MonoBehaviour
{
    // knob object for checking the frequency 
    // to be passed in via the inspector
    public FreqAdjust knob;
    private string currentText;


    private void replaceText(){
        currentText = knob.getFreq().ToString();
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
