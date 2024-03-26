using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        MenoLaser menoLaser = other.GetComponent<MenoLaser>();

        if (menoLaser != null)
        {
            return;
        }

        IkuminAttack Ikumin = other.GetComponent<IkuminAttack>();

        if(Ikumin != null)
        {
            return;
        }

        Weapon weapon = other.GetComponent<Weapon>();

        if (weapon != null)
        {
            weapon.gameObject.SetActive(false);
        }
    }
}
