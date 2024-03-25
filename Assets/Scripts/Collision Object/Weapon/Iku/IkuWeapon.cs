using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IkuWeapon : MonoBehaviour
{
    [SerializeField] public List<GameObject> ikuWeapon;

    [SerializeField] public List<GameObject> standbyWeapon;

    [SerializeField] GameObject[] specialIkumin;

    [SerializeField] Transform parent;

    [SerializeField] public Vector3 direction;

    [SerializeField] float ikuminCount;

    [SerializeField] float atkBuff;
    [SerializeField] float atkspdBuff;
    [SerializeField] float spdBuff;

    public int attackCount;

    public static IkuWeapon instance
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
    }

    private void Start()
    {
        parent = GameObject.Find("Attack Manager").GetComponent<Transform>();
        atkBuff = 1;
        atkspdBuff = 1;
        spdBuff = 1;
        Create();
        StartCoroutine(IkuminAttack());
        if (DataManager.instance.subArray[1, 7] > 0)
        {
            StartCoroutine(IkuminStackUp());
        }
        if (DataManager.instance.subArray[1, 8] > 0)
        {
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
    }

    public IEnumerator IkuminAttack()
    {
        while (true)
        {
            
            for (int i = 0; i < 9; i++)
            {
                if (standbyWeapon[i].activeSelf == false)
                {
                    standbyWeapon[i].SetActive(true);

                    if (GameManager.instance.attackLV == 7)
                    {
                        GameManager.instance.ikuminCount++;
                    }
                }
                yield return new WaitForSeconds(3f);
            }
        }
    }

    private IEnumerator IkuminStackUp()
    {
        while (true)
        {
            while (GameManager.instance.state)
            {
                ikuminCount += 3;

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
                int random = Random.Range(0, 2);

                Instantiate(specialIkumin[random], parent);

                yield return new WaitForSeconds(10f);
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
