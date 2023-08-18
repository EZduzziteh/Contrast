using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Interactable : NetworkBehaviour {
   
    
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
    }



    [ServerRpc(RequireOwnership = false)]
    public virtual void InteractServerRpc()
    {
       // Debug.Log("interacting with"+this.gameObject);
    }

    
}
