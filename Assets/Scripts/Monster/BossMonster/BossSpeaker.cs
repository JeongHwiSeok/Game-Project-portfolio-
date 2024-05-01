using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpeaker : BigMonster
{
    [SerializeField] GameObject attack;
    [SerializeField] GameObject attackRange;
    [SerializeField] Animator[] animators;

    private void Awake()
    {
        maxHp = 2500;
        hp = maxHp;
        atk = 25;
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
        invincible = true;
        StartCoroutine(LastPosition());
        StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        while (true)
        {
            while (GameManager.instance.state)
            {
                animators[0].speed = 1;
                animators[1].speed = 0.5f;

                attackRange.SetActive(true);

                animators[1].speed = 0.5f;
                yield return new WaitForSeconds(1);
                animators[1].speed = 0.7f;
                yield return new WaitForSeconds(1);
                animators[1].speed = 1f;
                yield return new WaitForSeconds(1);

                attackRange.SetActive(false);

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
}
