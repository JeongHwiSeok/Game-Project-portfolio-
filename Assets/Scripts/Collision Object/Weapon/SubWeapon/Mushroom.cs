using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Weapon
{
    [SerializeField] public Vector3 target;
    [SerializeField] Animator animator;

    [SerializeField] Monster monster;

    [SerializeField] Vector3 point;

    [SerializeField] Vector3 direction;

    [SerializeField] float subAtk;

    [SerializeField] float time;

    [SerializeField] bool flag;
    [SerializeField] bool runFlag;

    [SerializeField] public CircleCollider2D circleCollider2D;

    private void OnEnable()
    {
        atk = 0;
        flag = true;
        runFlag = false;
        animator = gameObject.GetComponent<Animator>();
        circleCollider2D = gameObject.GetComponent<CircleCollider2D>();
        circleCollider2D.enabled = false;
        animator.Play("Summon");
    }

    private void Update()
    {
        time += Time.deltaTime;
        
        if (GameManager.instance.state)
        {
            if (runFlag)
            {
                PositionStatus(direction);
            }
        }
    }

    public void Target()
    {
        direction = target - transform.position;

        direction.z = 0f;
        direction.Normalize();

        atk = subAtk;
    }

    private void PositionStatus(Vector3 direction)
    {
        transform.Translate(direction * speed * Time.deltaTime);

        Vector3 offset = transform.position - target;
        float sqrLen = offset.sqrMagnitude;

        if (sqrLen <= 0.01f)
        {
            runFlag = false;
            circleCollider2D.enabled = true;
            animator.SetLayerWeight(1, 1);
            animator.Play("Boom");
        }
    }

    public void RunOn()
    {
        runFlag = true;
        animator.SetBool("Run", true);
    }

    public void Point(Vector3 _target)
    {
        point = _target;
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
}
