using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilsTail : Weapon
{
    [SerializeField] GameObject tailAttack;
    [SerializeField] public int itemLV;

    [SerializeField] float atkBuff;
    [SerializeField] float speedBuff;
    [SerializeField] float knockBack2;

    [SerializeField] float duration;
    [SerializeField] float range;

    [SerializeField] bool flag;

    [SerializeField] Transform parent;

    [SerializeField] Vector3 target;

    [SerializeField] List<GameObject> standbyTail;

    private void Start()
    {
        atk = 0;
        normalspeed = 5;
        knockBack = 0f;
        knockBack2 = 0.5f;
        atkBuff = 1;
        speedBuff = 1;
        range = 2.8f;
        duration = 3;

        flag = true;

        parent = GameObject.Find("Attack Manager").transform;

        GameObject bullet = Instantiate(tailAttack, parent);

        bullet.GetComponent<TailBullet>().StatInput(atk, normalspeed, knockBack);

        bullet.SetActive(false);

        standbyTail.Add(bullet);

        GameObject.Find("Attack Manager").GetComponent<WeaponManager>().AddWeapon(bullet);

        StartCoroutine(Fire());
    }

    public void Activate()
    {
        switch (itemLV)
        {
            case 1:
                break;
            case 2:
                atkBuff = 1.2f;
                break;
            case 3:
                knockBack2 = 0.6f;
                break;
            case 4:
                duration = 2;
                break;
            case 5:
                range = 3.36f;
                atkBuff = 1.4f;
                break;
            case 6:
                duration = 1;
                break;
            case 7:
                range = 4.2f;
                break;
        }
    }

    private IEnumerator Fire()
    {
        yield return new WaitForSeconds(1f);
        while (true)
        {
            while (GameManager.instance.state)
            {
                if (standbyTail[0].activeSelf != true)
                { 
                    standbyTail[0].GetComponent<TailBullet>().StatInput(30 * atkBuff, normalspeed * speedBuff, knockBack2);
                    standbyTail[0].GetComponent<TailBullet>().Target(target);

                    target.Normalize();

                    standbyTail[0].transform.position = target * range;

                    standbyTail[0].transform.localScale = new Vector3(range, range, range);
                    standbyTail[0].SetActive(true);
                    flag = true;
                }
                yield return new WaitForSeconds(duration);
            }
            if (GameManager.instance.monsterSpawn != true)
            {
                yield break;
            }
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Monster monster = collision.GetComponent<Monster>();

        if (monster != null && flag)
        {
            target = monster.gameObject.transform.position;
            flag = false;
        }
    }
}
