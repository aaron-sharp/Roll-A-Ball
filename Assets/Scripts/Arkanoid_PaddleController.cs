using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arkanoid_PaddleController : MonoBehaviour
{
    public float speed = 5f;  // The speed at which the paddle moves

    private float screenWidth;  // Width of the screen

    public float maxBounceAngle = 60f;  // The maximum bounce angle in degrees

    public GameObject ballPrefab;  // Reference to the ball prefab

    public Transform ballSpawn;  // Reference to the ball spawn position

    private GameObject ball;  // Reference to the spawned ball object

    public Text loseText;

    void Start()
    {
        // Get the width of the screen in world coordinates
        //float screenHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;
        //screenWidth = screenHalfWidth * 2f;

        SpawnBall();

        Arkanoid_Lose arkanoidLose = FindObjectOfType<Arkanoid_Lose>();
        if (arkanoidLose != null)
        {
            arkanoidLose.BallDestroyed += HandleBallDestroyed;
        }
    }


    void FixedUpdate()
    {
        // Move the ball with the paddle until launched
        if (ball != null)
        {
            ball.transform.position = ballSpawn.position;
        }
    }

    void Update()
    {
        // Get horizontal input (e.g., arrow keys or A/D keys)
        float moveInput = Input.GetAxis("Horizontal");


        // Calculate the new position of the paddle
        float newPosition = transform.position.x + moveInput * speed * Time.deltaTime;

        // Update the paddle position
        transform.position = new Vector3(newPosition, transform.position.y, transform.position.z);

        // Check if ball exists and spacebar is pressed to launch the ball
        if (ball != null && Input.GetKeyDown(KeyCode.Space))
        {
            LaunchBall();
        }


    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Calculate the new bounce direction based on the collision point
            Vector3 hitPoint = collision.contacts[0].point;
            float paddleWidth = transform.localScale.x;
            float relativeIntersectX = transform.position.x - hitPoint.x;
            float normalizedRelativeIntersectX = relativeIntersectX / (paddleWidth / 2f);
            float bounceAngle = normalizedRelativeIntersectX * maxBounceAngle;

            // Calculate the new ball velocity based on the bounce angle
            Rigidbody ballRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 newVelocity = Quaternion.Euler(0f, bounceAngle, 0f) * ballRb.velocity;
            ballRb.velocity = newVelocity;
        }



    }

    void LaunchBall()
    {
        ball.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 5f);
        ball = null;
        loseText.enabled = false;
    }


void SpawnBall()
    {
        ball = Instantiate(ballPrefab, ballSpawn.position, Quaternion.identity);
    }
    private void HandleBallDestroyed()
    {
        SpawnBall();
    }

}
