using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpUIManager : MonoBehaviour
{
    [SerializeField] Transform parent;
    [SerializeField] GameObject itemButton;
    [SerializeField] Button[] commandButton;
    [SerializeField] Text[] statText;
    [SerializeField] GameObject canvas;

    [SerializeField] Sprite[] itemImage;

    private int addItemSlot;

    private bool[] itemNumberCheck;

    private int[] itemBox;

    private void Awake()
    {
        itemNumberCheck = new bool[16];

        addItemSlot = 16;

        canvas = GameObject.Find("Canvas");

        for (int i = 0; i < 3 + DataManager.instance.data.shopInfo[addItemSlot]; i++)
        {
            GameObject item = itemButton;
            item.GetComponent<ItemButtonUI>().itemNumber = ItemCheck();
            item.GetComponent<ItemButtonUI>().itemImage.sprite = itemImage[item.GetComponent<ItemButtonUI>().itemNumber];
            item.GetComponent<ItemButtonUI>().levelUpUIManager = this;
            Instantiate(item, parent);
        }

        statText[0].text = canvas.GetComponent<UIManager>().PlayerLv.ToString();
        statText[1].text = PlayerManager.instance.Hp + " / " + DictionaryManager.instance.CharacterInfoOutput(GameManager.instance.charNum).Hp;
        statText[2].text = " * " + PlayerManager.instance.Atk;
        statText[3].text = GameManager.instance.CharacterSpeed.ToString();
        statText[4].text = PlayerManager.instance.Cri.ToString();
    }

    private int ItemCheck()
    {
        int checkNumber;
        if (WeaponFullCheck() && SupportFullCheck())
        {
            itemBox = new int[12];
            for (int i = 0; i < 6; i++)
            {
                itemBox[i] = canvas.GetComponent<UIManager>().weaponItem[i,0];
                itemBox[i + 6] = canvas.GetComponent<UIManager>().supportItem[i,0];
            }
            checkNumber = Random.Range(0, 11);
            while (itemNumberCheck[checkNumber])
            {
                checkNumber = Random.Range(0, 11);
            }
            itemNumberCheck[checkNumber] = true;
            return itemBox[checkNumber];
        }
        else
        {
            checkNumber = Random.Range(1, 15);
            while (itemNumberCheck[checkNumber])
            {
                checkNumber = Random.Range(1, 15);
            }
            if (WeaponFullCheck())
            {
                if (DictionaryManager.instance.ItemInfoOutput(checkNumber).ItemType == "weapon")
                {
                    for (int i = 0; i < 6; i++)
                    {
                        if (canvas.GetComponent<UIManager>().weaponItem[i,0] == checkNumber)
                        {
                            return checkNumber;
                        }
                    }
                    itemBox = new int[14];
                    for (int i = 0; i < 6; i++)
                    {
                        itemBox[i] = canvas.GetComponent<UIManager>().weaponItem[i,0];
                    }
                    for (int i = 6; i < 14; i++)
                    {
                        itemBox[i] = i + 1;
                    }
                    checkNumber = Random.Range(0, 13);
                    return itemBox[checkNumber];
                }
                return checkNumber;
            }
            else if (SupportFullCheck())
            {
                if (DictionaryManager.instance.ItemInfoOutput(checkNumber).ItemType == "support")
                {
                    for (int i = 0; i < 6; i++)
                    {
                        if (canvas.GetComponent<UIManager>().supportItem[i,0] == checkNumber)
                        {
                            return checkNumber;
                        }
                    }
                    itemBox = new int[13];
                    for (int i = 0; i < 7; i++)
                    {
                        itemBox[i] = i;
                    }
                    for (int i = 7; i < 13; i++)
                    {
                        itemBox[i] = canvas.GetComponent<UIManager>().supportItem[i,0];
                    }
                    checkNumber = Random.Range(0, 12);
                    return itemBox[checkNumber];
                }
                return checkNumber;
            }
            else
            {
                itemNumberCheck[checkNumber] = true;
                return checkNumber;
            }
        }
    }

    private bool WeaponFullCheck()
    {
        if(canvas.GetComponent<UIManager>().weaponNumber == 6)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool SupportFullCheck()
    {
        if (canvas.GetComponent<UIManager>().supportNumber == 6)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnDestroy()
    {
        GameObject.Find("Canvas").GetComponent<UIManager>().flag = true;
    }
}
