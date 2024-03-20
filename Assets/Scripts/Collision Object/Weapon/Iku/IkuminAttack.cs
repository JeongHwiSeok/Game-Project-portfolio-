using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IkuminAttack : Weapon
{
    [SerializeField] Vector3 point;
    [SerializeField] Vector3 direction;
    [SerializeField] Movement movement;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] AnimationCurve curve;

    [SerializeField] public Animator animator;

    private int count;
    private float time;
    [SerializeField] private float angle;

    private void OnEnable()
    {
        atk = 30;
        knockBack = 0.5f;
        point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        transform.position = new Vector3(0, 0, 0);
        speed = 5f;
        spriteRenderer = GetComponent<SpriteRenderer>();
        movement = Movement.Attack;
        time = 0;
    }

    private void Update()
    {
        time += Time.deltaTime;
        float x0 = 0;
        float x1 = point.x;
        float distance = x1 - x0;
        float nextX = Mathf.MoveTowards(transform.position.x, x1, speed * Time.deltaTime);
        float baseY = Mathf.Lerp(0, point.y, (nextX - x0) / distance);
        float arc = (nextX - x0) * (nextX - x1) / (-0.25f * distance * distance);
        Vector3 nextPosition = new Vector3(nextX, baseY + arc, 0);
        transform.position = nextPosition;
        if (nextPosition == point)
        {

        }
        Status();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DisableZone zone = other.GetComponent<DisableZone>();
        Monster monster = other.GetComponent<Monster>();

        if(zone != null)
        {
            gameObject.SetActive(false);
            //AttackDirection(Vector3.zero);
            //if (direction.x < 0)
            //{
            //    spriteRenderer.flipX = true;
            //}
            //movement = Movement.Move;
        }
    }

    private void Status()
    {
        switch (movement)
        {
            case Movement.Attack:
                animator.Play("Shoot");
                break;
            case Movement.Move:
                animator.Play("run");
                break;
        }
    }
}
