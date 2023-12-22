using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarAnimation1 : AnimationController
{
    protected override IEnumerator EndAnim()
    {
        while (true)
        {
            yield return new WaitForSeconds(0f);
            anim.Play("Iffectstar1");
            Debug.Log("EndAnim1");
        }
    }
}
