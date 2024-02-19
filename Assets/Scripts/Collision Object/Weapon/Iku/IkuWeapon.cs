using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IkuWeapon : MonoBehaviour
{
    [SerializeField] public List<GameObject> ikuWeapon;

    [SerializeField] public List<GameObject> standbyWeapon;

    [SerializeField] public Vector3 direction;

    public float speed;
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
        Create();
        StartCoroutine(IkuminAttack());
    }

    private void Create()
    {
        Transform parent = GameObject.Find("Attack Manager").GetComponent<Transform>();

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
                }
                yield return new WaitForSeconds(3f);
            }
        }
    }
}
