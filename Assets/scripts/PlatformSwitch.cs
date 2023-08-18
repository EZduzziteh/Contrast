using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlatformSwitch : Interactable
{
    public List<Moving_Platform> platformsToAffect = new List<Moving_Platform>();


    [ServerRpc(RequireOwnership = false)]
    public override void InteractServerRpc()
    {
        foreach (Moving_Platform platform in platformsToAffect)
        {
            if (platform.isFrozen.Value)
            {
                platform.isFrozen.Value = false;
            }
            else
            {
                platform.isFrozen.Value = true;

            }
        }
    }
}
