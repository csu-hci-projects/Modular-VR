using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class SpawnTrigger : MonoBehaviour
{
    private InputDevice targetDevice;
    private GameObject spawnedObject;

    public GameObject objectPrefab;



    // Update is called once per frame
    void Update()
    {

    }

    void assignTargetDevice(Collider other)
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevices(devices);

        //need to assign the devices before we can check whether a certain input has occurred
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
        targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue);
        if (gripValue >= 0.5f && !spawnedObject)
        {
            targetDevice.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 devicePos);
            spawnedObject = Instantiate(objectPrefab, devicePos, Quaternion.identity);

        }
    }
}
