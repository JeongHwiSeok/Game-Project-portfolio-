using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] Vector3 currentPosition;
    [SerializeField] Vector3 previusPosition;

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

    [SerializeField] int dotDamage;

    protected virtual void OnEnable()
    {
        parent = transform.parent.parent.GetChild(11);
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
        dropItemManager = transform.parent.parent.GetChild(11).GetComponent<DropItemManager>();
        speed = GameManager.instance.MonsterSpeed;
        StartCoroutine(LastPosition());
    }

    protected virtual void Update()
    {   
        if(GameManager.instance.state)
        {
            if (hp <= 0)
            {
                timeCheck += Time.deltaTime;
                transform.Translate(previusPosition * Time.deltaTime);
                spriteRenderer.color = new Color(1, 1, 1, 0.5f);

                if (timeCheck >= 0.2f)
                {
                    timeCheck = 0;
                    DevilsTail.instance.GetComponent<DevilsTail>().MonsterRemove(gameObject);
                    gameObject.SetActive(false);
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
            Chickennuggie.instance.Count++;
        }
        if(firstCheck)
        {
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
                int count = Random.Range(1, 1000);
                if (count <= PlayerManager.instance.Cri * 10)
                {
                    hp -= (int)((weapon.Atk * PlayerManager.instance.Atk * PlayerManager.instance.spAtk) * 1.5f);
                }
                else
                {
                    hp -= (int)(weapon.Atk * PlayerManager.instance.Atk * PlayerManager.instance.spAtk);
                }
                if (collision.GetComponent<EternityFlameBullet>() != null)
                {
                    dotDamage = 1;
                    StartCoroutine(FlameDot());
                }
                if (collision.GetComponent<SoundWave>() != null)
                {
                    StartCoroutine(Slow(collision.GetComponent<SoundWave>().Slow));
                }
                knockBack = weapon.KnockBack;
                if(hp > 0)
                {
                    damageTime = 0.2f;
                }
            }
            if (player != null)
            {
                player.Damage(atk);

                for (int i = 0; i < player.transform.GetChild(2).GetChild(2).GetComponent<SupportItemManager>().ListCount(); i++)
                {
                    if (player.transform.GetChild(2).GetChild(2).GetComponent<SupportItemManager>().ResearchList(i).GetComponent<ClockHat>() != null)
                    {
                        if (player.transform.GetChild(2).GetChild(2).GetComponent<SupportItemManager>().ResearchList(i).GetComponent<ClockHat>().flag)
                        {
                            player.transform.GetChild(2).GetChild(2).GetComponent<SupportItemManager>().ResearchList(i).GetComponent<ClockHat>().Activate();
                            return;
                        }
                    }
                }
                for (int i = 0; i < player.transform.GetChild(2).GetChild(2).GetComponent<SupportItemManager>().ListCount(); i++)
                {
                    if (player.transform.GetChild(2).GetChild(2).GetComponent<SupportItemManager>().ResearchList(i).GetComponent<SpaceFood>() != null)
                    {
                        if (player.transform.GetChild(2).GetChild(2).GetComponent<SupportItemManager>().ResearchList(i).GetComponent<SpaceFood>().flag)
                        {
                            player.transform.GetChild(2).GetChild(2).GetComponent<SupportItemManager>().ResearchList(i).GetComponent<SpaceFood>().Activate();
                            return;
                        }
                    }
                }
            }
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

    private IEnumerator Slow(float slow)
    {
        speed *= slow;
        yield return new WaitForSeconds(3f);
        speed /= slow;
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
