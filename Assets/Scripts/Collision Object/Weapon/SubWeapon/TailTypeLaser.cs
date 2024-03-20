using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailTypeLaser : Weapon
{
    [SerializeField] public int itemLV;

    [SerializeField] GameObject laser;

    [SerializeField] float atkBuff;
    [SerializeField] float speedBuff;

    [SerializeField] int maxCount;
    [SerializeField] int count;
    [SerializeField] float duration;

    [SerializeField] Vector3 currentTarget;
    [SerializeField] Vector3 preivousTarget;

    [SerializeField] Transform parent;

    [SerializeField] List<GameObject> standbyLaser;

    private void Start()
    {
        atk = 10;
        normalspeed = 7.5f;
        knockBack = 0;
        atkBuff = 1;
        speedBuff = 1;
        maxCount = 1;
        duration = 2;

        parent = GameObject.Find("Attack Manager").transform;

        for (int i = 0; i < 8; i++)
        {
            GameObject bullet = Instantiate(laser, parent);

            bullet.GetComponent<LaserBullet>().StatInput(atk, normalspeed, knockBack);

            bullet.SetActive(false);

            standbyLaser.Add(bullet);

            GameObject.Find("Attack Manager").GetComponent<WeaponManager>().AddWeapon(bullet);

            StartCoroutine(Fire());
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
                maxCount = 2;
                break;
            case 4:
                atkBuff = 1.5f;
                break;
            case 5:
                maxCount = 3;
                break;
            case 6:
                duration = 1;
                break;
            case 7:
                maxCount = 4;
                break;
        }
    }

    private IEnumerator Fire()
    {
        while (true)
        {
            while (GameManager.instance.state)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (standbyLaser[i].activeSelf != true)
                    {
                        if (currentTarget != Vector3.zero && currentTarget != preivousTarget)
                        {
                            standbyLaser[i].transform.position = new Vector3(0, 0, 0);
                            standbyLaser[i].GetComponent<LaserBullet>().StatInput(atk * atkBuff, normalspeed * speedBuff, knockBack);
                            standbyLaser[i].GetComponent<LaserBullet>().Target(currentTarget);
                            standbyLaser[i].SetActive(true);
                            count++;
                        }
                        if (count == maxCount)
                        {
                            break;
                        }

                        yield return new WaitForSeconds(0.3f);
                    }
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

        if (monster != null)
        {
            preivousTarget = currentTarget;
            currentTarget = monster.transform.position;
        }
    }
}
