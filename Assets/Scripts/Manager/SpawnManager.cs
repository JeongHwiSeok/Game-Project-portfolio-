using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] Transform parent;
    [SerializeField] List<GameObject> monsterList;

    [SerializeField] List<GameObject> standByMonsterList;

    [SerializeField] int maxCount;

    [SerializeField] float countTime;
    [SerializeField] int monsterNumber;
    [SerializeField] int changeCount;

    [SerializeField] int monsterMax;

    private void Start()
    {
        standByMonsterList.Capacity = 1000;

        maxCount = 20;

        CreateMonster();

        StartCoroutine(SpawnMonster());

        monsterMax = 500;
    }

    private void Update()
    {
        countTime += Time.deltaTime;
    }

    private void CreateMonster()
    {
        for (int i = 0; i < 20; i++)
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
            if (standByMonsterList.Count < maxCount)
            {
                CreateMonster();
            }
            for (int i = 0; i < maxCount; i++)
            {
                if (standByMonsterList[i].activeSelf == false)
                {
                    Vector3 pos = Random.insideUnitCircle * 20f;

                    while ((pos.x >= -10 && pos.x <= 10) && (pos.y >= -5 && pos.y <= 5))
                    {
                        pos = Random.insideUnitCircle * 20f;
                    }

                    standByMonsterList[i].transform.position = pos;

                    standByMonsterList[i].SetActive(true);
                }
            }
            if (maxCount < monsterMax)
            {
                maxCount++;
            }      
            if (countTime >= 60)
            {
                if(monsterNumber < monsterList.Count - 1)
                {
                    monsterNumber++;
                }
                changeCount = standByMonsterList.Count;
                StartCoroutine(ChangeMonster());
                countTime = 0;
            }
            if (GameManager.instance.monsterSpawn == false)
            {
                yield break;
            }
            yield return new WaitForSeconds(1f);
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
}