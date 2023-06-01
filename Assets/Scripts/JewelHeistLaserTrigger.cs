using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JewelHeistLaserTrigger : MonoBehaviour
{
    public GameObject laserObject;
    private Animator laserAnimator;

    // Start is called before the first frame update
    void Awake()
    {
        laserAnimator = laserObject.GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Trigger the laser animation
            laserAnimator.SetTrigger("Activate");
        }
    }
}
