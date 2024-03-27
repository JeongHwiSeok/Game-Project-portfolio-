using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] Vector3 currentPosition;
    [SerializeField] Vector3 previusPosition;

    [SerializeField] Vector3 backPosition;

    [SerializeField] protected int maxHp;
    [SerializeField] protected int hp;
    protected int atk;
    protected float speed;
    protected float knockBack;

    [SerializeField] protected bool firstCheck = false;

    [SerializeField] protected Vector3 direction;
    [SerializeField] protected SpriteRenderer spriteRenderer;

    [SerializeField] protected List<GameObject> dropItem;

    [SerializeField] Weapon weapon;
    [SerializeField] PlayerManager player;

    [SerializeField] protected float timeCheck;
    [SerializeField] protected float damageTime;

    [SerializeField] protected Transform parent;

    private DropItemManager dropItemManager;

    [SerializeField] GameObject drop;

    [SerializeField] bool timeBack;

    [SerializeField] int dotDamage;

    [SerializeField] bool rainSlow;
    [SerializeField] bool rainDamage;
    [SerializeField] bool trickDamage;

    [SerializeField] bool menoDebuff;

    [SerializeField] GameObject secondHand;

    [SerializeField] GameObject fire;

    [SerializeField] bool effectTrigger;

    protected virtual void OnEnable()
    {
        parent = transform.parent.parent.GetChild(11);
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
        dropItemManager = transform.parent.parent.GetChild(11).GetComponent<DropItemManager>();
        speed = GameManager.instance.MonsterSpeed;
        rainSlow = false;
        rainDamage = false;
        trickDamage = false;
        timeBack = false;
        menoDebuff = false;
        effectTrigger = true;
        StartCoroutine(LastPosition());
        StartCoroutine(BackPosition());
    }

    protected virtual void Update()
    {   
        if(GameManager.instance.state)
        {
            if (hp <= 0)
            {
                if (effectTrigger)
                {
                    GameObject obj = Instantiate(fire, gameObject.transform.parent.parent.GetChild(13));
                    obj.transform.position = gameObject.transform.position;
                    effectTrigger = false;
                }
                timeCheck += Time.deltaTime;
                transform.Translate(previusPosition * Time.deltaTime);
                spriteRenderer.color = new Color(1, 1, 1, 0.5f);

                if (timeCheck >= 0.2f)
                {
                    timeCheck = 0;
                    
                    gameObject.SetActive(false);
                }
            }
            else if (timeBack)
            {
                int random = Random.Range(0, 100);
                if (random < 5)
                {
                    StartCoroutine(TimeBack());
                }
                else
                {
                    timeBack = false;
                }
            }
            else if (damageTime > 0)
            {
                damageTime -= Time.deltaTime;
                transform.Translate(previusPosition * Time.deltaTime * knockBack);
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
        if (transform.position.x >= 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
        direction = GameManager.instance.player.transform.position - transform.position;

        direction.z = 0f;
        direction.Normalize();
    }

    protected virtual void PositionStatus(Vector3 direction)
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    protected virtual void OnDisable()
    {
        hp = maxHp;
        spriteRenderer.flipX = false;
        spriteRenderer.color = new Color(1, 1, 1, 1);
        if (Chickennuggie.instance != null)
        {
            GameManager.instance.cnCount++;
        }
        if (firstCheck)
        {
            GameManager.instance.monsterCount++;
            if (GameManager.instance.charNum == 2)
            {
                int random = Random.Range(0, 10);
                if (random < MenoManager.instance.jewalRandom)
                {
                    MenoManager.instance.jewalCount++;
                }
            }
            if (GameManager.instance.charNum == 1)
            {
                if (IkuManager.instance.ikuminStackFlag)
                {
                    int random = Random.Range(0, 10);
                    if (random < 1)
                    {
                        IkuManager.instance.ikuminCount++;
                    }
                }         
            }
            DropItem();
        }
        else
        {
            firstCheck = true;
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        weapon = collision.GetComponentInParent<Weapon>();
        player = collision.GetComponent<PlayerManager>();

        if(GameManager.instance.state)
        {
            if (weapon != null)
            {
                if (collision.GetComponent<MenoBullet>() != null)
                {
                    if (GameManager.instance.attackLV > 3)
                    {
                        StartCoroutine(MomentSlow(0.3f));
                    }
                    if (GameManager.instance.attackLV > 6)
                    {
                        menoDebuff = true;
                    }
                }
                if (collision.GetComponent<MenoLaser>() != null || collision.GetComponent<ChargeBullet>() != null)
                {
                    weapon.Atk *= 1.5f;
                }
                if (collision.GetComponent<MinuteHand>() != null || collision.GetComponent<HourHand>() != null)
                {
                    AoiManager.instance.attackCount++;

                    if (DataManager.instance.subArray[0, 7] == 1)
                    {
                        StartCoroutine(MomentSlow(0.1f));
                    }
                    else if (DataManager.instance.subArray[0, 7] == 2)
                    {
                        StartCoroutine(MomentSlow(0.2f));
                    }
                    else
                    {
                        StartCoroutine(MomentSlow(0.3f));
                    }
                }
                if (collision.GetComponent<UnisonWeapon>() != null && GameManager.instance.attackLV == 7)
                {
                    timeBack = true;
                    secondHand = collision.GetComponent<UnisonWeapon>().SecondHand();
                }
                if (collision.GetComponent<EternityFlameBullet>() != null)
                {
                    dotDamage = 1;
                    StartCoroutine(FlameDot());
                }
                if (collision.GetComponent<SoundWave>() != null)
                {
                    StartCoroutine(MomentSlow(collision.GetComponent<SoundWave>().Slow));
                }
                if (collision.GetComponent<RainFlooring>() != null)
                {
                    if (collision.GetComponent<RainFlooring>().itemLV == 7)
                    {
                        rainSlow = true;
                        rainDamage = true;
                        StartCoroutine(ContinueDamageRain((int)(weapon.Atk * BuffDebuffManager.instance.shopDamage)));
                        StartCoroutine(ContinueSlow());
                    }
                    else
                    {
                        rainDamage = true;
                        StartCoroutine(ContinueDamageRain((int)(weapon.Atk * BuffDebuffManager.instance.shopDamage)));
                    }
                }
                if (collision.GetComponent<CardFlooring>() != null)
                {
                    trickDamage = true;
                    StartCoroutine(ContinueDamageCard((int)(weapon.Atk * BuffDebuffManager.instance.shopDamage)));
                }
                Debug.Log("Weapon : " + weapon.name + " / Atk : " + weapon.Atk);
                knockBack = weapon.KnockBack;
                int count = Random.Range(1, 1000);
                if (count <= PlayerManager.instance.Cri * 10)
                {
                    hp -= (int)((weapon.Atk * PlayerManager.instance.Atk * BuffDebuffManager.instance.spAttackBuff) * 1.5f * BuffDebuffManager.instance.shopDamage);
                }
                else
                {
                    hp -= (int)(weapon.Atk * PlayerManager.instance.Atk * BuffDebuffManager.instance.spAttackBuff * BuffDebuffManager.instance.shopDamage);
                }
                if (hp > 0)
                {
                    damageTime = 0.2f;
                }
                if (collision.GetComponent<IkuminAttack>() != null)
                {
                    collision.GetComponent<IkuminAttack>().damageFlag = true;
                }
            }
            if (player != null)
            {
                player.Damage(atk);

                if (GameManager.instance.charNum == 2)
                {
                    if (MenoManager.instance.hologramCount > 0)
                    {
                        if (MenoManager.instance.hologram[MenoManager.instance.hologramCount - 1].activeSelf)
                        {
                            for (int i = 0; i < MenoManager.instance.hologramCount; i++)
                            {
                                MenoManager.instance.hologram[i].SetActive(false);
                            }
                            MenoManager.instance.HolograminvincibilityStart();
                        }
                    }
                }
                if (ClockHat.instance != null)
                {
                    ClockHat.instance.Activate();
                    return;
                }
                if (SpaceFood.instance != null)
                {
                    SpaceFood.instance.Activate();
                    return;
                }
            }
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<RainFlooring>() != null)
        {
            rainSlow = false;
            rainDamage = false;
        }
    }

    private IEnumerator FlameDot()
    {
        while (true)
        {
            while (gameObject.activeSelf && GameManager.instance.state)
            {
                hp -= dotDamage;

                yield return new WaitForSeconds(1);
            }
            yield return null;
        }
    }

    private IEnumerator LastPosition()
    {
        while (true)
        {
            previusPosition = currentPosition;
            currentPosition = transform.position;

            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator BackPosition()
    {
        while (true)
        {
            backPosition = transform.position;

            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator MomentSlow(float slow)
    {
        speed *= slow;
        yield return new WaitForSeconds(3f);
        speed /= slow;
        menoDebuff = false;
    }

    private IEnumerator ContinueSlow()
    {
        while (true)
        {
            while (GameManager.instance.state)
            {
                speed = GameManager.instance.MonsterSpeed * 0.5f;
                if (rainSlow != true)
                {
                    speed = GameManager.instance.MonsterSpeed;
                    yield break;
                }
                yield return null;
            }
            yield return null;
        }
    }

    private IEnumerator ContinueDamageRain(int damage)
    {
        while (true)
        {
            while (GameManager.instance.state)
            {
                hp -= damage;
                if (rainDamage != true)
                {
                    yield break;
                }
                yield return new WaitForSeconds(0.5f);
            }
            yield return null;
        }
    }
    private IEnumerator ContinueDamageCard(int damage)
    {
        while (true)
        {
            while (GameManager.instance.state)
            {
                hp -= damage;
                if (trickDamage != true)
                {
                    yield break;
                }
                yield return new WaitForSeconds(0.5f);
            }
            yield return null;
        }
    }

    private IEnumerator TimeBack()
    {
        speed = 0;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f);
        GameObject obj = Instantiate(secondHand);
        obj.transform.position = transform.position;
        yield return new WaitForSeconds(0.3f);
        timeBack = false;
        transform.position = backPosition;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        speed = GameManager.instance.MonsterSpeed;
    }

    private void DropItem()
    {
        int randomCount = Random.Range(1, 100);

        if(randomCount <= 5)
        {
            randomCount = Random.Range(1, 100);
            if(JewalBox.instance != null && randomCount <= 10)
            {
                drop = Instantiate(dropItem[4], parent);
            }
            else if(randomCount <= 95)
            {
                drop = Instantiate(dropItem[0], parent);
            }
            else
            {
                drop = Instantiate(dropItem[3], parent);
            }
        }
        else if(randomCount <= 90)
        {
            drop = Instantiate(dropItem[1], parent);
        }
        else
        {
            drop = Instantiate(dropItem[2], parent);
        }

        drop.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        
        if (drop.name != "PickUP-item(Clone)")
        {
            if (drop.name == "coin(Clone)")
            {
                dropItemManager.coinAdd(drop);
            }
            else if (drop.name == "RedCoin(Clone)")
            {
                dropItemManager.coinAdd(drop);
            }
            else
            {
                dropItemManager.dropAdd(drop);
            }
        }
    }
}
