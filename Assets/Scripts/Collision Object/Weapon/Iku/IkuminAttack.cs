using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IkuminAttack : Weapon
{
    [SerializeField] Vector3 point;
    [SerializeField] Vector3 direction;
    [SerializeField] Movement movement;
    [SerializeField] SpriteRenderer spriteRenderer;

    [SerializeField] public Animator animator;

    private int count;

    private void OnEnable()
    {
        point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        transform.position = new Vector3(0, 0, 0);
        speed = 5f;
        spriteRenderer = GetComponent<SpriteRenderer>();
        movement = Movement.Attack;
        AttackDirection(point);
    }

    private void Update()
    {
        PositionStatus();
        Status();
    }

    private void AttackDirection(Vector3 position)
    {
        direction = position - transform.position;

        direction.z = 0f;
        direction.Normalize();
    }

    private void PositionStatus()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DisableZone zone = other.GetComponent<DisableZone>();
        PlayerManager player = other.GetComponent<PlayerManager>();
        
        if(zone != null)
        {
            AttackDirection(Vector3.zero);
            if (direction.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            movement = Movement.Move;
        }
        if(player != null && count < 1)
        {
            count++;
        }
        else if (player != null && count > 0)
        {
            count = 0;
            gameObject.SetActive(false);
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
