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
    }

    protected virtual void Start()
    {
        StartCoroutine(EndAnim());
    }

    protected virtual IEnumerator EndAnim()
    {
        Debug.Log("EndAnim");
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0, 10));
        }
    }
}
