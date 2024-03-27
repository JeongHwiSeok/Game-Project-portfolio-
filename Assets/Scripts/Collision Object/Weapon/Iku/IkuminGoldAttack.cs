using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IkuminGoldAttack : Weapon
{
    [SerializeField] Vector3 direction;
    [SerializeField] int attackCount;

    private void OnEnable()
    {
        normalspeed = 5f;
        atk = 30f * BuffDebuffManager.instance.shopSkillAtk;
        knockBack = 0f;
        speed = normalspeed;
        direction = Random.insideUnitSphere;
        direction.z = 0;
        Target();
    }

    protected virtual void Update()
    {
        if (GameManager.instance.state)
        {
            Target();
            PositionStatus(direction);
        }
        if (attackCount > 20)
        {
            gameObject.SetActive(false);
        }
    }

    protected void Target()
    {
        if (direction.x >= 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
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
            direction = Random.insideUnitSphere;
            direction.z = 0;
            Target();
        }
    }
}
