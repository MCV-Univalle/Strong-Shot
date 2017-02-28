using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteAnimationTrigger : MonoBehaviour
{
    public void StartAnimation()
    {
        gameObject.GetComponent<Animator>().enabled = true;
    }

    public void StopAnimation()
    {
        gameObject.GetComponent<Animator>().enabled = false;
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(150, 150);
    }
}
