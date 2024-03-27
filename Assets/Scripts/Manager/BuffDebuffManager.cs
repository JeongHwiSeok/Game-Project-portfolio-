using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDebuffManager : MonoBehaviour
{
    #region 공격력 버프
    [SerializeField] public float spAttackBuff;
    #endregion

    #region 이동속도 버프
    [SerializeField] public float aoiP1SpeedBuff;
    #endregion

    #region 공격속도 버프
    [SerializeField] public float pwsSpeedBuff;
    [SerializeField] public float aoiP2SpeedBuff;
    #endregion

    #region 디버프
    [SerializeField] public float cbCountDebuff;
    [SerializeField] public float pwsDamageDebuff;
    #endregion

    #region 그 외
    [SerializeField] public int jbCoinPow;
    [SerializeField] public float pgcExpPow;
    [SerializeField] public float shpPickUpRangePow;

    #endregion

    #region 상점 버프디버프 효과
    [SerializeField] public float shopHp;
    [SerializeField] public float shopAtk;
    [SerializeField] public float shopSpd;
    [SerializeField] public float shopCri;
    [SerializeField] public float shopPickUpRange;
    [SerializeField] public float shopAtkSpeed;
    [SerializeField] public float shopRecovery;
    [SerializeField] public float shopDefence;
    [SerializeField] public float shopSkillAtk;
    [SerializeField] public float shopExp;
    [SerializeField] public float shopCoin;
    [SerializeField] public float shopDamage;
    [SerializeField] public float shopReturn;
    [SerializeField] public float shopExclusion;
    [SerializeField] public float shopBroadCast;
    #endregion
    #region 스탯 버프디버프 효과
    [SerializeField] public float statHp;
    [SerializeField] public float statAtk;
    [SerializeField] public float statSpd;
    [SerializeField] public float statCri;
    #endregion
    public static BuffDebuffManager instance
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
    }

    private void Start()
    {
        spAttackBuff = 1;

        aoiP1SpeedBuff = 1;

        pwsSpeedBuff = 1;
        aoiP2SpeedBuff = 1;

        cbCountDebuff = 1;
        pwsDamageDebuff = 1;

        jbCoinPow = 1;
        pgcExpPow = 1;
        shpPickUpRangePow = 1;

        shopHp = DataManager.instance.data.shopInfo[0] * 10;
        shopAtk = DataManager.instance.data.shopInfo[1] * 0.05f;
        shopSpd = DataManager.instance.data.shopInfo[2] * 0.01f;
        shopCri = DataManager.instance.data.shopInfo[3];
        shopPickUpRange = DataManager.instance.data.shopInfo[4] * 0.01f + 1;
        shopAtkSpeed = DataManager.instance.data.shopInfo[5] * 0.1f + 1;
        shopRecovery = DataManager.instance.data.shopInfo[6];
        shopDefence = DataManager.instance.data.shopInfo[7] * 0.01f;
        shopSkillAtk = DataManager.instance.data.shopInfo[8] * 0.1f + 1;
        shopExp = DataManager.instance.data.shopInfo[9] * 0.1f + 1;
        shopCoin = DataManager.instance.data.shopInfo[10] * 0.1f + 1;
        shopDamage = DataManager.instance.data.shopInfo[11] * 0.1f + 1;
        shopReturn = DataManager.instance.data.shopInfo[12];
        shopExclusion = DataManager.instance.data.shopInfo[13];
        shopBroadCast = DataManager.instance.data.shopInfo[14] * 0.1f + 1;

        statHp = DataManager.instance.subArray[GameManager.instance.charNum, 2] * 10;
        statAtk = DataManager.instance.subArray[GameManager.instance.charNum, 2] * 0.05f;
        statSpd = DataManager.instance.subArray[GameManager.instance.charNum, 2] * 0.01f;
        statCri = DataManager.instance.subArray[GameManager.instance.charNum, 2] * 0.5f;
    }
}
