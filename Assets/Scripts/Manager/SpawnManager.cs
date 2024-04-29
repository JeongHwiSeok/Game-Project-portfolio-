using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] Transform parent;

    [SerializeField] GameObject baseMonster;
    
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
            GameObject monster = Instantiate(baseMonster, parent);

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
            if(GameManager.instance.time >= (bigMonsterNumber +1 ) * 90 && GameManager.instance.time > 1 && GameManager.instance.time < 610)
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
            else if (GameManager.instance.time >= ((bigMonsterNumber + 1) * 90) + 60 && GameManager.instance.time < 1210)
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
            if (GameManager.instance.time >= 1 && GameManager.instance.time > (monsterNumber +1) *90 && GameManager.instance.time < 610)
            {
                monsterNumber++;
                changeCount = standByMonsterList.Count / 2 + 1;
                ChangeMonster();
            }
            else if (GameManager.instance.time > ((monsterNumber + 1) * 90) + 60 && GameManager.instance.time < 1200)
            {
                monsterNumber++;
                if (monsterNumber < 12)
                {
                    changeCount = standByMonsterList.Count / 2 + 1;
                }
                else
                {
                    changeCount = standByMonsterList.Count;
                }
                ChangeMonster();
            }
            if (GameManager.instance.time > (bossMonsterNumber +1) * 600 && bossMonsterNumber < bossMonsterList.Count && GameManager.instance.time < 1800)
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

    private void ChangeMonster()
    {
        for (int i = 0; i < changeCount; i++)
        {
            GameObject monster = standByMonsterList[i].gameObject;

            monster.GetComponent<Monster>().MonsterNumber = monsterNumber;
        }

        standByMonsterList = standByMonsterList.OrderBy(p => p.GetComponent<Monster>().MonsterNumber).ToList();
        Debug.Log("Test");
        Debug.Log(standByMonsterList);
    }
}