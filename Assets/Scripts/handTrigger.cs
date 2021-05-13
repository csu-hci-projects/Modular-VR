using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hello");
    }
}
