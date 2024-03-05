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

    [SerializeField] float hp;
    [SerializeField] float atk;
    [SerializeField] float cri;
    [SerializeField] public float exp;

    public float Hp
    {
        get { return hp; }
    }
    public float Atk
    {
        get { return atk; }
    }
    public float Cri
    {
        get { return cri; }
    }

    private void OnEnable()
    {
        // InputManager.instance.keyAction += Move;
        hp = DictionaryManager.instance.CharacterInfoOutput(GameManager.instance.charNum).Hp;
        atk = DictionaryManager.instance.CharacterInfoOutput(GameManager.instance.charNum).Atk;
        cri = DictionaryManager.instance.CharacterInfoOutput(GameManager.instance.charNum).Cri;
    }

    private void Start()
    {
        instance = this;
        InputManager.instance.keyAction += Move;
        movement = Movement.Idle;
        GameManager.instance.CharacterSpeed = DictionaryManager.instance.CharacterInfoOutput(GameManager.instance.charNum).Speed;
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

    public void Damage(float damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            GameManager.instance.GameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;

        if(obj.GetComponent<CollisionObject>() != null)
        {
            obj.GetComponent<CollisionObject>().Activate(this);
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
