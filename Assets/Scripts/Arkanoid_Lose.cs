using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Arkanoid_Lose : MonoBehaviour
{
    //public GameObject explosionPrefab;  // Reference to the explosion prefab

    public event Action BallDestroyed;  // Event to notify other scripts of ball destruction

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ExplodeBall(collision.gameObject);
        }
    }

    void ExplodeBall(GameObject ball)
    {
        //Instantiate(explosionPrefab, ball.transform.position, Quaternion.identity);
        Destroy(ball);
        // Trigger the BallDestroyed event
        BallDestroyed?.Invoke();

    }
}
