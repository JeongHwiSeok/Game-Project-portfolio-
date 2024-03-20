using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] List<GameObject> weapons;

    public static WeaponManager instance
    {
        get;
        private set;
    }

    public void AddWeapon(GameObject weapon)
    {
        weapons.Add(weapon);
    }

    public GameObject weaponsFind(int num)
    {
        return weapons[num];
    }

    public int ListCount()
    {
        return weapons.Count;
    }
}
