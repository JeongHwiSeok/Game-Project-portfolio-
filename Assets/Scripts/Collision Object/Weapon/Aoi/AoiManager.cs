using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoiManager : MonoBehaviour
{
    [SerializeField] public List<GameObject> aoiWeapon;

    [SerializeField] public List<GameObject> standbyWeapon;

    [SerializeField] List<GameObject> standbySpecialWeapon;

    [SerializeField] GameObject note;

    [SerializeField] public Vector3 direction;

    [SerializeField] Bakamori bakamori;

    [SerializeField] public float atkBuff;
    [SerializeField] public float size;
    [SerializeField] public float spdBuff;

    [SerializeField] public float bakamoriBuff;

    [SerializeField] public float timeControllBuff;

    private int maxCount;

    public int attackCount;

    public bool buffCheck;

    public float aoiSpecialWeaponTime;

    public float bakamoriTime;

    public static AoiManager instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        buffCheck = true;
        note.SetActive(false);
    }

    private void Start()
    {
        atkBuff = 1;
        spdBuff = 1;
        size = 1;
        bakamoriBuff = 1;
        maxCount = 1;
        attackCount = 0;
        Create();
        Attack();
        if (DataManager.instance.subArray[0, 7] > 0)
        {
            StartCoroutine(TimeControl());
        }
        if (DataManager.instance.subArray[0, 8] > 0)
        {
            if (DataManager.instance.subArray[0, 8] == 2)
            {
                maxCount = 2;
            }
            else if (DataManager.instance.subArray[0, 8] >= 3)
            {
                maxCount = 3;
            }
            StartCoroutine(AoiSpecialWeapon());
        }
        if (DataManager.instance.subArray[0, 9] > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            StartCoroutine(BakaMoriStart());
        }
    }

    private void Create()
    {
        Transform parent = GameObject.Find("Attack Manager").transform;

        for(int i = 0; i < 5; i++)
        {
            GameObject weapon = Instantiate(aoiWeapon[i], parent);

            weapon.SetActive(false);

            standbyWeapon.Add(weapon);

            GameManager.instance.weaponItemList.Add(weapon);

            if (i == 4)
            {
                weapon = Instantiate(aoiWeapon[3], parent);

                weapon.SetActive(false);

                standbyWeapon.Add(weapon);

                GameManager.instance.weaponItemList.Add(weapon);
            }
        }
        for (int i = 0; i < 3; i++)
        {
            GameObject specialWeapon = Instantiate(aoiWeapon[4], GameObject.Find("Map").transform.GetChild(12).GetComponent<Transform>());

            specialWeapon.SetActive(false);

            standbySpecialWeapon.Add(specialWeapon);
        }
    }
    public void AttackLVUP()
    {
        switch (GameManager.instance.attackLV)
        {
            case 2:
                atkBuff = 1.2f;
                AttackUP();
                break;
            case 3:
                spdBuff = 1.2f;
                break;
            case 4:
                break;
            case 5:
                size = 1.2f;
                break;
            case 6:
                spdBuff = 1.4f;
                break;
            case 7:
                break;
        }
    }

    private void AttackUP()
    {
        standbyWeapon[0].GetComponent<MinuteHand>().Atk *= atkBuff * bakamoriBuff;
        standbyWeapon[1].GetComponent<HourHand>().Atk *= atkBuff * bakamoriBuff;
    }

    public void Attack()
    {
        standbyWeapon[0].SetActive(true);
        standbyWeapon[1].SetActive(true);
    }

    public void AoiSpecialWeaponBuffStart()
    {
        StartCoroutine(AoiSpecialWeaponBuff());
    }

    public void BakaMoriBuffStart(int k)
    {
        StartCoroutine(BakaMoriBuff(k));
    }

    private IEnumerator TimeControl()
    {
        while (true)
        {
            while (GameManager.instance.state)
            {
                if (DataManager.instance.subArray[0, 7] == 1)
                {
                    if (attackCount >= 1000)
                    {
                        BuffDebuffManager.instance.aoiP1SpeedBuff = 1.1f;
                    }
                    else
                    {
                        BuffDebuffManager.instance.aoiP1SpeedBuff = attackCount / 10000 + 1;
                    }
                }
                else if (DataManager.instance.subArray[0, 7] == 2)
                {
                    if (attackCount >= 1000)
                    {
                        BuffDebuffManager.instance.aoiP1SpeedBuff = 1.2f;
                    }
                    else
                    {
                        BuffDebuffManager.instance.aoiP1SpeedBuff = attackCount / 5000 + 1;
                    }
                }
                else
                {
                    if (attackCount >= 1000)
                    {
                        BuffDebuffManager.instance.aoiP1SpeedBuff = 1.25f;
                    }
                    else
                    {
                        BuffDebuffManager.instance.aoiP1SpeedBuff = attackCount / 4000 + 1;
                    }
                }
                
                yield return new WaitForSeconds(10f);
            }
            if (GameManager.instance.monsterSpawn == false)
            {
                yield break;
            }
            yield return null;
        }
    }

    private IEnumerator BakaMoriStart()
    {
        while (true)
        {
            while (GameManager.instance.state)
            {
                bakamori.StartRullet();

                yield return new WaitForSeconds(30);
            }

            if (GameManager.instance.monsterSpawn == false)
            {
                yield break;
            }

            yield return null;
        }
    }

    private IEnumerator AoiSpecialWeapon()
    {
        while (true)
        {
            while (GameManager.instance.state)
            {
                for (int i = 0; i < maxCount; i++)
                {
                    if (standbySpecialWeapon[i].activeSelf == false && GameManager.instance.state)
                    {
                        float x = Random.insideUnitSphere.x * 5;
                        float y = Random.insideUnitSphere.y * 3;

                        while (Mathf.Abs(x) < 2 && Mathf.Abs(y) < 2)
                        {
                            x = Random.insideUnitSphere.x * 5;
                            y = Random.insideUnitSphere.y * 3;
                        }

                        standbySpecialWeapon[i].transform.position = new Vector3(x, y + 10, 0);
                        standbySpecialWeapon[i].GetComponent<AoiSpecialWeapon>().Target = new Vector3(x, y, 0);

                        standbySpecialWeapon[i].SetActive(true);

                        yield return new WaitForSeconds(10f);
                    }
                }
                yield return null;
            }
            if (GameManager.instance.monsterSpawn == false)
            {
                yield break;
            }
            yield return null;
        }
    }

    private IEnumerator AoiSpecialWeaponBuff()
    {
        aoiSpecialWeaponTime = GameManager.instance.time;

        while (GameManager.instance.time - aoiSpecialWeaponTime < 15)
        {
            for (int i = 0; i < GameManager.instance.weaponItemList.Count; i++)
            {
                if (GameManager.instance.weaponItemList[i].activeSelf)
                {
                    note.SetActive(true);
                    if (maxCount == 1)
                    {
                        BuffDebuffManager.instance.aoiP2SpeedBuff = 1.1f;
                        GameManager.instance.weaponItemList[i].GetComponent<Weapon>().SpeedUP();
                    }
                    else if (maxCount == 2)
                    {
                        BuffDebuffManager.instance.aoiP2SpeedBuff = 1.2f;
                        GameManager.instance.weaponItemList[i].GetComponent<Weapon>().SpeedUP();
                    }
                    else
                    {
                        BuffDebuffManager.instance.aoiP2SpeedBuff = 1.3f;
                        GameManager.instance.weaponItemList[i].GetComponent<Weapon>().SpeedUP();
                    }
                }
            }
            yield return null;
        }

        for (int i = 0; i < GameManager.instance.weaponItemList.Count; i++)
        {
            if (GameManager.instance.weaponItemList[i].activeSelf)
            {
                BuffDebuffManager.instance.aoiP2SpeedBuff = 1f;
                GameManager.instance.weaponItemList[i].GetComponent<Weapon>().SpeedUP();
            }
        }
        note.SetActive(false);
        buffCheck = true;
    }

    private IEnumerator BakaMoriBuff(int k)
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;

        bakamoriTime = GameManager.instance.time;

        while (GameManager.instance.time - bakamoriTime > 30)
        {
            if (k == 1)
            {
                if (DataManager.instance.subArray[0, 9] == 1)
                {
                    bakamoriBuff = (GameManager.instance.CharacterSpeed) / 200 + 1;
                }
                else if (DataManager.instance.subArray[0, 9] == 2)
                {
                    bakamoriBuff = (GameManager.instance.CharacterSpeed) / 150 + 1;
                }
                else
                {
                    bakamoriBuff = (GameManager.instance.CharacterSpeed) / 100 + 1;
                }
            }                
            else
            {
                PlayerManager.instance.Hp -= 1;
                if (DataManager.instance.subArray[0, 9] == 1)
                {
                    bakamoriBuff = 1.05f;
                }
                else if (DataManager.instance.subArray[0, 9] == 2)
                {
                    bakamoriBuff = 1.1f;
                }
                else
                {
                    bakamoriBuff = 1.15f;
                }   
            }
            yield return null;
        }
    }
}
