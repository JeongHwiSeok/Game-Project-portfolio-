using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IkuManager : MonoBehaviour
{
    [SerializeField] public List<GameObject> ikuWeapon;

    [SerializeField] public List<GameObject> standbyWeapon;

    [SerializeField] public List<GameObject> standbySpecial;

    [SerializeField] public Text ikuminText;

    [SerializeField] GameObject[] specialIkumin;

    [SerializeField] Transform parent;

    [SerializeField] public Vector3 direction;

    [SerializeField] public float ikuminCount;

    [SerializeField] float atkBuff;
    [SerializeField] float atkspdBuff;
    [SerializeField] float spdBuff;
    [SerializeField] float duration;
    [SerializeField] float size;
    [SerializeField] float coolTime;

    [SerializeField] public bool ikuminStackFlag;
    [SerializeField] public bool ikuminBoom;

    public int attackCount;

    public static IkuManager instance
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
        ikuminText.gameObject.SetActive(false);
    }

    private void Start()
    {
        parent = GameObject.Find("Map").transform.GetChild(12);
        atkBuff = 1;
        atkspdBuff = 1;
        spdBuff = 1;
        duration = 1;
        ikuminStackFlag = false;
        size = 1;
        ikuminBoom = false;
        Create();
        StartCoroutine(IkuminAttack());
        if (DataManager.instance.subArray[1, 7] > 0)
        {
            ikuminText.gameObject.SetActive(true);
            StartCoroutine(IkuminStackUp());
        }
        if (DataManager.instance.subArray[1, 8] > 0)
        {
            if (DataManager.instance.subArray[1, 8] == 1)
            {
                coolTime = 15;
            }
            else if (DataManager.instance.subArray[1, 8] == 2)
            {
                coolTime = 10;
            }
            else
            {
                coolTime = 5;
            }
            StartCoroutine(SpecialIkumin());
        }
        if (DataManager.instance.subArray[1, 9] > 0)
        {
            StartCoroutine(IkuminStack());
        }
    }

    private void Create()
    {
        for (int i = 0; i < 9; i++)
        {
            GameObject weapon = Instantiate(ikuWeapon[0], parent);

            weapon.SetActive(false);

            standbyWeapon.Add(weapon);
        }
        for (int i = 0; i < 2; i++)
        {
            GameObject weapon = Instantiate(specialIkumin[i], parent);

            weapon.SetActive(false);

            standbySpecial.Add(weapon);
        }
    }

    public void AttackLVUP()
    {
        switch (GameManager.instance.attackLV)
        {
            case 2:
                atkBuff = 1.2f;
                break;
            case 3:
                duration = 0.8f;
                break;
            case 4:
                ikuminStackFlag = true;
                break;
            case 5:
                size = 1.2f;
                break;
            case 6:
                atkBuff = 1.4f;
                break;
            case 7:
                ikuminBoom = true;
                break;
        }
    }

    public IEnumerator IkuminAttack()
    {
        while (true)
        {
            while (GameManager.instance.state)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (standbyWeapon[i].activeSelf == false && GameManager.instance.state)
                    {
                        standbyWeapon[i].transform.localScale = new Vector3(0.7f * size, 0.7f * size, 0.7f * size);
                        standbyWeapon[i].GetComponent<IkuminAttack>().Atk = 30 * atkBuff * BuffDebuffManager.instance.spAttackBuff;
                        standbyWeapon[i].SetActive(true);

                        if (GameManager.instance.attackLV == 7)
                        {
                            GameManager.instance.ikuminCount++;
                        }
                    }
                    yield return new WaitForSeconds(duration);
                }
            }
            yield return null;
        }
    }

    private IEnumerator IkuminStackUp()
    {
        while (true)
        {
            while (GameManager.instance.state)
            {
                if (DataManager.instance.subArray[1, 7] == 1)
                {
                    ikuminCount += 1;
                    ikuminText.text = ikuminCount.ToString();
                }
                else if (DataManager.instance.subArray[1, 7] == 2)
                {
                    ikuminCount += 3;
                    ikuminText.text = ikuminCount.ToString();
                }
                else
                {
                    ikuminCount += 5;
                    ikuminText.text = ikuminCount.ToString();
                }

                yield return new WaitForSeconds(5f);
            }
            if (GameManager.instance.monsterSpawn == false)
            {
                yield break;
            }
            yield return null;
        }
    }

    private IEnumerator SpecialIkumin()
    {
        while (true)
        {
            while (GameManager.instance.state)
            {
                int random = Random.Range(0, 100);
                if (random < 5)
                {
                    random = Random.Range(0, 2);

                    if (standbySpecial[random].activeSelf == false)
                    {
                        standbySpecial[random].SetActive(true);
                    }
                }
                yield return new WaitForSeconds(coolTime);
            }
            if (GameManager.instance.monsterSpawn == false)
            {
                yield break;
            }
            yield return null;
        }
    }

    private IEnumerator IkuminStack()
    {
        while (true)
        {
            while (GameManager.instance.state)
            {
                if (DataManager.instance.subArray[1, 9] == 1)
                {
                    if (ikuminCount <= 100)
                    {
                        atkBuff = ikuminCount / 300 + 1;
                        atkspdBuff = ikuminCount / 600 + 1;
                        spdBuff = ikuminCount / 600 + 1;
                    }
                    else
                    {
                        atkBuff = 2;
                        atkspdBuff = 1.5f;
                        spdBuff = 1.5f;
                    }
                }
                else if (DataManager.instance.subArray[1, 9] == 2)
                {
                    if (ikuminCount <= 100)
                    {
                        atkBuff = ikuminCount / 200 + 1;
                        atkspdBuff = ikuminCount / 400 + 1;
                        spdBuff = ikuminCount / 400 + 1;
                    }
                    else
                    {
                        atkBuff = 2;
                        atkspdBuff = 1.5f;
                        spdBuff = 1.5f;
                    }
                }
                else
                {
                    if (ikuminCount <= 100)
                    {
                        atkBuff = ikuminCount / 100 + 1;
                        atkspdBuff = ikuminCount / 200 + 1;
                        spdBuff = ikuminCount / 200 + 1;
                    }
                    else
                    {
                        atkBuff = 2;
                        atkspdBuff = 1.5f;
                        spdBuff = 1.5f;
                    }
                } 

                yield return new WaitForSeconds(5f);
            }
            if (GameManager.instance.monsterSpawn == false)
            {
                yield break;
            }
            yield return null;
        }
    }
}
