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

    [SerializeField] List<GameObject> standbyIkuminUfo;

    private void OnEnable()
    {
        atk = 10;
        normalspeed = 180;
        speed = normalspeed;
        knockBack = 0.5f;
        size = 1f;
        time = 2;
        duration = 3;
        parent = GameObject.Find("Attack Manager").transform;

        GameObject standbyChakram = Instantiate(snakeChakramBullet, parent);

        standbyChakram.GetComponent<SnakeChakramRotation>().StatInput(atk, normalspeed, knockBack);

        standbyChakram.SetActive(false);

        standbyIkuminUfo.Add(standbyChakram);

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
                size = 1.2f;
                atkBuff = 1.2f;
                break;
            case 3:
                duration = 2;
                break;
            case 4:
                size = 1.5f;
                atkBuff = 1.5f;
                break;
            case 5:
                time = 3;
                break;
            case 6:
                size = 2.0f;
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

                if (standbyIkuminUfo[0].activeSelf != true)
                {
                    standbyIkuminUfo[0].transform.position = new Vector3(1, 0, 0) * 2;
                    standbyIkuminUfo[0].GetComponent<SnakeChakramRotation>().StatInput(atk * atkBuff, normalspeed * speedBuff, knockBack);
                    standbyIkuminUfo[0].GetComponent<SnakeChakramRotation>().Duration(time);
                    standbyIkuminUfo[0].GetComponent<SnakeChakramRotation>().transform.localScale *= size;
                    standbyIkuminUfo[0].SetActive(true);
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
