using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bakamori : MonoBehaviour
{
    public void StartRullet()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        StartCoroutine(Rullet());
    }

    public IEnumerator Rullet()
    {
        gameObject.GetComponent<Animator>().Play("Rullet");

        yield return new WaitForSeconds(3f);

        int random = Random.Range(0, 2);

        if (random == 1)
        {
            gameObject.GetComponent<Animator>().Play("Sucesses");
        }
        else
        {
            gameObject.GetComponent<Animator>().Play("Fail");
        }

        AoiManager.instance.BakaMoriBuffStart(random);
    }
}
