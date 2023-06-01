using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arkanoid_BrickController : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject); // Destroy the brick gameObject
        }
    }
}
