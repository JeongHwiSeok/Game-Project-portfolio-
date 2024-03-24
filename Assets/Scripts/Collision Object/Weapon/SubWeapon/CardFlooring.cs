using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFlooring : Weapon
{
    [SerializeField] Vector3 target;
    [SerializeField] Vector3 direction;

    [SerializeField] public float time;

    [SerializeField] public int itemLV;

    [SerializeField] RuntimeAnimatorController[] animators;
    [SerializeField] Sprite[] sprites;

    private void OnEnable()
    {
        int random = Random.Range(0, 2);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[random];
        gameObject.GetComponent<Animator>().runtimeAnimatorController = animators[random];
        StartCoroutine(DisableOff());
    }

    public void StatInput(float a, float b, float c)
    {
        atk = a;
        normalspeed = b;
        knockBack = c;
        SpeedUP();
    }

    public void ColliderON()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    private IEnumerator DisableOff()
    {
        yield return new WaitForSeconds(time);

        gameObject.SetActive(false);
    }
}
