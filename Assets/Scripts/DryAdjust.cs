using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class DryAdjust : MonoBehaviour
{
    //get Instance of the first ocillator that was spawned
    private GameObject ocillator;
    // flag to be set what a hand has entered the trigger zone of the knob
    private bool hasEntered = false;
    //the device that is currently interating with the knob
    private InputDevice targetDevice;
    // the previous position of the device while it is interacting with the knob
    private float prevDevicePosition = 0.0f;

    public float currentDryVal = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        ocillator = GameObject.Find("VCO-2(Clone)");
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
        //get the trigger state and device position every frame
        targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float trigValue);
        targetDevice.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 devicePos);
        AudioReverbFilter reverb = ocillator.GetComponent(typeof(AudioReverbFilter)) as AudioReverbFilter;

        if (hasEntered && trigValue == 1.0f)
        {
            //drt level: Mix level of dry signal in output in millibels (mB). Ranges from -10000.0 to 0.0. Default is 0.
            if (prevDevicePosition > devicePos.z){
                reverb.dryLevel += 250.0f;
                currentDryVal = reverb.dryLevel;
            }  
            else if (prevDevicePosition < devicePos.z) {
                reverb.dryLevel -= 250.0f;
                currentDryVal = reverb.dryLevel;
            }    
            prevDevicePosition = devicePos.z;
        }
        else hasEntered = false;

    }
       
}
