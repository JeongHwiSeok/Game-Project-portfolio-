using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDebuffManager : MonoBehaviour
{
    #region ���ݷ� ����
    [SerializeField] public float spAttackBuff;
    #endregion

    #region �̵��ӵ� ����
    [SerializeField] public float aoiP1SpeedBuff;
    #endregion

    #region ���ݼӵ� ����
    [SerializeField] public float pwsSpeedBuff;
    [SerializeField] public float aoiP2SpeedBuff;
    #endregion

    #region �����
    [SerializeField] public float cbCountDebuff;
    [SerializeField] public float pwsDamageDebuff;
    #endregion

    #region �� ��
    [SerializeField] public int jbCoinPow;
    [SerializeField] public float pgcExpPow;
    [SerializeField] public float shpPickUpRangePow;

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
    }
}
