using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoiWeapon : MonoBehaviour
{
    [SerializeField] public List<GameObject> aoiWeapon;

    [SerializeField] public List<GameObject> standbyWeapon;

    [SerializeField] MinuteHand minuteHand;

    [SerializeField] public Vector3 direction;

    public float speed;
    public int attackCount;

    public static AoiWeapon instance
    {
        get;
        private set;
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

            weapon.transform.position = new Vector3(0, 1.5f, 0);

            weapon.SetActive(false);

            standbyWeapon.Add(weapon);
        }
    }

    public void Attack()
    {
        standbyWeapon[0].SetActive(true);
        standbyWeapon[1].SetActive(true);
        StartCoroutine(Unison());
    }

    public IEnumerator Unison()
    {
        while(GameManager.instance.state)
        {
            yield return new WaitForSeconds(4320 / minuteHand.Speed);
            if (standbyWeapon[2].activeSelf == false)
            {
                standbyWeapon[0].SetActive(false);
                standbyWeapon[1].SetActive(false);
                standbyWeapon[2].SetActive(true);
            }
        }
    }
}
