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

    [SerializeField] List<GameObject> standbyChakrams;

    private void OnEnable()
    {
        atk = 10;
        normalspeed = 180;
        speed = normalspeed;
        knockBack = 0.1f;
        atkBuff = 1.0f;
        size = 1f;
        time = 2;
        duration = 5;
        parent = GameObject.Find("Attack Manager").transform;

        GameObject standbyChakram = Instantiate(snakeChakramBullet, parent);

        standbyChakram.GetComponent<SnakeChakramRotation>().StatInput(atk, normalspeed, knockBack);

        standbyChakram.SetActive(false);

        standbyChakrams.Add(standbyChakram);

        GameManager.instance.weaponItemList.Add(standbyChakram);
        
        StartCoroutine(Create());
    }

    public void Activate()
    {
        switch (itemLV)
        {
            case 1:
                break;
            case 2:
                size = 1.2f;
                atkBuff = 1.2f;
                break;
            case 3:
                duration = 4;
                break;
            case 4:
                size = 1.5f;
                atkBuff = 1.5f;
                break;
            case 5:
                time = 3;
                break;
            case 6:
                size = 2f;
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
                if (standbyChakrams[0].activeSelf != true)
                {
                    standbyChakrams[0].transform.position = new Vector3(1, 0, 0) * 3;
                    standbyChakrams[0].GetComponent<SnakeChakramRotation>().StatInput(atk * atkBuff, normalspeed * speedBuff * BuffDebuffManager.instance.pwsSpeedBuff, knockBack);
                    standbyChakrams[0].GetComponent<SnakeChakramRotation>().Duration(time);
                    standbyChakrams[0].GetComponent<SnakeChakramRotation>().transform.localScale = new Vector3( 3.5f * size, 3.5f * size, 3.5f * size);
                    standbyChakrams[0].SetActive(true);
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
