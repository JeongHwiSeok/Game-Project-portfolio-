using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDeviceTypeHalo : MonoBehaviour
{
    [SerializeField] BoxCollider2D playerBoxCollider2D;
    [SerializeField] CoolTime coolTime;
    [SerializeField] GameObject shieldDeviceTypeHalo;

    [SerializeField] float time;

    [SerializeField] bool flag;

    [SerializeField] int targetTime;

    [SerializeField] public int itemLV;

    private void Awake()
    {
        flag = true;
        itemLV = 1;
        coolTime.itemNum = 18;
        shieldDeviceTypeHalo =  Instantiate(coolTime.gameObject, GameObject.Find("Canvas").transform.GetChild(5).transform);
        PlayerManager.instance.Shield = (int)(DictionaryManager.instance.CharacterInfoOutput(GameManager.instance.charNum).Hp * 0.3f);
        PlayerManager.instance.haloShield = (int)(DictionaryManager.instance.CharacterInfoOutput(GameManager.instance.charNum).Hp * 0.3f);
        PlayerManager.instance.MaxShieldAdd();
        PlayerManager.instance.shieldDeviceTypeHalo = this;
        time = UIManager.instance.time;
    }

    private void Update()
    {
        if (shieldDeviceTypeHalo.GetComponent<CoolTime>().CooltimeActiveCheck())
        {
            shieldDeviceTypeHalo.GetComponent<CoolTime>().CoverCoolTime(UIManager.instance.time - time, targetTime);
        }
        if (PlayerManager.instance.Shield < (int)(DictionaryManager.instance.CharacterInfoOutput(GameManager.instance.charNum).Hp * 0.3f) && flag == false)
        {
            flag = true;
        }
        else if (UIManager.instance.time - time >= targetTime)
        {
            Activate();
        }
    }

    public void Activate()
    {
        if (flag)
        {
            PlayerManager.instance.Shield = (int)(DictionaryManager.instance.CharacterInfoOutput(GameManager.instance.charNum).Hp * 0.3f);
            flag = false;
            shieldDeviceTypeHalo.GetComponent<CoolTime>().CoverFill();
            time = UIManager.instance.time;
        }
    }

    public void TimeReset()
    {
        time = UIManager.instance.time;
        shieldDeviceTypeHalo.GetComponent<CoolTime>().CoverFill();
    }

    public bool FlagCheck()
    {
        return flag;
    }

    public bool TimeCheck()
    {
        return UIManager.instance.time - time >= targetTime;
    }

    public void LevelCheck()
    {
        switch (itemLV)
        {
            case 1:
                targetTime = 15;
                break;
            case 2:
                targetTime = 10;
                break;
            case 3:
                targetTime = 5;
                break;
        }
    }
}
