using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text countText;
    public Text winText;
    public GameObject explosionFX;
    public GameObject pickupFX;
    public LayerMask groundLayer; // Layer Mask to filter for ground objects

    private Rigidbody rb;
    private int count;
    private AudioSource audioSource;
    private Vector3 targetPos;
    private bool isMoving = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        count = 0;
        SetCountText();
        winText.text = "";
    }

	private void Update()
	{
		if (Input.GetMouseButton(0)) // Check if left mouse button is held down
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			if (Physics.Raycast(ray, out hit))
			{
				if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
				{
					targetPos = hit.point;
					isMoving = true;
				}
			}
		}
		else
		{
			isMoving = false; // Stop moving when mouse button is released
		}
	}


    private void FixedUpdate()
    {
        if (isMoving)
        {
            // Move the player towards the target position
            Vector3 direction = targetPos - rb.position;
            direction.Normalize();
            rb.AddForce(direction * speed);

            // Stop moving the player if it is close to the target position
            if (Vector3.Distance(rb.position, targetPos) < 0.5f)
            {
                isMoving = false;
            }
        }
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