﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
	
	// Create public variables for player speed, and for the Text UI game objects
	public float speed;
	public Text countText;
	public Text winText;
	public GameObject explosionFX;
	public GameObject pickupFX;

	// Create private references to the rigidbody component on the player, and the count of pick up objects picked up so far
	private Rigidbody rb;
	private int count;
	private AudioSource audioSource;


	// from Roll-a-ball course
    private float movementX;
    private float movementY;

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }
	

	// At the start of the game..
	void Start ()
	{
		// Assign the Rigidbody component to our private rb variable
		rb = GetComponent<Rigidbody>();

		// Assign the AudioSource component to our private audioSource variable
		audioSource = GetComponent<AudioSource>();

		// Set the count to zero 
		count = 0;

		// Run the SetCountText function to update the UI (see below)
		SetCountText ();

		// Set the text property of our Win Text UI to an empty string, making the 'You Win' (game over message) blank
		winText.text = "";

	}


	// When this game object intersects a collider with 'is trigger' checked, 
	// store a reference to that collider in a variable named 'other'..
	void OnTriggerEnter(Collider other)
	{
		// ..and if the game object we intersect has the tag 'Pick Up' assigned to it..
		if (other.gameObject.CompareTag("Pick Up"))
		{
			// Make the other game object (the pick up) inactive, to make it disappear
			other.gameObject.SetActive(false);

			// Add one to the score variable 'count'
			count = count + 1;

			// Run the 'SetCountText()' function (see below)
			SetCountText();

			audioSource.Play();

			// play and destroy pickup fx
			var currentPickupFX = Instantiate(pickupFX, other.transform.position, other.transform.rotation);
			Destroy(currentPickupFX, 3);

		}


	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Enemy"))
		{
			// play explosion fx
			Instantiate(explosionFX, transform.position, Quaternion.identity);

			// Destroy the current object
			Destroy(gameObject);

			// Set the text to "You Lose!"
			winText.text = "You Lose!";
			AnimateText();

			// play explosion sound
			collision.gameObject.GetComponent<AudioSource>().Play();

			GameManager.Instance.ReloadScene();

			collision.gameObject.GetComponentInChildren<Animator>().SetFloat("Speed_f", 0);

			
		}
	}


	// Create a standalone function that can update the 'countText' UI and check if the required amount to win has been achieved
	void SetCountText()
	{
		// Update the text field of our 'countText' variable
		countText.text = "Count: " + count.ToString ();

		// Check if our 'count' is equal to or exceeded 12
		if (count >= 5) 
		{
			// Set the text value of our 'winText'
			winText.text = "You Win!";
			AnimateText();

			// remove enemy
			Destroy(GameObject.FindGameObjectWithTag("Enemy"));

			// remove this script so that you can't continue to get points and move around
			Destroy(GetComponent<PlayerController>());

			GameManager.Instance.ReloadScene();

		}
	}


	private void AnimateText()
    {
		if(winText.gameObject.GetComponent<Animator>())
			winText.gameObject.GetComponent<Animator>().SetTrigger("AnimateText");
    }

}