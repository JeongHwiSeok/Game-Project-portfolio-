using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] Transform parent;
    [SerializeField] List<GameObject> monsterList;
    
    [SerializeField] List<GameObject> bigMonsterList;

    [SerializeField] List<GameObject> standByMonsterList;

    [SerializeField] int maxCount;

    [SerializeField] int monsterNumber;
    [SerializeField] int bigMonsterNumber;
    [SerializeField] int changeCount;

    [SerializeField] int monsterMax;

    [SerializeField] public float cbDebuff;

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

        cbDebuff = 1;
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
            if (standByMonsterList.Count < maxCount * cbDebuff)
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
            if (maxCount < monsterMax * cbDebuff && UIManager.instance.time % 30 <= 1 && UIManager.instance.time > 1)
            {
                if (maxCount + (int)(10 * cbDebuff) < monsterMax * cbDebuff)
                {
                    maxCount += (int)(10 * cbDebuff);
                }
            }      
            if(UIManager.instance.time % 60 < 1 && UIManager.instance.time >= (bigMonsterNumber +1 ) * 60 && UIManager.instance.time > 1)
            {
                GameObject monster = Instantiate(bigMonsterList[bigMonsterNumber], parent);

                monster.SetActive(false);

                Vector3 pos = Random.insideUnitCircle * 20f;

                while ((pos.x >= -10 && pos.x <= 10) && (pos.y >= -5 && pos.y <= 5))
                {
                    pos = Random.insideUnitCircle * 20f;
                }

                monster.transform.position = pos;

                monster.SetActive(true);

                bigMonsterNumber++;
            }
            if (UIManager.instance.time % 120 <= 1 && UIManager.instance.time >= 1 && UIManager.instance.time > (monsterNumber +1) *120)
            {
                if(monsterNumber < monsterList.Count - 1)
                {
                    monsterNumber++;
                }
                changeCount = standByMonsterList.Count;
                StartCoroutine(ChangeMonster());
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