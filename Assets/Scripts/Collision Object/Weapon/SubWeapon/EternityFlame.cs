using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EternityFlame : Weapon
{
    [SerializeField] public int itemLV;

    [SerializeField] GameObject eternityFlameBullet;
    
    [SerializeField] float atkBuff;
    [SerializeField] float speedBuff;

    [SerializeField] int count;
    [SerializeField] float duration;

    [SerializeField] Vector3 target;

    [SerializeField] Transform parent;

    [SerializeField] List<GameObject> standbyFlameBullet;

    private void Start()
    {
        atk = 10;
        normalspeed = 5;
        knockBack = 0;
        atkBuff = 1;
        speedBuff = 1;
        count = 1;
        duration = 3;

        parent = GameObject.Find("Attack Manager").transform;

        for (int i = 0; i < 4; i++)
        {
            GameObject bullet = Instantiate(eternityFlameBullet, parent);

            bullet.GetComponent<EternityFlameBullet>().StatInput(atk, normalspeed, knockBack);

            bullet.SetActive(false);

            standbyFlameBullet.Add(bullet);

            GameManager.instance.weaponItemList.Add(bullet);

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
                count = 2;
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
                    if (standbyFlameBullet[i].activeSelf != true)
                    {
                        standbyFlameBullet[i].transform.position = new Vector3(0, 0, 0);
                        standbyFlameBullet[i].GetComponent<EternityFlameBullet>().StatInput(atk * atkBuff, normalspeed * speedBuff * BuffDebuffManager.instance.pwsSpeedBuff, knockBack);

                        target = Random.insideUnitSphere;
                        target.z = 0;
                        
                        standbyFlameBullet[i].GetComponent<EternityFlameBullet>().Target(target);
                        standbyFlameBullet[i].SetActive(true);
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
