using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarAnimation2 : AnimationController
{
    protected virtual IEnumerator EndAnim()
    {
        yield return new WaitForSeconds(Random.Range(0,10));

        anim.Play("Iffect-star 2");
    }
}
