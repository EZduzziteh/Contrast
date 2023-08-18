using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSwitch : Interactable
{
    public List<Moving_Platform> platformsToAffect = new List<Moving_Platform>();
    public override void Interact()
    {
        foreach (Moving_Platform platform in platformsToAffect)
        {
            if (platform.isFrozen)
            {
                platform.isFrozen = false;
            }
            else
            {
                platform.isFrozen = true;

            }
        }
    }
}
