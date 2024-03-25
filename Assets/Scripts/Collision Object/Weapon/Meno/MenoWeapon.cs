using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenoWeapon : MonoBehaviour
{
    [SerializeField] public List<GameObject> menoWeapon;

    [SerializeField] public List<GameObject> standbyWeapon;
    [SerializeField] public List<GameObject> menoBullet;

    [SerializeField] public GameObject[] hologram;

    [SerializeField] public Vector3 point;

    [SerializeField] public Vector3 direction;

    public int hologramCount;

    public int jewalRandom;

    public float jewalCount;

    public float pickUpBuff;

    public static MenoWeapon instance
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
        if (DataManager.instance.subArray[2, 7] > 0)
        {
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

        for (int i = 0; i < 9; i++)
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

    private IEnumerator BulletAttack()
    {
        while(true)
        {
            int count = 0;
            point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
            for (int i = 0; i < 9; i++)
            {
                if (menoBullet[i].activeSelf == false && count < 3)
                {
                    menoBullet[i].SetActive(true);
                    count++;
                }
                else if (count >= 3)
                {
                    break;
                }

                yield return new WaitForSeconds(0.05f);
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
        if(Input.GetKeyDown(KeyCode.Z) && DataManager.instance.subArray[2, 7] > 0 && standbyWeapon[1].activeSelf == false)
        {
            point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
            standbyWeapon[0].SetActive(true);
        }
    }

    private void ChargeBullet()
    {
        if (Input.GetKeyDown(KeyCode.X) && DataManager.instance.subArray[2, 7] > 0 && standbyWeapon[0].activeSelf == false)
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
