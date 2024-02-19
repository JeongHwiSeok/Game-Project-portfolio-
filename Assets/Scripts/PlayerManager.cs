using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Movement
{
    Idle,
    Move,
    Attack
}

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance
    {
        get;
        private set;
    }

    // [SerializeField] public SpriteRenderer renderer;
    [SerializeField] public Animator animator;
    [SerializeField] public Movement movement;
    [SerializeField] public Vector2 pos;

    private void OnEnable()
    {
        // InputManager.instance.keyAction += Move;
    }

    private void Start()
    {
        instance = this;
        InputManager.instance.keyAction += Move;
        movement = Movement.Idle;
    }

    private void Update()
    {
        Status();
    }
    private void Move()
    {
        if(GameManager.instance.state == false)
        {
            return;
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            transform.localEulerAngles = new Vector3(0, 180, 0);
            movement = Movement.Move;
            Status();
        }
        else if(Input.GetAxis("Horizontal") > 0)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
            movement = Movement.Move;
            Status();
        }
        else if(Input.GetAxis("Vertical") != 0)
        {
            movement = Movement.Move;
            Status();
        }
    }

    public void Idle()
    {
        movement = Movement.Idle;
    }

    private void Status()
    {
        switch(movement)
        {
            case Movement.Idle:
                animator.Play("Idle");
                break;
            case Movement.Move:
                animator.Play("run");
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CollisionObject collisionObject = collision.GetComponent<CollisionObject>();

        if(collisionObject != null)
        {
            collisionObject.Activate(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CollisionObject collisionObject = collision.GetComponent<CollisionObject>();

        if (collisionObject != null)
        {
            collisionObject.UnActivate(this);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CollisionObject collisionObject = collision.gameObject.GetComponent<CollisionObject>();

        ContactPoint2D contact = collision.contacts[0];

        pos = contact.normal;

        if (collisionObject != null)
        {
            collisionObject.CollisionActivate(this);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        CollisionObject collisionObject = collision.gameObject.GetComponent<CollisionObject>();

        if (collisionObject != null)
        {
            collisionObject.CollisionUnActivate(this);
        }
    }


    private void OnDisable()
    {
        InputManager.instance.keyAction -= Move;
    }
}
