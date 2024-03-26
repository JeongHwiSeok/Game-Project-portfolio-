using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubWeaponManager : MonoBehaviour
{
    [SerializeField] List<GameObject> supportWeaponList;

    public void AddList(GameObject obj)
    {
        GameObject supportWeapon = Instantiate(obj, transform);

        supportWeaponList.Add(supportWeapon);
        GameManager.instance.weaponItemList.Add(supportWeapon);
    }

    public GameObject ResearchList(int num)
    {
        return supportWeaponList[num];
    }

    public int ListCount()
    {
        return supportWeaponList.Count;
    }
}
