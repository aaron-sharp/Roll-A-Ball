using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JewelHeist_PlayerController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Destroy the player
            Destroy(gameObject);
        }
    }
}
