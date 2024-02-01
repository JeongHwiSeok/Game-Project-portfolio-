using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoiWeapon : Weapon
{
    [SerializeField] public List<GameObject> aoiWeapon;

    [SerializeField] public List<GameObject> standbyWeapon;

    protected override void Create()
    {
        Transform parent = GameObject.Find("AoiWeapon").GetComponent<Transform>();

        for (int i = 0; i < 6; i++)
        {
            GameObject weapon = Instantiate(aoiWeapon[0], parent);

            weapon.SetActive(false);

            standbyWeapon.Add(weapon);
        }
        for (int i = 0; i < 1; i++)
        {
            GameObject weapon = Instantiate(aoiWeapon[1], parent);

            weapon.SetActive(false);

            standbyWeapon.Add(weapon);
        }
    }

    protected override IEnumerator Attack()
    {
        throw new System.NotImplementedException();
    }
}
