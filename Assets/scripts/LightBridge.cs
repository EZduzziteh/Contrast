using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class LightBridge : NetworkBehaviour
{
    
    public NetworkVariable<bool> isTurnedOn = new NetworkVariable<bool>();

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        isTurnedOn.OnValueChanged += OnValueChanged;
    }

    private void OnValueChanged(bool previousValue, bool newValue)
    {
        Debug.Log("brdige was: "+previousValue+" bridge is now: " + newValue);

        isTurnedOn.Value = newValue;
        if(isTurnedOn.Value == true)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }


   
    
}
