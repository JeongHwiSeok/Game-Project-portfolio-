using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IkuminTransmitter : Weapon
{
    [SerializeField] GameObject ikuminUfo;
    [SerializeField] public int itemLV;
    [SerializeField] float duration;
    [SerializeField] int count;

    [SerializeField] float atkBuff;
    [SerializeField] float speedBuff;

    [SerializeField] float dis;

    [SerializeField] Transform parent;

    [SerializeField] List<GameObject> standbyIkuminUfo;

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
            GameObject standbyChakram = Instantiate(ikuminUfo, parent);

            standbyChakram.GetComponent<IkuminUfo>().StatInput(atk, normalspeed, knockBack);

            standbyChakram.SetActive(false);

            standbyIkuminUfo.Add(standbyChakram);

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
                    if (standbyIkuminUfo[i].activeSelf != true)
                    {
                        standbyIkuminUfo[i].transform.position = new Vector3(Mathf.Cos((i * 360 / count) * Mathf.Deg2Rad), Mathf.Sin((i * 360 / count) * Mathf.Deg2Rad), 0) * dis;
                        standbyIkuminUfo[i].GetComponent<IkuminUfo>().StatInput(atk * atkBuff, normalspeed * speedBuff, knockBack);
                        standbyIkuminUfo[i].GetComponent<IkuminUfo>().Duration(duration);
                        standbyIkuminUfo[i].SetActive(true);
                    }
                }
                yield return new WaitForSeconds(duration + 2);
            }
            if (GameManager.instance.monsterSpawn != true)
            {
                yield break;
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
