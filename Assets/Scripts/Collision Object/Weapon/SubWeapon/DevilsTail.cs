using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilsTail : Weapon
{
    [SerializeField] GameObject tailAttack;
    [SerializeField] public int itemLV;

    [SerializeField] float atkBuff;
    [SerializeField] float speedBuff;

    [SerializeField] float duration;
    [SerializeField] float range;

    [SerializeField] Transform parent;

    [SerializeField] Vector3 target;

    [SerializeField] List<GameObject> standbyTail;

    [SerializeField] List<GameObject> monsterList;

    private void Start()
    {
        atk = 30;
        normalspeed = 5;
        knockBack = 0.5f;
        atkBuff = 1;
        speedBuff = 1;
        range = 1;
        duration = 3;

        parent = GameObject.Find("Attack Manager").transform;

        GameObject bullet = Instantiate(tailAttack, parent);

        bullet.GetComponent<TailBullet>().StatInput(atk, normalspeed, knockBack);

        bullet.SetActive(false);

        standbyTail.Add(bullet);

        GameObject.Find("Attack Manager").GetComponent<WeaponManager>().AddWeapon(bullet);

        StartCoroutine(Fire());
    }

    private void Update()
    {
        if (monsterList.Count>0)
        {
            Vector3 offset = monsterList[0].transform.position - Vector3.zero;
            float sqrLen = offset.sqrMagnitude;
            foreach (GameObject found in monsterList)
            {
                if (sqrLen > found.transform.position.sqrMagnitude)
                {
                    target = found.transform.position;
                }
            }
        }        
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
                knockBack = 0.6f;
                break;
            case 4:
                duration = 2;
                break;
            case 5:
                range = 1.2f;
                atkBuff = 1.4f;
                break;
            case 6:
                duration = 1;
                break;
            case 7:
                range = 1.5f;
                break;
        }
    }

    private IEnumerator Fire()
    {
        while (true)
        {
            while (GameManager.instance.state)
            {
                if (standbyTail[0].activeSelf != true)
                {
                    standbyTail[0].transform.position = new Vector3(0, 0, 0);
                    standbyTail[0].GetComponent<TailBullet>().StatInput(atk * atkBuff, normalspeed * speedBuff, knockBack);
                    standbyTail[0].GetComponent<TailBullet>().Target(target);
                    standbyTail[0].transform.localScale *= range;
                    standbyTail[0].SetActive(true);
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

    public void MonsterRemove(GameObject obj)
    {
        monsterList.Remove(obj);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Monster monster = collision.GetComponent<Monster>();

        if (monster != null)
        {
            monsterList.Add(monster.gameObject);
        }
    }
}
