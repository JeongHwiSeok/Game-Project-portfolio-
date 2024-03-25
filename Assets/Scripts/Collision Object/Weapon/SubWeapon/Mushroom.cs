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

    [SerializeField] float time;

    [SerializeField] bool flag;
    [SerializeField] bool move;

    [SerializeField] public CircleCollider2D circleCollider2D;

    private void OnEnable()
    {
        atk = 0;
        flag = true;
        move = true;
        animator = gameObject.GetComponent<Animator>();
        circleCollider2D = gameObject.GetComponent<CircleCollider2D>();
        animator.Play("Summon");
    }

    private void Update()
    {
        if (GameManager.instance.state && move)
        {
            time += Time.deltaTime;
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
        transform.Translate(direction * speed * Time.deltaTime);

        Vector3 offset = transform.position - point;
        float sqrLen = offset.sqrMagnitude;

        if (sqrLen <= 0.05f || time > 10)
        {
            time = 0;
            atk = subAtk;
            move = false;
            animator.SetLayerWeight(1, 1);
            animator.SetBool("Boom", true);
            animator.Play("Boom");
        }
    }

    public void Run()
    {
        animator.SetBool("Run", true);
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

    public void Boom()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Monster monster = collision.GetComponent<Monster>();

        if (monster != null)
        {
            atk = subAtk;
            move = false;
            animator.SetLayerWeight(1, 1);
            animator.SetBool("Boom", true);
            animator.Play("Boom");
        }
    }
}
