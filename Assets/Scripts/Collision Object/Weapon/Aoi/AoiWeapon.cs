using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoiWeapon : MonoBehaviour
{
    [SerializeField] public List<GameObject> aoiWeapon;

    [SerializeField] public List<GameObject> standbyWeapon;

    [SerializeField] List<GameObject> standbySpecialWeapon;

    [SerializeField] public Vector3 direction;

    [SerializeField] WeaponManager weaponManager;

    public int attackCount;

    public bool buffCheck;

    public static AoiWeapon instance
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

        weaponManager = GameObject.Find("Attack Manager").GetComponent<WeaponManager>();
    }

    private void Start()
    {
        Create();
        Attack();
        if (DataManager.instance.subArray[0,8] > 0)
        {

        }
        StartCoroutine(AoiSpecialWeapon());
    }

    private void Create()
    {
        Transform parent = GameObject.Find("Attack Manager").transform;

        for(int i = 0; i < 5; i++)
        {
            GameObject weapon = Instantiate(aoiWeapon[i], parent);

            weapon.SetActive(false);

            standbyWeapon.Add(weapon);

            weaponManager.AddWeapon(weapon);

            if (i == 4)
            {
                weapon = Instantiate(aoiWeapon[3], parent);

                weapon.SetActive(false);

                standbyWeapon.Add(weapon);

                weaponManager.AddWeapon(weapon);
            }
        }
        for (int i = 0; i < 3; i++)
        {
            GameObject specialWeapon = Instantiate(aoiWeapon[4], GameObject.Find("Map").transform.GetChild(12).GetComponent<Transform>());

            specialWeapon.SetActive(false);

            standbySpecialWeapon.Add(specialWeapon);
        }
    }

    public void Attack()
    {
        standbyWeapon[0].SetActive(true);
        standbyWeapon[1].SetActive(true);
    }

    private IEnumerator AoiSpecialWeapon()
    {
        while (true)
        {
            while (GameManager.instance.state)
            {
                for (int i = 0; i < 3; i++)
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

                        standbySpecialWeapon[i].transform.localPosition = new Vector3(x, y + 10, 0);
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
}
