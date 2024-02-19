using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenoWeapon : MonoBehaviour
{
    [SerializeField] public List<GameObject> menoWeapon;

    [SerializeField] public List<GameObject> standbyWeapon;
    [SerializeField] public List<GameObject> menoBullet;

    [SerializeField] public Vector3 point;

    [SerializeField] public Vector3 direction;

    public float speed;
    public int attackCount;

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

    public IEnumerator BulletAttack()
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

    private void Laser()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
            standbyWeapon[0].SetActive(true);
        }
    }

    private void ChargeBullet()
    {
        if (Input.GetKeyDown(KeyCode.X))
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
