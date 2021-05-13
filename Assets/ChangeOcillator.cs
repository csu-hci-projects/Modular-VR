using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeOcillator : MonoBehaviour
{

    public string oscType;
    private GameObject ocillator;
    public bool hasCollided = false;

    void OnTriggerEnter(Collider other)
    {
        hasCollided = true;
    }


    // Start is called before the first frame update
    void Start()
    {
        ocillator = GameObject.Find("VCO-2(Clone)");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
