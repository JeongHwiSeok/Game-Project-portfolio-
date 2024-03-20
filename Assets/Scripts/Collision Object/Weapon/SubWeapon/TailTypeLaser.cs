using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailTypeLaser : Weapon
{
    [SerializeField] public int itemLV;

    [SerializeField] GameObject laser;

    [SerializeField] float atkBuff;
    [SerializeField] float speedBuff;

    [SerializeField] int count;
    [SerializeField] float duration;

    [SerializeField] Vector3 target;

    [SerializeField] Transform parent;

    [SerializeField] List<GameObject> standbyLaser;

    private void Start()
    {
        atk = 10;
        normalspeed = 10;
        knockBack = 0;
        atkBuff = 1;
        speedBuff = 1;
        count = 1;
        duration = 3;

        parent = GameObject.Find("Attack Manager").transform;

        for (int i = 0; i < 4; i++)
        {
            GameObject bullet = Instantiate(laser, parent);

            bullet.GetComponent<EternityFlameBullet>().StatInput(atk, normalspeed, knockBack);

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
                atkBuff = 1.5f;
                break;
            case 4:
                duration = 2;
                break;
            case 5:
                speedBuff = 1.25f;
                atkBuff = 1.4f;
                break;
            case 6:
                duration = 1f;
                break;
            case 7:
                count = 4;
                break;
        }
    }

    private IEnumerator Fire()
    {
        while (true)
        {
            while (GameManager.instance.state)
            {
                for (int i = 0; i < count; i++)
                {
                    if (standbyLaser[i].activeSelf != true)
                    {
                        standbyLaser[i].transform.position = new Vector3(0, 0, 0);
                        standbyLaser[i].GetComponent<EternityFlameBullet>().StatInput(atk * atkBuff, normalspeed * speedBuff, knockBack);

                        target = Random.insideUnitSphere;
                        target.z = 0;

                        standbyLaser[i].GetComponent<EternityFlameBullet>().Target(target);
                        standbyLaser[i].SetActive(true);
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
