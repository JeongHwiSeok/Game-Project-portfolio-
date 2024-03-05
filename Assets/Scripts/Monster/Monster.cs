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
    [SerializeField] protected bool redCoin = false;

    [SerializeField] protected Vector3 direction;
    [SerializeField] protected SpriteRenderer spriteRenderer;

    [SerializeField] protected List<GameObject> dropItem;

    [SerializeField] Weapon weapon;
    [SerializeField] PlayerManager player;

    [SerializeField] protected float timeCheck;
    [SerializeField] protected float damageTime;

    [SerializeField] protected Transform parent;

    private int[] dropPercent = new int[5] {3,5,7,11,29};
    private int maxDropCount;

    protected virtual void OnEnable()
    {
        maxDropCount = 10;
        parent = transform.parent.parent.GetChild(11);
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
        StartCoroutine(LastPosition());
        for (int i = 0; i < dropPercent.Length; i++)
        {
            maxDropCount *= dropPercent[i];
        }
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
        if(firstCheck)
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

        if(GameManager.instance.state)
        {
            if (weapon != null)
            {
                hp -= (int)weapon.Atk;
                knockBack = weapon.KnockBack;
                if(hp > 0)
                {
                    damageTime = 0.2f;
                }
            }
            if (player != null)
            {
                player.Damage(atk);
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
        int randomCount = Random.Range(1, maxDropCount+1);

        GameObject drop;

        if(randomCount % dropPercent[4] == 0)
        {
            randomCount = Random.Range(1, 6);
            if(redCoin && randomCount % 5 == 0)
            {
                // drop = Instantiate(dropItem[5]);
                return;
            }
            else
            {
                drop = Instantiate(dropItem[0], parent);
            }
        }
        else if(randomCount % dropPercent[0] == 0)
        {
            randomCount = Random.Range(1, 106);
            
            if (randomCount % 3 == 0 && UIManager.instance.time >= 60)
            {
                drop = Instantiate(dropItem[2], parent);
            }
            else if (randomCount % 5 == 0 && UIManager.instance.time >= 120)
            {
                drop = Instantiate(dropItem[3], parent);
            }
            else
            {
                drop = Instantiate(dropItem[1], parent);
            }
        }
        else
        {
            return;
        }

        float x = Random.insideUnitSphere.x + transform.position.x;

        float y = Random.insideUnitSphere.y + transform.position.y;

        drop.transform.position = new Vector3(x, y, 0);
    }
}
