using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShiro : BigMonster
{
    [SerializeField] GameObject attack;
    [SerializeField] GameObject attackRange;
    [SerializeField] Animator[] animators;

    [SerializeField] bool attackFlag;

    [SerializeField] public bool[] attackStap;

    private void Awake()
    {
        maxHp = 5000;
        hp = maxHp;
        atk = 30;
    }

    public int ATK
    {
        get { return atk; }
    }

    protected override void OnEnable()
    {
        parent = transform.parent.parent.GetChild(11);
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
        dropItemManager = transform.parent.parent.GetChild(11).GetComponent<DropItemManager>();
        speed = GameManager.instance.MonsterSpeed;
        rainSlow = false;
        rainDamage = false;
        trickDamage = false;
        attackFlag = false;
        for (int i = 0; i < 3; i++)
        {
            attackStap[i] = false;
        }
        StartCoroutine(LastPosition());
        StartCoroutine(Attack());
    }

    protected override void Update()
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
                if (attackFlag)
                {
                    if (attackStap[0])
                    {
                        UpTarget();
                    }
                    else if (attackStap[1])
                    {
                        AttackTarget();
                        AttackPositionStatus(direction);
                    }
                    else
                    {
                        DownTarget();
                    }
                    
                }
                else
                {
                    Target();
                    PositionStatus(direction);
                }
            }
        }
    }
    protected void UpTarget()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        Vector3 point = transform.position;
        point.y = 11.5f;
        transform.position = Vector3.Lerp(transform.position, point, 3 * Time.deltaTime);
        if (point.y - transform.position.y <= 0.1f)
        {
            attackStap[0] = false;
            attackStap[1] = true;
        }
    }

    protected void AttackTarget()
    {
        speed = 0.8f;
        if (transform.position.x >= 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
        Vector3 target = GameManager.instance.player.transform.position;
        target.y += 11.5f;
        direction = target - transform.position;

        direction.z = 0f;
        direction.Normalize();
    }

    protected void AttackPositionStatus(Vector3 direction)
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    protected void DownTarget()
    {
        Vector3 point = transform.position;
        point.y = 0;
        transform.position = Vector3.Lerp(transform.position, point, 3 * Time.deltaTime);
        if (transform.position.y - point.y <= 1f)
        {
            attackStap[2] = false;
            attackFlag = false;
            speed = 1;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    private IEnumerator Attack()
    {
        while (true)
        {
            while (GameManager.instance.state)
            {
                animators[0].speed = 1;
                animators[1].speed = 0.5f;

                attackFlag = true;
                attackStap[0] = true;

                yield return new WaitForSeconds(1);

                attackRange.SetActive(true);
                attackRange.GetComponent<PolygonCollider2D>().enabled = false;

                animators[1].speed = 0.5f;
                yield return new WaitForSeconds(1);
                animators[1].speed = 0.6f;
                yield return new WaitForSeconds(1);
                animators[1].speed = 0.7f;
                yield return new WaitForSeconds(1);
                animators[1].speed = 0.8f;
                yield return new WaitForSeconds(1);
                animators[1].speed = 0.9f;
                yield return new WaitForSeconds(1);
                animators[1].speed = 1f;
                yield return new WaitForSeconds(1);

                attack.SetActive(true);

                yield return new WaitForSeconds(7);
            }
            for (int i = 0; i < 2; i++)
            {
                animators[i].speed = 0;
            }
            if (GameManager.instance.monsterSpawn == false)
            {
                yield break;
            }
            yield return null;
        }
    }
    protected override void OnDisable()
    {
        hp = maxHp;
        spriteRenderer.flipX = false;
        spriteRenderer.color = new Color(1, 1, 1, 1);
        if (firstCheck)
        {
            GameManager.instance.monsterCount++;
            GameManager.instance.GameClear();
        }
        else
        {
            firstCheck = true;
        }
    }
}
