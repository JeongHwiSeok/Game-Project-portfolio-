using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailTypeLaser : Weapon
{
    [SerializeField] public int itemLV;

    [SerializeField] GameObject laser;

    [SerializeField] Vector3 target;

    [SerializeField] float atkBuff;
    [SerializeField] float speedBuff;
    [SerializeField] float size;

    [SerializeField] int maxCount;
    [SerializeField] int count;
    [SerializeField] float duration;

    [SerializeField] Transform parent;

    [SerializeField] List<GameObject> standbyLaser;

    private void Start()
    {
        atk = 10;
        knockBack = 1;
        atkBuff = 1;
        speedBuff = 1;
        maxCount = 1;
        duration = 5;
        size = 1;

        parent = GameObject.Find("Attack Manager").transform;

        for (int i = 0; i < 2; i++)
        {
            GameObject bullet = Instantiate(laser, parent);

            bullet.GetComponent<LaserBullet>().StatInput(atk, normalspeed, knockBack);

            bullet.SetActive(false);

            standbyLaser.Add(bullet);

            GameObject.Find("Attack Manager").GetComponent<WeaponManager>().AddWeapon(bullet);
        }

        StartCoroutine(Fire());
    }
    public void Activate()
    {
        switch (itemLV)
        {
            case 1:
                break;
            case 2:
                size = 1.3f;
                break;
            case 3:
                duration = 4.5f;
                break;
            case 4:
                atkBuff = 1.5f;
                break;
            case 5:
                duration = 4f;
                break;
            case 6:
                size = 1.8f;
                break;
            case 7:
                maxCount = 2;
                break;
        }
    }

    private IEnumerator Fire()
    {
        while (true)
        {
            while (GameManager.instance.state)
            {
                target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));

                for (int i = 0; i < maxCount; i++)
                {
                    if (i == 0)
                    {
                        if (standbyLaser[i].activeSelf != true)
                        {
                            standbyLaser[i].gameObject.transform.position = new Vector3(0, 0, 0);
                            standbyLaser[i].GetComponent<LaserBullet>().StatInput(atk * atkBuff, normalspeed * speedBuff, knockBack);
                            standbyLaser[i].GetComponent<LaserBullet>().Target(target);
                            standbyLaser[i].gameObject.transform.localScale = new Vector3(2 * size, 2 * size, 2 * size);
                            standbyLaser[i].SetActive(true);
                        }
                    }
                    else
                    {
                        if (standbyLaser[i].activeSelf != true)
                        {
                            standbyLaser[i].gameObject.transform.position = new Vector3(0, 0, 0);
                            standbyLaser[i].GetComponent<LaserBullet>().StatInput(atk * atkBuff, normalspeed * speedBuff, knockBack);
                            standbyLaser[i].GetComponent<LaserBullet>().Target(target * -1);
                            standbyLaser[i].gameObject.transform.localScale = new Vector3(2 * size, 2 * size, 2 * size);
                            standbyLaser[i].SetActive(true);
                        }
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
}
