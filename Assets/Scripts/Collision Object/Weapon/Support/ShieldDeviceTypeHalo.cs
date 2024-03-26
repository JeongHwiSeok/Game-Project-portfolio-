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
    public static ShieldDeviceTypeHalo instance
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
        flag = true;
        itemLV = 1;
        coolTime.itemNum = 18;
        shieldDeviceTypeHalo =  Instantiate(coolTime.gameObject, GameObject.Find("Canvas").transform.GetChild(5).transform);
        PlayerManager.instance.Shield = (int)(DictionaryManager.instance.CharacterInfoOutput(GameManager.instance.charNum).Hp * 0.3f);
        PlayerManager.instance.haloShield = (int)(DictionaryManager.instance.CharacterInfoOutput(GameManager.instance.charNum).Hp * 0.3f);
        PlayerManager.instance.MaxShieldAdd();
        time = GameManager.instance.time;
    }

    private void Update()
    {
        if (shieldDeviceTypeHalo.GetComponent<CoolTime>().CooltimeActiveCheck())
        {
            shieldDeviceTypeHalo.GetComponent<CoolTime>().CoverCoolTime(GameManager.instance.time - time, targetTime);
        }
        if (PlayerManager.instance.Shield < (int)(DictionaryManager.instance.CharacterInfoOutput(GameManager.instance.charNum).Hp * 0.3f) && flag == false)
        {
            flag = true;
        }
        else if (GameManager.instance.time - time >= targetTime)
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
            time = GameManager.instance.time;
        }
    }

    public void TimeReset()
    {
        time = GameManager.instance.time;
        shieldDeviceTypeHalo.GetComponent<CoolTime>().CoverFill();
    }

    public bool FlagCheck()
    {
        return flag;
    }

    public bool TimeCheck()
    {
        return GameManager.instance.time - time >= targetTime;
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
