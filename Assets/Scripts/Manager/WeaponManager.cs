using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] public List<Weapon> weapons;

    public static Weapon instance
    {
        get;
        private set;
    }

    public void AddWeapon(Weapon weapon)
    {
        weapons.Add(weapon);
    }
}
