using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //if any player hits a checkpoint, update for all players.
            foreach(PlayerControl player in FindObjectsOfType<PlayerControl>())
            {
                player.setCheckPoint(this);
            }
        }
    }
}
