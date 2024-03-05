using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButtonUI : MonoBehaviour
{
    [SerializeField] public int itemNumber;

    [SerializeField] public Image itemImage;
    [SerializeField] Text itemName;
    [SerializeField] Text itemInfo;

    [SerializeField] UIManager uIManager;

    [SerializeField] public LevelUpUIManager levelUpUIManager;

    private bool flag;

    private void Awake()
    {
        uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        itemName.text = DictionaryManager.instance.ItemInfoOutput(itemNumber).Name;
        itemInfo.text = "";

        flag = false;
        transform.GetComponentInChildren<Button>().onClick.AddListener(itemInput);
    }

    private void itemInput()
    {
        if (DictionaryManager.instance.ItemInfoOutput(itemNumber).ItemType == "weapon")
        {
            for (int i = 0; i < 6; i++)
            {
                if (uIManager.weaponItem[i, 0] == itemNumber)
                {
                    uIManager.weaponItem[i, 1]++;
                    uIManager.weaponLv[i].text = "Lv. " + uIManager.weaponItem[i, 1].ToString();
                    flag = true;
                    break;
                }
            }
            if (flag == false)
            {
                uIManager.weaponImage[uIManager.weaponNumber].sprite = itemImage.sprite;
                uIManager.weaponItem[uIManager.weaponNumber, 0] = itemNumber;
                uIManager.weaponLv[uIManager.weaponNumber].gameObject.SetActive(true);
                uIManager.weaponLv[uIManager.weaponNumber].text = "Lv. " + (++uIManager.weaponItem[uIManager.weaponNumber, 1]).ToString();
                uIManager.weaponNumber++;
            }
        }
        else
        {
            for (int i = 0; i < 6; i++)
            {
                if (uIManager.supportItem[i, 0] == itemNumber)
                {
                    uIManager.supportItem[i, 1]++;
                    uIManager.supportLv[i].text = "Lv. " + uIManager.supportItem[i, 1].ToString();
                    flag = true;
                    break;
                }
            }
            if (flag == false)
            {
                uIManager.supportImage[uIManager.supportNumber].sprite = itemImage.sprite;
                uIManager.supportItem[uIManager.supportNumber, 0] = itemNumber;
                uIManager.supportLv[uIManager.supportNumber].gameObject.SetActive(true);
                uIManager.supportLv[uIManager.supportNumber].text = "Lv. " + (++uIManager.supportItem[uIManager.supportNumber, 1]).ToString();
                uIManager.supportNumber++;
            }    
        }
        GameManager.instance.state = true;
        Destroy(levelUpUIManager.gameObject);
    }
}
