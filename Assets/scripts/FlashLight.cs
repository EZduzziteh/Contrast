using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : Interactable
{
    [SerializeField]
    GameObject lightBridge;



    public void ToggleLightBridge()
    {
        if (lightBridge.activeInHierarchy)
        {
            lightBridge.SetActive(false);
        }
        else
        {

            lightBridge.SetActive(true);
        }
    }



    public override void Interact()
    {
        ToggleLightBridge();
    }
}
