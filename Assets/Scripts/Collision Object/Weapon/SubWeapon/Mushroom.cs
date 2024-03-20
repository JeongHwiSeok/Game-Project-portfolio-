using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Weapon
{
    [SerializeField] GameObject target;

    [SerializeField] Vector3 point;

    [SerializeField] Vector3 direction;

    [SerializeField] float subAtk;

    [SerializeField] float time;

    [SerializeField] bool flag;

    [SerializeField] public CircleCollider2D circleCollider2D;

    private void OnEnable()
    {
        atk = 0;
        flag = true;
        circleCollider2D = gameObject.GetComponent<CircleCollider2D>();
        circleCollider2D.enabled = false;
    }

    private void Update()
    {
        time += Time.deltaTime;
        
        if (GameManager.instance.state)
        {
            if (flag)
            {
                ImpactPoint();
            }
            else
            {
                Target();
                PositionStatus(direction);
            }
        }
    }

    protected virtual void Target()
    {
        direction = target.transform.position - transform.position;

        direction.z = 0f;
        direction.Normalize();
    }

    private void PositionStatus(Vector3 direction)
    {
        if (transform.position == direction)
        {
            atk = subAtk;
            circleCollider2D.enabled = true;
            gameObject.SetActive(false);
        }
        else
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    private void ImpactPoint()
    {
        float x0 = 0;
        float x1 = point.x;
        float distance = x1 - x0;
        float nextX = Mathf.MoveTowards(transform.position.x, x1, 1 * Time.deltaTime);
        float baseY = Mathf.Lerp(0, point.y, (nextX - x0) / distance);
        float arc = (nextX - x0) * (nextX - x1) / (-0.25f * distance * distance);
        Vector3 nextPosition = new Vector3(nextX, baseY + arc, 0);
        transform.position = nextPosition;
        if (nextPosition == point)
        {
            flag = false;
        }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Monster monster = collision.GetComponent<Monster>();

        if (monster != null)
        {
            target = monster.gameObject;
        }
    }
}
