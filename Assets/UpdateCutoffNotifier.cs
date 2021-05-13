using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UpdateCutoffNotifier : MonoBehaviour
{
    // knob object for checking the cutoff
    // to be passed in via the inspector
    public CuttoffAdjust knob;
    private string currentText;


    private void replaceText(){
        currentText = knob.currentCutoff.ToString();
        GetComponent<TextMeshPro>().text = currentText;
    }
    
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
