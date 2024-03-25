using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperOticCube : Weapon
{
    [SerializeField] GameObject hyperCube;
    [SerializeField] public int itemLV;

    [SerializeField] float atkBuff;
    [SerializeField] float speedBuff;

    [SerializeField] float duration;
    [SerializeField] float size;
    [SerializeField] int count;
    [SerializeField] float time;

    [SerializeField] Transform parent;

    [SerializeField] Vector3 target;

    [SerializeField] List<GameObject> standbyCube;


    private void Start()
    {
        atk = 5;
        normalspeed = 180;
        knockBack = 1f;
        atkBuff = 1;
        speedBuff = 1;
        size = 1;
        duration = 5;
        count = 1;
        time = 1;

        parent = GameObject.Find("Attack Manager").transform;

        for (int i = 0; i < 2; i++)
        {
            GameObject bullet = Instantiate(hyperCube, parent);

            bullet.GetComponent<HyperCube>().StatInput(atk, normalspeed, knockBack);

            bullet.SetActive(false);

            standbyCube.Add(bullet);

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
                size = 1.15f;
                break;
            case 3:
                time = 2;
                break;
            case 4:
                size = 1.3f;
                break;
            case 5:
                atkBuff = 1.3f;
                break;
            case 6:
                duration = 4;
                break;
            case 7:
                count = 2;
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
                    if (standbyCube[0].activeSelf != true)
                    {
                        standbyCube[0].transform.position = new Vector3(1.5f, 0, 0);
                        standbyCube[0].GetComponent<HyperCube>().StatInput(atk * atkBuff, normalspeed * speedBuff, knockBack);
                        standbyCube[0].GetComponent<HyperCube>().Duration(time);
                        standbyCube[0].transform.localScale = new Vector3(2.8f * size, 2.8f * size, 2.8f * size);
                        standbyCube[0].SetActive(true);
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
