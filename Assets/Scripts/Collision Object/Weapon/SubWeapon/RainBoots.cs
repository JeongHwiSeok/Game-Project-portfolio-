using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainBoots : Weapon
{
    [SerializeField] GameObject rainFlooring;
    [SerializeField] public int itemLV;

    [SerializeField] float atkBuff;
    [SerializeField] float speedBuff;

    [SerializeField] float duration;
    [SerializeField] float size;

    [SerializeField] float count;

    [SerializeField] Transform parent;

    [SerializeField] Vector3 target;

    [SerializeField] List<GameObject> standbyRainFlooring;

    private void Start()
    {
        atk = 10;
        normalspeed = 5;
        knockBack = 0f;
        atkBuff = 1;
        speedBuff = 1;
        size = 1;
        duration = 2;

        parent = GameObject.Find("Attack Manager").transform;

        GameObject bullet = Instantiate(rainFlooring, parent);

        bullet.GetComponent<RainFlooring>().StatInput(atk, normalspeed, knockBack);

        bullet.SetActive(false);

        standbyRainFlooring.Add(bullet);

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
                size = 1.1f;
                break;
            case 3:
                atkBuff = 1.3f;
                break;
            case 4:
                size = 1.25f;
                break;
            case 5:
                atkBuff = 1.6f;
                break;
            case 6:
                size = 1.5f;
                break;
            case 7:
                break;
        }
    }

    private IEnumerator Fire()
    {
        while (true)
        {
            while (GameManager.instance.state)
            {
                if (standbyRainFlooring[0].activeSelf != true)
                {
                    standbyRainFlooring[0].GetComponent<RainFlooring>().StatInput(atk * atkBuff, normalspeed * speedBuff, knockBack);
                    standbyRainFlooring[0].GetComponent<RainFlooring>().time = duration;
                    standbyRainFlooring[0].GetComponent<RainFlooring>().gameObject.transform.position = transform.position;
                    standbyRainFlooring[0].GetComponent<RainFlooring>().itemLV = itemLV;
                    standbyRainFlooring[0].transform.localScale *= size;
                    standbyRainFlooring[0].SetActive(true);
                }
                yield return new WaitForSeconds(2);
            }
            if (GameManager.instance.monsterSpawn != true)
            {
                yield break;
            }
            yield return null;
        }
    }
}
