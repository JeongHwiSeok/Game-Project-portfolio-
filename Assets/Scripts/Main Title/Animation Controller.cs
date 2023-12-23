using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class AnimationController : MonoBehaviour
{
    [SerializeField] protected Animator anim;

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        anim.enabled = false;
        StartCoroutine(RandomDelay());
    }

    protected virtual IEnumerator RandomDelay()
    {
        yield return new WaitForSeconds(Random.Range(0, 5f));
        anim.enabled = true;
    }

    protected virtual IEnumerator EndAnim()
    {
        yield return new WaitForSeconds(Random.Range(0, 5f));

        anim.Play("Move", -1, 0f);
    }
}
