using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] int maxHp;
    [SerializeField] int hp;
    [SerializeField] float atk;
    [SerializeField] float cri;
    [SerializeField] public float exp;

    [SerializeField] int shield;
    [SerializeField] public int haloShield;
    [SerializeField] int maxShield;

    [SerializeField] GameObject hpBar;

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
        hp = (int)DictionaryManager.instance.CharacterInfoOutput(GameManager.instance.charNum).Hp + (int)BuffDebuffManager.instance.statHp + (int)BuffDebuffManager.instance.shopHp;
        maxHp = hp;
        atk = DictionaryManager.instance.CharacterInfoOutput(GameManager.instance.charNum).Atk + BuffDebuffManager.instance.statAtk + BuffDebuffManager.instance.shopAtk;
        cri = DictionaryManager.instance.CharacterInfoOutput(GameManager.instance.charNum).Cri + BuffDebuffManager.instance.statCri + BuffDebuffManager.instance.shopCri;
    }

    private void Start()
    {
        movement = Movement.Idle;
        GameManager.instance.CharacterSpeed = DictionaryManager.instance.CharacterInfoOutput(GameManager.instance.charNum).Speed;
        if (BuffDebuffManager.instance.shopRecovery > 0)
        {
            StartCoroutine(Recovery());
        }
        hpBar.SetActive(false);
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
        if (BuffDebuffManager.instance.shopDefence > 0)
        {
            if (damage * BuffDebuffManager.instance.shopDefence <= 1)
            {
                damage -= 1;
            }
            else
            {
                damage -= (int)(damage * BuffDebuffManager.instance.shopDefence);
            }
        }
        if (shield > damage)
        {
            shield -= (int)damage;
        }
        else
        {
            hp += (int)(shield-damage);
            shield = 0;
            StartCoroutine(HpBar());
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

    private IEnumerator Recovery()
    {
        while (true)
        {
            while (GameManager.instance.state)
            {
                if (hp < maxHp)
                {
                    hp += (int)BuffDebuffManager.instance.shopRecovery;
                }
                yield return new WaitForSeconds(5);
            }
            if (GameManager.instance.monsterSpawn == false)
            {
                yield break;
            }
            yield return null;
        }
    }

    private IEnumerator HpBar()
    {
        hpBar.SetActive(true);
        while (true)
        {
            int count = 0;

            while (GameManager.instance.state)
            {
                if (hp == maxHp)
                {
                    count++;
                    if (count > 3)
                    {
                        hpBar.SetActive(false);
                        yield break;
                    }
                    yield return new WaitForSeconds(1f);
                }
                hpBar.transform.GetChild(0).GetComponent<Slider>().value = (float)hp / (float)maxHp;

                yield return null;
            }
            yield return null;
        }
    }
}