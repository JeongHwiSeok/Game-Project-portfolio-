using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpUIManager : MonoBehaviour
{
    [SerializeField] Transform parent;
    [SerializeField] GameObject itemButton;
    [SerializeField] GameObject attackButton;
    [SerializeField] Button[] commandButton;
    [SerializeField] Text[] statText;
    [SerializeField] GameObject canvas;

    [SerializeField] Sprite[] itemImage;

    private int addItemSlot;

    private int maxSlot;

    [SerializeField] private int[] itemBox;

    [SerializeField] bool check;

    private void Awake()
    {
        for (int i = 1; i < 26; i++)
        {
            GameManager.instance.itemNumberCheck[i] = false;
            if (GameManager.instance.itemLvCheck[i] == DictionaryManager.instance.ItemInfoOutput(i).MaxLV)
            {
                GameManager.instance.itemNumberCheck[i] = true;
            }
        }

        addItemSlot = 16;

        maxSlot = 3 + DataManager.instance.data.shopInfo[addItemSlot];

        canvas = GameObject.Find("Canvas");

        for (int i = 0; i < maxSlot; i++)
        {
            GameObject item = itemButton;
            if (i == 5)
            {
                item = attackButton;
                item.GetComponent<ItemButtonUI>().levelUpUIManager = this;
                Instantiate(item, parent);
                break;
            }
            item.GetComponent<ItemButtonUI>().itemNumber = ItemCheck();
            if (item.GetComponent<ItemButtonUI>().itemNumber == 0)
            {
                item.GetComponent<ItemButtonUI>().itemNumber = SubItemCheck();
            }
            item.GetComponent<ItemButtonUI>().itemImage.sprite = SpriteManager.instance.ItemSprite(item.GetComponent<ItemButtonUI>().itemNumber);
            item.GetComponent<ItemButtonUI>().levelUpUIManager = this;
            Instantiate(item, parent);
        }
        if (GameManager.instance.attackLV < 6 && maxSlot < 5)
        {
            GameObject item = attackButton;
            item.GetComponent<AttackButtonUI>().levelUpUIManager = this;
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
        check = true;
        int checkNumber;
        if (WeaponFullCheck() && SupportFullCheck())
        {
            itemBox = new int[12];
            for (int i = 0; i < 6; i++)
            {
                if (canvas.GetComponent<UIManager>().weaponItem[i, 1] < 7)
                {
                    itemBox[i] = canvas.GetComponent<UIManager>().weaponItem[i, 0];
                }
                if (canvas.GetComponent<UIManager>().supportItem[i, 1] < DictionaryManager.instance.ItemInfoOutput(canvas.GetComponent<UIManager>().supportItem[i, 0]).MaxLV)
                {
                    itemBox[i + 6] = canvas.GetComponent<UIManager>().supportItem[i, 0];
                }
            }
            for (int i = 0; i < 12; i++)
            {
                if (GameManager.instance.itemNumberCheck[itemBox[i]] != true)
                {
                    check = false;
                    break;
                }
            }
            if (check)
            {
                return 0;
            }
            checkNumber = Random.Range(0, 12);
            while (GameManager.instance.itemNumberCheck[itemBox[checkNumber]])
            {
                checkNumber = Random.Range(0, 12);
            }
            GameManager.instance.itemNumberCheck[itemBox[checkNumber]] = true;
            return itemBox[checkNumber];
        }
        else
        {
            checkNumber = Random.Range(1, 1 + GameManager.instance.weaponcount + GameManager.instance.supportcount);

            while (GameManager.instance.itemNumberCheck[checkNumber])
            {
                checkNumber = Random.Range(1, 1 + GameManager.instance.weaponcount + GameManager.instance.supportcount);
            }

            if (WeaponFullCheck())
            {
                if (DictionaryManager.instance.ItemInfoOutput(checkNumber).ItemType == "weapon")
                {
                    itemBox = new int[6 + GameManager.instance.supportcount];
                    for (int i = 0; i < 6; i++)
                    {
                        if (canvas.GetComponent<UIManager>().weaponItem[i, 1] < 7)
                        {
                            itemBox[i] = canvas.GetComponent<UIManager>().weaponItem[i, 0];
                        }
                    }
                    for (int i = 1; i < GameManager.instance.supportcount + 1; i++)
                    {
                        itemBox[i + 5] = i + GameManager.instance.supportcount;
                    }
                    checkNumber = Random.Range(0, 6 + GameManager.instance.supportcount);
                    while (GameManager.instance.itemNumberCheck[itemBox[checkNumber]] || itemBox[checkNumber] == 0)
                    {
                        checkNumber = Random.Range(0, 6 + GameManager.instance.supportcount);
                    }
                    GameManager.instance.itemNumberCheck[itemBox[checkNumber]] = true;
                    return itemBox[checkNumber];
                }
                while (GameManager.instance.itemNumberCheck[checkNumber])
                {
                    checkNumber = Random.Range(GameManager.instance.weaponcount +1 , GameManager.instance.supportcount + GameManager.instance.weaponcount);
                }
                GameManager.instance.itemNumberCheck[checkNumber] = true;
                return checkNumber;
            }
            else if (SupportFullCheck())
            {
                if (DictionaryManager.instance.ItemInfoOutput(checkNumber).ItemType == "support")
                {
                    itemBox = new int[6 + GameManager.instance.weaponcount];
                    for (int i = 0; i < GameManager.instance.weaponcount; i++)
                    {
                        itemBox[i] = i+1;
                    }
                    for (int i = 0; i < 6; i++)
                    {
                        if (canvas.GetComponent<UIManager>().supportItem[i, 1] < DictionaryManager.instance.ItemInfoOutput(canvas.GetComponent<UIManager>().supportItem[i, 0]).MaxLV)
                        {
                            itemBox[GameManager.instance.weaponcount + i] = canvas.GetComponent<UIManager>().supportItem[i, 0];
                        }
                    }
                    checkNumber = Random.Range(0, 6 + GameManager.instance.weaponcount);
                    while (GameManager.instance.itemNumberCheck[itemBox[checkNumber]] || itemBox[checkNumber] == 0)
                    {
                        checkNumber = Random.Range(0, 6 + GameManager.instance.weaponcount);
                    }
                    GameManager.instance.itemNumberCheck[itemBox[checkNumber]] = true;
                    return itemBox[checkNumber];
                }
                while (GameManager.instance.itemNumberCheck[checkNumber])
                {
                    checkNumber = Random.Range(1, GameManager.instance.weaponcount);
                }
                GameManager.instance.itemNumberCheck[checkNumber] = true;
                return checkNumber;
            }
            else
            {
                GameManager.instance.itemNumberCheck[checkNumber] = true;
                return checkNumber;
            }
        }
    }

    private int SubItemCheck()
    {
        int random = Random.Range(GameManager.instance.supportcount+GameManager.instance.weaponcount + 1, GameManager.instance.supportcount + GameManager.instance.weaponcount + 5);
        while (GameManager.instance.itemNumberCheck[random])
        {
            random = Random.Range(GameManager.instance.supportcount + GameManager.instance.weaponcount + 1, GameManager.instance.supportcount + GameManager.instance.weaponcount + 5);
        }
        GameManager.instance.itemNumberCheck[random] = true;
        return random;
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
