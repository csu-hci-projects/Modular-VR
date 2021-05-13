using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

/*
 * This is the input handler basically
 * 
 */



public class handPresence : MonoBehaviour
{
    public InputDeviceCharacteristics controllerCharacteristics;
    public GameObject handModelPrefab; // will be provided in unity as either left or right hand

    private InputDevice targetDevice;
    private GameObject spawnedHandModel;
    private Animator handAnimator;
    
    public GameObject inventoryPrefab;
    private GameObject spawnedInventoryModel;

    private bool handActive = true;
    private bool invActive = false;

    void Start()
    {
       
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
            spawnedHandModel = Instantiate(handModelPrefab, transform);
            spawnedHandModel.SetActive(handActive);
            handAnimator = spawnedHandModel.GetComponent<Animator>();

            // instantiate the inventory game object  (toggled by pressing the primary button)
            spawnedInventoryModel = Instantiate(inventoryPrefab, transform);
            spawnedInventoryModel.SetActive(invActive);
        }
    }

    void updateHandAnimation()
    {
        if(targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }
    }

    void toggleInv(bool primaryButtonVal)
    {
        if (primaryButtonVal)
        {
            handActive = !handActive;
            invActive = !invActive;
            spawnedHandModel.SetActive(handActive);
            spawnedInventoryModel.SetActive(invActive);

        }
    }

    // Update is called once per frame
    void Update()
    {
        //todo set a public member variable isGripping for triggers to tell if user is gripping or not
        updateHandAnimation();
        targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue);
        toggleInv(primaryButtonValue);
    }
}
