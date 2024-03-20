using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMonster : MonoBehaviour
{
    [SerializeField] Vector3 currentPosition;
    [SerializeField] Vector3 previusPosition;

    [SerializeField] protected int maxHp;
    [SerializeField] protected int hp;
    protected int atk;
    protected float speed;
    protected float knockBack;

    [SerializeField] protected bool firstCheck = false;
    [SerializeField] protected bool redCoin = false;

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

    protected virtual void OnEnable()
    {
        parent = transform.parent.parent.GetChild(11);
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
        dropItemManager = transform.parent.parent.GetChild(11).GetComponent<DropItemManager>();
        StartCoroutine(LastPosition());
    }

    protected virtual void Update()
    {
        if (GameManager.instance.state)
        {
            if (hp <= 0)
            {
                timeCheck += Time.deltaTime;
                transform.Translate(previusPosition * Time.deltaTime);
                spriteRenderer.color = new Color(1, 1, 1, 0.5f);

                if (timeCheck >= 0.2f)
                {
                    timeCheck = 0;
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
        if (firstCheck)
        {
            DropItem();
        }
        else
        {
            firstCheck = true;
        }
    }

    protected virtual void OnDestroy()
    {
        DropItem();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        weapon = collision.GetComponentInParent<Weapon>();
        player = collision.GetComponent<PlayerManager>();

        if (GameManager.instance.state)
        {
            if (weapon != null)
            {
                int count = Random.Range(1, 1000);
                if (count <= PlayerManager.instance.Cri * 10)
                {
                    hp -= (int)((weapon.Atk * PlayerManager.instance.Atk) * 1.5f);
                }
                else
                {
                    hp -= (int)(weapon.Atk * PlayerManager.instance.Atk);
                }
                knockBack = weapon.KnockBack;
                if (hp > 0)
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
                        player.transform.GetChild(2).GetChild(2).GetComponent<SupportItemManager>().ResearchList(i).GetComponent<ClockHat>().Activate();
                        return;
                    }
                }
                for (int i = 0; i < player.transform.GetChild(2).GetChild(2).GetComponent<SupportItemManager>().ListCount(); i++)
                {
                    if (player.transform.GetChild(2).GetChild(2).GetComponent<SupportItemManager>().ResearchList(i).GetComponent<SpaceFood>() != null)
                    {
                        player.transform.GetChild(2).GetChild(2).GetComponent<SupportItemManager>().ResearchList(i).GetComponent<SpaceFood>().Activate();
                        return;
                    }
                }
            }
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

    private void DropItem()
    {
        drop = Instantiate(dropItem[0], parent);

        drop.transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        if (drop.name != "PickUP-item(Clone)")
        {
            if (drop.name != "coin(Clone)")
            {
                dropItemManager.dropAdd(drop);
            }
            else
            {
                dropItemManager.coinAdd(drop);
            }
        }
    }
}
