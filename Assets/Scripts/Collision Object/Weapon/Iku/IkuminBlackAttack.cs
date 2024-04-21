using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IkuminBlackAttack : Weapon
{
    [SerializeField] Vector3 direction;
    [SerializeField] bool move;

    private void OnEnable()
    {
        normalspeed = 5f;
        atk = 0f;
        knockBack = 1f;
        speed = normalspeed;
        move = true;
        direction = Random.insideUnitSphere;
        direction.z = 0;
        Target();
    }

    protected virtual void Update()
    {
        if (GameManager.instance.state && move)
        {
            Target();
            PositionStatus(direction);
        }
    }

    protected void Target()
    {
        if (direction.x >= 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    protected void PositionStatus(Vector3 direction)
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Monster>() != null || collision.GetComponent<BigMonster>() != null)
        {
            move = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<Animator>().enabled = false;
            gameObject.transform.GetChild(0).GetComponent<IkuminBlackBoom>().gameObject.SetActive(true);
            gameObject.transform.GetChild(0).GetComponent<Animator>().SetBool("Boom", true);
        }
    }
}
