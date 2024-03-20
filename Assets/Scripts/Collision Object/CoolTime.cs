using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolTime : MonoBehaviour
{
    [SerializeField] Image mainItemSprite;
    [SerializeField] Image coverSprite;
    [SerializeField] Text count;
    [SerializeField] Text cooltime;

    [SerializeField] public int itemNum;
    [SerializeField] float percent;

    private void Awake()
    {
        mainItemSprite.sprite = SpriteManager.instance.ItemSprite(itemNum);
        count.gameObject.SetActive(false);
        cooltime.gameObject.SetActive(false);
        coverSprite.fillAmount = 0;
    }

    private void Update()
    {
        coverSprite.fillAmount = percent;
    }

    public void CoverCoolTime(float time, int targetTime)
    {
        if (time >= targetTime)
        {
            cooltime.gameObject.SetActive(false);
            coverSprite.fillAmount = 0;
        }
        else
        {
            percent = ((float)targetTime - time) / targetTime;
            cooltime.text = (targetTime - time).ToString("00");
        }
    }

    public bool CooltimeActiveCheck()
    {
        return cooltime.gameObject.activeSelf;
    }

    public void CoverFill()
    {
        coverSprite.fillAmount = 1;
        cooltime.gameObject.SetActive(true);
    }

    public void CountText(string num)
    {
        count.text = num;
    }

    public void CountActivate()
    {
        count.gameObject.SetActive(true);
    }
}
