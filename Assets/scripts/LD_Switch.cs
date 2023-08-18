using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class LD_Switch : Interactable
{
    [ServerRpc(RequireOwnership = false)]
    public override void InteractServerRpc()
    {
        LightDarkManager.instance.SwitchModes();
        base.InteractServerRpc();
    }
}
