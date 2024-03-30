using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MenoManager : MonoBehaviour
{
    [SerializeField] public List<GameObject> menoWeapon;

    [SerializeField] List<GameObject> standbyWeapon;
    [SerializeField] public List<GameObject> menoBullet;

    [SerializeField] public GameObject[] hologram;

    [SerializeField] public Vector3 point;

    [SerializeField] public Vector3 direction;

    [SerializeField] public float atkBuff;
    [SerializeField] public float spdBuff;
    [SerializeField] public float knockBack;
    [SerializeField] public float duration;

    public int hologramCount;

    public int jewalRandom;

    public float jewalCount;
    [SerializeField] public Text count;

    public float pickUpBuff;

    public static MenoManager instance
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
        count.gameObject.SetActive(false);
    }

    private void Start()
    {
        atkBuff = 1;
        spdBuff = 1;
        knockBack = 0;
        duration = 0.7f;
        if (DataManager.instance.subArray[2, 7] > 0)
        {
            count.gameObject.SetActive(true);
            if (DataManager.instance.subArray[2, 7] == 1)
            {
                jewalRandom = 1;
            }
            else if (DataManager.instance.subArray[2, 7] == 2)
            {
                jewalRandom = 3;
            }
            else
            {
                jewalRandom = 5;
            }
        }
        if (DataManager.instance.subArray[2, 8] > 0)
        {
            if (DataManager.instance.subArray[2, 8] == 1)
            {
                hologramCount = 1;
            }
            else if (DataManager.instance.subArray[2, 8] == 2)
            {
                hologramCount = 2;
            }
            else
            {
                hologramCount = 3;
            }
            StartCoroutine(HologramStart());
        }
        if (DataManager.instance.subArray[2, 9] > 0)
        {
            StartCoroutine(PickUPBuff());
        }
        pickUpBuff = 1;
        InputManager.instance.keyAction += Laser;
        InputManager.instance.keyAction += ChargeBullet;
        Create();
        StartCoroutine(BulletAttack());
    }

    private void Create()
    {
        Transform parent = GameObject.Find("Attack Manager").GetComponent<Transform>();

        for (int i = 0; i < 15; i++)
        {
            GameObject weapon = Instantiate(menoWeapon[0], parent);

            weapon.SetActive(false);

            menoBullet.Add(weapon);
        }
        for(int i = 3; i < 5; i++)
        {
            GameObject weapon = Instantiate(menoWeapon[i-2], parent);

            weapon.SetActive(false);

            standbyWeapon.Add(weapon);
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
                duration = 0.56f;
                break;
            case 4:
                break;
            case 5:
                knockBack = 0.2f;
                break;
            case 6:
                atkBuff = 1.5f;
                spdBuff = 1.3f;
                break;
            case 7:
                break;
        }
    }

    private IEnumerator BulletAttack()
    {
        while(true)
        {
            while (GameManager.instance.state)
            {
                int count = 0;
                for (int i = 0; i < 15; i++)
                {
                    if (menoBullet[i].activeSelf == false && GameManager.instance.state && count < 3)
                    {
                        point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
                        menoBullet[i].SetActive(true);
                        count++;
                    }
                    if (count >= 3)
                    {
                        break;
                    }
                    yield return new WaitForSeconds(0.3f);
                }
                yield return new WaitForSeconds(duration);
            }
            if (GameManager.instance.monsterSpawn == false)
            {
                yield break;
            }
            yield return new WaitForSeconds(1f);
        }
    }
    private IEnumerator HologramStart()
    {
        while (true)
        {
            while (GameManager.instance.state)
            {
                int count = hologramCount-1;
                while (count > -1)
                {
                    if (hologram[count].activeSelf != true)
                    {
                        hologram[count].transform.position = new Vector3(Mathf.Cos(((count + 1) * 360 / hologramCount) * Mathf.Deg2Rad), Mathf.Sin(((count + 1) * 360 / hologramCount) * Mathf.Deg2Rad), 0) * 3;
                        hologram[count].SetActive(true);
                        PickUP.instance.menoHologram = ((float)(hologramCount - count))/10 + 1;
                        count--;
                    }
                    yield return new WaitForSeconds(5f);
                }
                yield return null;
            }

            if (GameManager.instance.monsterSpawn == false)
            {
                yield break;
            }

            yield return new WaitForSeconds(1f);
        }
    }
    private IEnumerator PickUPBuff()
    {
        while (true)
        {
            while (GameManager.instance.state)
            {
                if (DataManager.instance.subArray[2, 9] == 1)
                {
                    if (PickUP.instance.PickUPRange() - 1 >= 2)
                    {
                        pickUpBuff = 1.5f;
                    }
                    else
                    {
                        pickUpBuff = (PickUP.instance.PickUPRange() - 1) / 2 * 0.5f + 1;
                    }
                }
                else if (DataManager.instance.subArray[2, 9] == 2)
                {
                    if (PickUP.instance.PickUPRange() - 1 >= 1.5f)
                    {
                        pickUpBuff = 1.7f;
                    }
                    else
                    {
                        pickUpBuff = (PickUP.instance.PickUPRange() - 1) / 1.5f * 0.7f + 1;
                    }
                }
                else
                {
                    if (PickUP.instance.PickUPRange() - 1 >= 1)
                    {
                        pickUpBuff = 2f;
                    }
                    else
                    {
                        pickUpBuff = (PickUP.instance.PickUPRange() - 1) + 1;
                    }
                }

                yield return new WaitForSeconds(10f);
            }

            if (GameManager.instance.monsterSpawn == false)
            {
                yield break;
            }

            yield return new WaitForSeconds(1f);
        }
    }

    public void HolograminvincibilityStart()
    {
        StartCoroutine(Holograminvincibility());
    }
    private IEnumerator Holograminvincibility()
    {
        PlayerManager.instance.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(3f);
        PlayerManager.instance.gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }


    private void Laser()
    {
        if(Input.GetKeyDown(KeyCode.Z) && DataManager.instance.subArray[2, 7] > 0 && standbyWeapon[1].activeSelf == false && jewalCount >= 10)
        {
            standbyWeapon[0].SetActive(true);
        }
    }

    private void ChargeBullet()
    {
        if (Input.GetKeyDown(KeyCode.X) && DataManager.instance.subArray[2, 7] > 0 && standbyWeapon[0].activeSelf == false && jewalCount >= 10)
        {
            standbyWeapon[1].SetActive(true);
        }
    }

    private void OnDisable()
    {
        InputManager.instance.keyAction -= Laser;
        InputManager.instance.keyAction -= ChargeBullet;
    }
}
