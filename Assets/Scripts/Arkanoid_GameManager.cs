using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Arkanoid_GameManager : MonoBehaviour
{

    public Text loseText;
    public Text winText;
    void Start()
    {
        winText.enabled = false;
        loseText.enabled = false;
        
        Arkanoid_Lose arkanoidLose = FindObjectOfType<Arkanoid_Lose>();
        if (arkanoidLose != null)
        {
            arkanoidLose.BallDestroyed += HandleBallDestroyed;
        }

    }

    private void HandleBallDestroyed()
    {
        loseText.enabled = true;
    }
}
