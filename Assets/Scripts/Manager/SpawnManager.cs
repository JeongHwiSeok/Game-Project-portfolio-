using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] Transform parent;
    [SerializeField] List<GameObject> monsterList;
    
    [SerializeField] List<GameObject> bigMonsterList;

    [SerializeField] List<GameObject> bossMonsterList;

    [SerializeField] List<GameObject> standByMonsterList;

    [SerializeField] int maxCount;

    [SerializeField] int monsterNumber;
    [SerializeField] int bigMonsterNumber;
    [SerializeField] int bossMonsterNumber;
    [SerializeField] int changeCount;

    [SerializeField] int monsterMax;

    public static SpawnManager instance
    {
        get;
        private set;
    }
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        standByMonsterList.Capacity = 1000;

        maxCount = 20;

        CreateMonster();

        StartCoroutine(SpawnMonster());

        monsterMax = 300;
    }

    private void CreateMonster()
    {
        for (int i = 0; i < 50; i++)
        {
            GameObject monster = Instantiate(monsterList[monsterNumber], parent);

            monster.SetActive(false);

            standByMonsterList.Add(monster);
        }
    }

    private IEnumerator SpawnMonster()
    {
        while (true)
        {
            if (standByMonsterList.Count < maxCount * BuffDebuffManager.instance.cbCountDebuff * BuffDebuffManager.instance.shopBroadCast)
            {
                CreateMonster();
            }
            for (int i = 0; i < maxCount; i++)
            {
                if (standByMonsterList[i].activeSelf == false)
                {
                    Vector3 pos = Random.insideUnitCircle * 20f;

                    while (Mathf.Abs(pos.x) <= 10 && Mathf.Abs(pos.x) <= 5)
                    {
                        pos = Random.insideUnitCircle * 20f;
                    }

                    standByMonsterList[i].transform.position = pos;

                    standByMonsterList[i].SetActive(true);
                }
            }
            if (maxCount < monsterMax * BuffDebuffManager.instance.cbCountDebuff * BuffDebuffManager.instance.shopBroadCast && GameManager.instance.time % 30 <= 1 && GameManager.instance.time > 1)
            {
                if (maxCount + (int)(10 * BuffDebuffManager.instance.cbCountDebuff * BuffDebuffManager.instance.shopBroadCast) < monsterMax * BuffDebuffManager.instance.cbCountDebuff * BuffDebuffManager.instance.shopBroadCast)
                {
                    maxCount += (int)(10 * BuffDebuffManager.instance.cbCountDebuff * BuffDebuffManager.instance.shopBroadCast);
                }
            }      
            if(GameManager.instance.time % 90 < 1 && GameManager.instance.time >= (bigMonsterNumber +1 ) * 90 && GameManager.instance.time > 1 && GameManager.instance.time < 610)
            {
                GameObject monster = Instantiate(bigMonsterList[bigMonsterNumber], parent);

                monster.SetActive(false);

                Vector3 pos = Random.insideUnitCircle * 20f;

                while (Mathf.Abs(pos.x) <= 10 && Mathf.Abs(pos.x) <= 5)
                {
                    pos = Random.insideUnitCircle * 20f;
                }

                monster.transform.position = pos;

                monster.SetActive(true);

                bigMonsterNumber++;
            }
            else if ((GameManager.instance.time - 60) % 90 < 1 && GameManager.instance.time >= ((bigMonsterNumber + 1) * 90) + 60 && GameManager.instance.time < 1210)
            {
                GameObject monster = Instantiate(bigMonsterList[bigMonsterNumber], parent);

                monster.SetActive(false);

                Vector3 pos = Random.insideUnitCircle * 20f;

                while (Mathf.Abs(pos.x) <= 10 && Mathf.Abs(pos.y) <= 5)
                {
                    pos = Random.insideUnitCircle * 20f;
                }

                monster.transform.position = pos;

                monster.SetActive(true);

                bigMonsterNumber++;
            }
            if (GameManager.instance.time % 90 <= 1 && GameManager.instance.time >= 1 && GameManager.instance.time > (monsterNumber +1) *90 && GameManager.instance.time < 610)
            {
                if(monsterNumber < monsterList.Count - 1)
                {
                    monsterNumber++;
                }
                changeCount = standByMonsterList.Count / 2;
                StartCoroutine(ChangeMonster());
            }
            else if ((GameManager.instance.time - 60) % 90 <= 1 && GameManager.instance.time > ((monsterNumber + 1) * 90) + 60 && GameManager.instance.time < 1200)
            {
                if (monsterNumber < monsterList.Count - 1)
                {
                    monsterNumber++;
                }
                if (monsterNumber < 13)
                {
                    changeCount = standByMonsterList.Count / 2;
                    StartCoroutine(ChangeMonster());
                }
                else
                {
                    changeCount = standByMonsterList.Count;
                    StartCoroutine(EndChangeMonster());
                }
            }
            if (GameManager.instance.time % 600 <= 1 && GameManager.instance.time >= 1 && GameManager.instance.time > (bossMonsterNumber +1) * 600 && bossMonsterNumber < bossMonsterList.Count)
            {
                GameObject monster = Instantiate(bossMonsterList[bossMonsterNumber], parent);

                monster.SetActive(false);

                Vector3 pos = Random.insideUnitCircle * 20f;

                while (Mathf.Abs(pos.x) <= 10 && Mathf.Abs(pos.y) <= 5)
                {
                    pos = Random.insideUnitCircle * 20f;
                }

                monster.transform.position = pos;

                monster.SetActive(true);

                bossMonsterNumber++;
            }
            if (GameManager.instance.monsterSpawn == false)
            {
                yield break;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator ChangeMonster()
    {
        while (changeCount > 0)
        {
            for (int i = 0; i < changeCount; i++)
            {
                GameObject deleteObj = standByMonsterList[i].gameObject;

                if (deleteObj.activeSelf == false)
                {
                    standByMonsterList.Remove(standByMonsterList[i]);

                    Destroy(deleteObj);

                    GameObject monster = Instantiate(monsterList[monsterNumber], parent);

                    monster.SetActive(false);

                    standByMonsterList.Add(monster);

                    changeCount--;
                }
            }
            yield return null;
        }
    }

    private IEnumerator EndChangeMonster()
    {
        while (changeCount > 0)
        {
            for (int i = 0; i < changeCount; i++)
            {
                GameObject deleteObj = standByMonsterList[i].gameObject;

                if (deleteObj.activeSelf == false)
                {
                    standByMonsterList.Remove(standByMonsterList[i]);

                    Destroy(deleteObj);

                    GameObject monster = Instantiate(monsterList[monsterNumber], parent);

                    monster.SetActive(false);

                    standByMonsterList.Add(monster);

                    changeCount--;
                }
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
}