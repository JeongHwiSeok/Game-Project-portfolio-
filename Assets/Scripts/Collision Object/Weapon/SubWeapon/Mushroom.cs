using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Weapon
{
    [SerializeField] Animator animator;

    [SerializeField] Monster monster;

    [SerializeField] Vector3 point;

    [SerializeField] Vector3 direction;

    [SerializeField] float subAtk;
    [SerializeField] public float range;

    [SerializeField] float time;

    [SerializeField] bool move;

    private void OnEnable()
    {
        if (gameObject.transform.GetChild(0).GetComponent<MushRoomBoom>().gameObject.activeSelf)
        {
            gameObject.transform.GetChild(0).GetComponent<MushRoomBoom>().gameObject.SetActive(false);
        }    
        time = 0;
        move = false;
        animator.Play("Summon");
    }

    private void Update()
    {
        if (GameManager.instance.state && move)
        {
            time += Time.deltaTime;
            Target();
            PositionStatus(direction);
        }
    }

    public void Target()
    {
        direction = point - transform.position;

        direction.z = 0f;
        direction.Normalize();
    }

    private void PositionStatus(Vector3 direction)
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        Vector3 offset = transform.position - point;
        float sqrLen = offset.sqrMagnitude;

        if (sqrLen <= 0.05f || time > 10)
        {
            time = 0;
            move = false;
            animator.SetBool("Boom", true);
            gameObject.transform.GetChild(0).GetComponent<MushRoomBoom>().StatInput(subAtk, normalspeed, knockBack, range);
            gameObject.transform.GetChild(0).GetComponent<MushRoomBoom>().gameObject.SetActive(true);
        }
    }

    public void Run()
    {
        animator.SetBool("Run", true);
        move = true;
    }

    public void Point(Vector3 _target)
    {
        point = _target;
        Target();
    }

    public void StatInput(float a, float b, float c)
    {
        subAtk = a;
        normalspeed = b;
        knockBack = c;
        SpeedUP();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        monster = collision.GetComponent<Monster>();

        if (monster != null)
        {
            time = 0;
            move = false;
            animator.SetBool("Boom", true);
            gameObject.transform.GetChild(0).GetComponent<MushRoomBoom>().StatInput(subAtk, normalspeed, knockBack, range);
            gameObject.transform.GetChild(0).GetComponent<MushRoomBoom>().gameObject.SetActive(true);
        }
    }
}
