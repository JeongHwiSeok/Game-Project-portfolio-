using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrickCard : Weapon
{
    [SerializeField] GameObject cardFlooring;
    [SerializeField] public int itemLV;

    [SerializeField] float atkBuff;
    [SerializeField] float speedBuff;

    [SerializeField] float duration;
    [SerializeField] float size;

    [SerializeField] float count;

    [SerializeField] Transform parent;

    [SerializeField] Vector3 target;

    [SerializeField] List<GameObject> standbyCardFlooring;

    private void Start()
    {
        atk = 10;
        normalspeed = 5;
        knockBack = 0f;
        atkBuff = 1;
        speedBuff = 1;
        size = 1;
        duration = 2;
        count = 1;

        parent = GameObject.Find("Attack Manager").transform;

        for (int i = 0; i < 4; i++)
        {
            GameObject bullet = Instantiate(cardFlooring, parent);

            bullet.GetComponent<CardFlooring>().StatInput(atk, normalspeed, knockBack);

            bullet.SetActive(false);

            standbyCardFlooring.Add(bullet);

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
                size = 1.2f;
                break;
            case 3:
                count = 2;
                break;
            case 4:
                duration = 3;
                atkBuff = 1.2f;
                break;
            case 5:
                atkBuff = 1.5f;
                break;
            case 6:
                count = 3;
                break;
            case 7:
                count = 4;
                size = 1.4f;
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
                    if (standbyCardFlooring[i].activeSelf != true)
                    {
                        standbyCardFlooring[i].GetComponent<CardFlooring>().StatInput(atk * atkBuff, normalspeed * speedBuff, knockBack);
                        standbyCardFlooring[i].GetComponent<CardFlooring>().time = duration;

                        target = Random.insideUnitSphere;
                        target.z = 0;

                        standbyCardFlooring[i].GetComponent<CardFlooring>().gameObject.transform.position = target;
                        standbyCardFlooring[i].transform.localScale *= size;
                        standbyCardFlooring[i].SetActive(true);
                    }
                    yield return new WaitForSeconds(2);
                }
            }
            if (GameManager.instance.monsterSpawn != true)
            {
                yield break;
            }
            yield return null;
        }
    }
}
