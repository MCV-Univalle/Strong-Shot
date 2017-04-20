using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {
    private float timeLapse;
    private float randomAnim;
    private Animator animator;

    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    void Update () {
        timeLapse += Time.deltaTime;
        randomAnim = Random.Range(0f, 1f);
        print(timeLapse);
        if (timeLapse > 20f)
        {
            if (randomAnim > 0.5f)
            {
                animator.Play("SideWave", -1, 0f);
            }
            else
            {
                animator.Play("Stretch", -1, 0f);
            }
            timeLapse = 0f;
        }
	}

    public void WordCorrect()
    {
        animator.Play("Hit", -1, 0f);
    }

    public void WordMiss()
    {
        animator.Play("Miss", -1, 0f);
    }

    public void EndGame()
    {
        animator.Play("Bye", -1, 0f);
    }
}
