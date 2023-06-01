using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JewelHeist_AnimationRandomizer : MonoBehaviour
{
    public float minAnimationTime = 1.0f;
    public float maxAnimationTime = 3.0f;
    public float minStartDelay = 0.0f;
    public float maxStartDelay = 2.0f;

    private Animator animator;

    private float animationTime;
    private float startDelay;

    void Awake()
    {
        animator = GetComponent<Animator>();
        animator = GetComponent<Animator>();
        animationTime = Random.Range(minAnimationTime, maxAnimationTime);
        startDelay = Random.Range(minStartDelay, maxStartDelay);

        animator.SetFloat("AnimationTime", animationTime);
        animator.SetFloat("StartDelay", startDelay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
