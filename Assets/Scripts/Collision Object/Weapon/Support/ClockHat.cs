using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockHat : MonoBehaviour
{
    [SerializeField] BoxCollider2D playerBoxCollider2D;
    [SerializeField] CoolTime coolTime;
    [SerializeField] GameObject clockHat;

    [SerializeField] public bool flag;

    [SerializeField] float time;
    public static ClockHat instance
    {
        get;
        private set;
    }
    private void Awake()
    {
        instance = this;
        flag = true;
        playerBoxCollider2D = transform.parent.parent.parent.GetComponent<BoxCollider2D>();
        coolTime.itemNum = 11;
        clockHat =  Instantiate(coolTime.gameObject, GameObject.Find("Canvas").transform.GetChild(5).transform);
    }

    private void Update()
    {
        if (clockHat.GetComponent<CoolTime>().CooltimeActiveCheck())
        {
            clockHat.GetComponent<CoolTime>().CoverCoolTime(GameManager.instance.time - time, 30);
        }
        if (GameManager.instance.time - time >= 3f && flag == false)
        {
            InvincibilityOff();
        }
        if (GameManager.instance.time - time >= 30f)
        {
            flag = true;
        }
    }

    public void Activate()
    {
        if (flag)
        {
            playerBoxCollider2D.enabled = false;
            time = GameManager.instance.time;
            flag = false;
        }
    }

    private void InvincibilityOff()
    {
        playerBoxCollider2D.enabled = true;
        clockHat.GetComponent<CoolTime>().CoverFill();
    }
}
