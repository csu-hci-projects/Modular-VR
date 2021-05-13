using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateDiffusionNotifier : MonoBehaviour
{
    // Start is called before the first frame update
    
    public DiffusionAdjust knob;
    private string currentText;


    private void replaceText(){
        currentText = knob.currentDiffusion.ToString();
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
