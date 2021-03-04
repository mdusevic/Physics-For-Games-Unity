using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public GameObject player;
    Animator animator;

    private bool playerOnPlatform = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (!playerOnPlatform)
        {
            player.transform.SetParent(null);
        }

        if (playerOnPlatform)
        {
            player.transform.SetParent(gameObject.transform);
            animator.enabled = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player && !playerOnPlatform)
        {
            playerOnPlatform = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player && playerOnPlatform)
        {
            playerOnPlatform = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Terrain")
        {
            animator.enabled = false;
        }
    }
}
