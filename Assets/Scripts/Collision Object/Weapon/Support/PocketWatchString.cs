using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PocketWatchString : MonoBehaviour
{
    [SerializeField] public int itemLV;
    [SerializeField] WeaponManager weaponManager;
    [SerializeField] float buff;

    private void Awake()
    {
        itemLV = 1;
        weaponManager = GameObject.Find("Attack Manager").GetComponent<WeaponManager>();
        StartCoroutine(SpeedUP());
    }
    public void Activate()
    {
        switch (itemLV)
        {
            case 1:
                GameManager.instance.pwsBuff = 1.1f;
                buff = 1.1f;
                break;
            case 2:
                GameManager.instance.pwsBuff = 1.2f;
                buff = 1.2f;
                break;
            case 3:
                GameManager.instance.pwsBuff = 1.3f;
                buff = 1.3f;
                break;
            case 4:
                GameManager.instance.pwsBuff = 1.4f;
                buff = 1.4f;
                break;
            case 5:
                GameManager.instance.pwsBuff = 1.5f;
                buff = 1.5f;
                break;
        }
        PlayerManager.instance.pwsDamage = 1.3f;
    }

    public IEnumerator SpeedUP()
    {
        while (true)
        {
            while(GameManager.instance.state)
            {
                for (int i = 0; i < weaponManager.ListCount(); i++)
                {
                    if (weaponManager.weaponsFind(i).activeSelf)
                    {
                        weaponManager.weaponsFind(i).GetComponent<Weapon>().pwsSpeedBuff = buff;
                        weaponManager.weaponsFind(i).GetComponent<Weapon>().SpeedUP();
                    }
                }
                yield return new WaitForSeconds(1f);
            }
            yield return null;
        }
    }
}
