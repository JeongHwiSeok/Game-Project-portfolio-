using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeChakramRotation : Weapon
{
    [SerializeField] GameObject chakram;
    [SerializeField] public int itemLV;
    [SerializeField] float duration;
    [SerializeField] int count;

    [SerializeField] float atkBuff;
    [SerializeField] float speedBuff;

    [SerializeField] float dis;

    [SerializeField] Transform parent;

    [SerializeField] List<GameObject> standbyChakramList;

    private void OnEnable()
    {
        atk = 10;
        normalspeed = 180;
        speed = normalspeed;
        knockBack = 0.5f;
        dis = 2f;
        parent = GameObject.Find("Attack Manager").transform;
        for (int i = 0; i < 4; i++)
        {
            GameObject standbyChakram = Instantiate(chakram, parent);

            standbyChakram.GetComponent<SnakeChakram>().StatInput(atk, normalspeed, knockBack);

            standbyChakram.SetActive(false);

            standbyChakramList.Add(standbyChakram);

            GameObject.Find("Attack Manager").GetComponent<WeaponManager>().AddWeapon(standbyChakram);
        }
        StartCoroutine(Create());
    }

    public void Activate()
    {
        switch (itemLV)
        {
            case 1:
                count = 1;
                duration = 4;
                atkBuff = 1;
                speedBuff = 1;
                break;
            case 2:
                count = 2;
                atkBuff = 1.3f;
                break;
            case 3:
                duration = 6;
                break;
            case 4:
                count = 3;
                break;
            case 5:
                atkBuff = 1.6f;
                speedBuff = 1.5f;
                break;
            case 6:
                count = 4;
                break;
            case 7:
                atkBuff = 2f;
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
                for (int i = 0; i < count; i++)
                {
                    if (standbyChakramList[i].activeSelf != true && flag)
                    {
                        standbyChakramList[i].transform.position = new Vector3(Mathf.Cos((i * 360/count) * Mathf.Deg2Rad), Mathf.Sin((i * 360 / count) * Mathf.Deg2Rad), 0) * dis;
                        standbyChakramList[i].GetComponent<SnakeChakram>().StatInput(atk * atkBuff, normalspeed * speedBuff, knockBack);
                        standbyChakramList[i].GetComponent<SnakeChakram>().Duration(duration);
                        standbyChakramList[i].SetActive(true);
                    }
                }
                flag = false;
                yield return new WaitForSeconds(duration + 2);
                flag = true;
            }
            if (GameManager.instance.monsterSpawn != true)
            {
                yield break;
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
