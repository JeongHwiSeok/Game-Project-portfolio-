using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeChakram : Weapon
{
    [SerializeField] GameObject snakeChakramBullet;
    [SerializeField] public int itemLV;
    [SerializeField] float duration;
    [SerializeField] int count;

    [SerializeField] float atkBuff;
    [SerializeField] float speedBuff;
    [SerializeField] float time;

    [SerializeField] float size;

    [SerializeField] Transform parent;

    [SerializeField] List<GameObject> standbyChakram;

    private void OnEnable()
    {
        atk = 10;
        normalspeed = 180;
        speed = normalspeed;
        knockBack = 0.1f;
        size = 2.8f;
        time = 2;
        duration = 5;
        parent = GameObject.Find("Attack Manager").transform;

        GameObject standbyChakram = Instantiate(snakeChakramBullet, parent);

        standbyChakram.GetComponent<SnakeChakramRotation>().StatInput(atk, normalspeed, knockBack);

        standbyChakram.SetActive(false);

        this.standbyChakram.Add(standbyChakram);

        GameObject.Find("Attack Manager").GetComponent<WeaponManager>().AddWeapon(standbyChakram);
        
        StartCoroutine(Create());
    }

    public void Activate()
    {
        switch (itemLV)
        {
            case 1:
                break;
            case 2:
                size = 3.36f;
                atkBuff = 1.2f;
                break;
            case 3:
                duration = 4;
                break;
            case 4:
                size = 4.2f;
                atkBuff = 1.5f;
                break;
            case 5:
                time = 3;
                break;
            case 6:
                size = 5.6f;
                break;
            case 7:
                atkBuff = 2.0f;
                break;
        }
    }

    private IEnumerator Create()
    {
        bool flag = true;
        while (true)
        {
            while (GameManager.instance.state)
            {
                if (standbyChakram[0].activeSelf != true)
                {
                    standbyChakram[0].transform.position = new Vector3(1, 0, 0) * 3;
                    standbyChakram[0].GetComponent<SnakeChakramRotation>().StatInput(atk * atkBuff, normalspeed * speedBuff, knockBack);
                    standbyChakram[0].GetComponent<SnakeChakramRotation>().Duration(time);
                    standbyChakram[0].GetComponent<SnakeChakramRotation>().transform.localScale = new Vector3(size, size, size);
                    standbyChakram[0].SetActive(true);
                }

                yield return new WaitForSeconds(duration);
            }
            if (GameManager.instance.monsterSpawn != true)
            {
                yield break;
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
