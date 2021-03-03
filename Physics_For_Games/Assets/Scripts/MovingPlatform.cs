using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public GameObject player;

    private bool playerOnPlatform = false;

    private void FixedUpdate()
    {
        if (!playerOnPlatform)
        {
            player.transform.SetParent(null);
        }

        if (playerOnPlatform)
        {
            player.transform.SetParent(gameObject.transform);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            playerOnPlatform = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerOnPlatform = false;
        }
    }
}
