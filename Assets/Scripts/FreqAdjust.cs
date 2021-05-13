using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class FreqAdjust : MonoBehaviour
{
    // flag to be set what a hand has entered the trigger zone of the knob
    private bool hasEntered = false;
    //the device that is currently interating with the knob
    private InputDevice targetDevice;
    // set the default frequency for the ocillator
    private float frequency = 440.0f;
    // the previous position of the device while it is interacting with the knob
    private float prevDevicePosition = 0.0f;

    // used by osc to determine the current desirerd frequency
    public float getFreq()
    {
        float tempFreq = frequency;
        return tempFreq;
    }

    void assignTargetDevice(Collider other)
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevices(devices);
        
        //only assign a device if it is detected as either the right hand or the left hand
        foreach (InputDevice device in devices)
        {
            if (other.gameObject.name == "Left Hand" && device.name == "Oculus Touch Controller - Left")
                targetDevice = device;
            else if (other.gameObject.name == "Right Hand" && device.name == "Oculus Touch Controller - Right")
                targetDevice = device;
        }
    }

//this is a temp comment
    private void OnTriggerStay(Collider other)
    {
        assignTargetDevice(other);
        targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float trigValue);
        if (trigValue == 1.0f)
        {
            hasEntered = true;
            targetDevice.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 devicePos);
            prevDevicePosition = devicePos.z;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //get the grip state and device position every frame
        targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float trigValue);
        targetDevice.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 devicePos);

        if (hasEntered && trigValue == 1.0f)
        {            
            //subtract from frequency if moving hand backwards
            if(prevDevicePosition > devicePos.z)
            {
                frequency = frequency += 2.0f;
            }
            //add to frequency if moving hand forward
            else if (prevDevicePosition < devicePos.z)
            {
                frequency = frequency -=2.0f;
            }
            prevDevicePosition = devicePos.z;
        }   
        else
        {
            hasEntered = false;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }
}
