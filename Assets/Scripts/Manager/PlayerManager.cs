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

    [SerializeField] int hp;
    [SerializeField] float atk;
    [SerializeField] float cri;
    [SerializeField] public float exp;

    [SerializeField] int shield;
    [SerializeField] public int haloShield;
    [SerializeField] int maxShield;

    [SerializeField] public float pwsDamage;
    [SerializeField] public float spAtk;

    [SerializeField] public ShieldDeviceTypeHalo shieldDeviceTypeHalo;

    [SerializeField] WeaponManager weaponManager;

    public float time;

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
        InputManager.instance.keyAction += Move;
        spAtk = 1;
        pwsDamage = 1;
        shield = 0;
    }

    private void OnEnable()
    {
        hp = (int)DictionaryManager.instance.CharacterInfoOutput(GameManager.instance.charNum).Hp;
        atk = DictionaryManager.instance.CharacterInfoOutput(GameManager.instance.charNum).Atk;
        cri = DictionaryManager.instance.CharacterInfoOutput(GameManager.instance.charNum).Cri;
    }

    private void Start()
    {
        instance = this;
        movement = Movement.Idle;
        GameManager.instance.CharacterSpeed = DictionaryManager.instance.CharacterInfoOutput(GameManager.instance.charNum).Speed;
        weaponManager = GameObject.Find("Attack Manager").GetComponent<WeaponManager>();
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
        if (shieldDeviceTypeHalo != null)
        {
            if (shieldDeviceTypeHalo.FlagCheck() && shieldDeviceTypeHalo.TimeCheck())
            {
                shieldDeviceTypeHalo.Activate();
            }
            else
            {
                shieldDeviceTypeHalo.TimeReset();
            }
        }
        damage *= pwsDamage;
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

    private IEnumerator AoiBuff()
    {
        time = UIManager.instance.time;

        bool flag = true;

        while (UIManager.instance.time - time < 15)
        {
            if (flag)
            {
                for (int i = 0; i < weaponManager.ListCount(); i++)
                {
                    if (weaponManager.weaponsFind(i).activeSelf)
                    {
                        weaponManager.weaponsFind(i).GetComponent<Weapon>().aswSpeedBuff = 1.5f;
                        weaponManager.weaponsFind(i).GetComponent<Weapon>().SpeedUP();
                    }
                }
            }
            yield return null;
        }

        for (int i = 0; i < weaponManager.ListCount(); i++)
        {
            if (weaponManager.weaponsFind(i).activeSelf)
            {
                weaponManager.weaponsFind(i).GetComponent<Weapon>().aswSpeedBuff = 1f;
                weaponManager.weaponsFind(i).GetComponent<Weapon>().SpeedUP();
            }
        }
        
        AoiWeapon.instance.buffCheck = true;
    }

    public void AoiBuffStart()
    {
        StartCoroutine(AoiBuff());
    }

    private void OnDisable()
    {
        InputManager.instance.keyAction -= Move;
    }
}
