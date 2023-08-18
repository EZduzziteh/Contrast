using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class FlashLight : Interactable
{
    [SerializeField]
    LightBridge lightBridge;


    
    public void ToggleLightBridge()
    {
        if (lightBridge.isTurnedOn.Value == true)
        {
           
            lightBridge.isTurnedOn.Value = false;
        }
        else
        {

            lightBridge.isTurnedOn.Value = true;
        }
    }


    [ServerRpc(RequireOwnership = false)]
    public override void InteractServerRpc()
    {
        ToggleLightBridge();
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        
    }
}
