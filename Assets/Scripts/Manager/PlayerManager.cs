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
    [SerializeField] public SpriteRenderer spriteRenderer;
    [SerializeField] public Vector2 pos;

    [SerializeField] int hp;
    [SerializeField] float atk;
    [SerializeField] float cri;
    [SerializeField] public float exp;

    [SerializeField] int shield;
    [SerializeField] public int haloShield;
    [SerializeField] int maxShield;

    public int Hp
    {
        set { hp = value; }
        get { return hp; }
    }
    public float Atk
    {
        set { atk = value; }
        get { return atk; }
    }
    public float Cri
    {
        set { cri = value; }
        get { return cri; }
    }
    public int Shield
    {
        set { shield = value; }
        get { return shield; }
    }
    public int MaxShield
    {
        get { return maxShield; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        InputManager.instance.keyAction += Move;
        shield = 0;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        hp = (int)DictionaryManager.instance.CharacterInfoOutput(GameManager.instance.charNum).Hp;
        atk = DictionaryManager.instance.CharacterInfoOutput(GameManager.instance.charNum).Atk;
        cri = DictionaryManager.instance.CharacterInfoOutput(GameManager.instance.charNum).Cri;
    }

    private void Start()
    {
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
            spriteRenderer.flipX = true;
            movement = Movement.Move;
            Status();
        }
        else if(Input.GetAxis("Horizontal") > 0)
        {
            spriteRenderer.flipX = false;
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
        if (ShieldDeviceTypeHalo.instance != null)
        {
            if (ShieldDeviceTypeHalo.instance.FlagCheck() && ShieldDeviceTypeHalo.instance.TimeCheck())
            {
                ShieldDeviceTypeHalo.instance.Activate();
            }
            else
            {
                ShieldDeviceTypeHalo.instance.TimeReset();
            }
        }
        damage *= BuffDebuffManager.instance.pwsDamageDebuff;
        if (shield > damage)
        {
            shield -= (int)damage;
        }
        else
        {
            hp += (int)(shield-damage);
            shield = 0;
        }
        if(hp <= 0)
        {
            GameManager.instance.GameOver();
        }
    }

    public void MaxShieldAdd()
    {
        maxShield = 0;
        maxShield += haloShield;
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