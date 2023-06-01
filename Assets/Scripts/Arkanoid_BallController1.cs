using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arkanoid_BallController1 : MonoBehaviour
{
    public float initialSpeed = 5f;  // The initial speed of the ball

    private Rigidbody rb;
    private bool ballLaunched = false;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = 0f;  // Set the drag to zero to prevent slowing down
        rb.angularDrag = 0f;  // Set the angular drag to zero
    }

    void Update()
    {
        if (rb.velocity.y > 0)
        {
            // Clamp the velocity to only allow movement along the z and x axis
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        }

        if (!ballLaunched && Input.GetKeyDown(KeyCode.Space))
        {
            LaunchBall();
        }
    }

    void LaunchBall()
    {
        rb.velocity = transform.forward * initialSpeed;
        ballLaunched = true;
    }
}
