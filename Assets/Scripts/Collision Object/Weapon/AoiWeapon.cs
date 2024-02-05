using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoiWeapon : MonoBehaviour
{
    [SerializeField] public List<GameObject> aoiWeapon;

    [SerializeField] public List<GameObject> standbyWeapon;

    [SerializeField] public Vector3 direction;

    [SerializeField] Vector3 minuteHand;
    [SerializeField] Vector3 hourHand;


    public float speed;
    public int attackCount;

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
    }

    private void Start()
    {
        Create();
        Attack();
    }

    private void Create()
    {
        Transform parent = GameObject.Find("Attack Manager").GetComponent<Transform>();

        for(int i = 0; i < 3; i++)
        {
            GameObject weapon = Instantiate(aoiWeapon[i], parent);

            weapon.SetActive(false);

            standbyWeapon.Add(weapon);
        }
    }

    public void Attack()
    {
        standbyWeapon[0].SetActive(true);
        standbyWeapon[1].SetActive(true);
    }
}
