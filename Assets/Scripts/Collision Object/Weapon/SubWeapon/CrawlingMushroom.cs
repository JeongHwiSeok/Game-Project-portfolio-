using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlingMushroom : Weapon
{
    [SerializeField] GameObject mushroom;
    [SerializeField] public int itemLV;

    [SerializeField] float atkBuff;
    [SerializeField] float speedBuff;

    [SerializeField] int count;
    [SerializeField] float duration;
    [SerializeField] float range;

    [SerializeField] Transform parent;

    [SerializeField] Vector3 target;

    [SerializeField] List<GameObject> standbyMushRoom;

    private void Start()
    {
        atk = 30;
        normalspeed = 5;
        knockBack = 1;
        atkBuff = 1;
        speedBuff = 1;
        count = 1;
        range = 1;
        duration = 3;

        parent = GameObject.Find("Attack Manager").transform;

        for (int i = 0; i < 3; i++)
        {
            GameObject bullet = Instantiate(mushroom, parent);

            bullet.GetComponent<EternityFlameBullet>().StatInput(atk, normalspeed, knockBack);

            bullet.SetActive(false);

            standbyMushRoom.Add(bullet);

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
                range = 1.2f;
                break;
            case 3:
                atkBuff = 1.3f;
                break;
            case 4:
                count = 2;
                break;
            case 5:
                duration = 2.4f;
                break;
            case 6:
                range = 1.4f;
                break;
            case 7:
                count = 3;
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
                    if (standbyMushRoom[i].activeSelf != true)
                    {
                        standbyMushRoom[i].transform.position = new Vector3(0, 0, 0);
                        standbyMushRoom[i].GetComponent<Mushroom>().StatInput(atk * atkBuff, normalspeed * speedBuff, knockBack);

                        target = Random.insideUnitSphere;
                        target.z = 0;

                        standbyMushRoom[i].GetComponent<Mushroom>().Point(target);
                        standbyMushRoom[i].GetComponent<Mushroom>().circleCollider2D.radius *= range;
                        standbyMushRoom[i].SetActive(true);
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
