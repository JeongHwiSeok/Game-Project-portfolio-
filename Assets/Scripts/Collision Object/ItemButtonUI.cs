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

    [SerializeField] SupportItemManager supportItemManager;
    [SerializeField] SubWeaponManager subWeaponManager;

    [SerializeField] List<GameObject> supportItemList;
    [SerializeField] List<GameObject> subWeaponList;

    private bool flag;

    private void Awake()
    {
        supportItemManager = GameObject.FindWithTag("Player").transform.GetChild(2).GetChild(2).GetComponent<SupportItemManager>();
        subWeaponManager = GameObject.FindWithTag("Player").transform.GetChild(2).GetChild(1).GetComponent<SubWeaponManager>();
        uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        itemName.text = DictionaryManager.instance.ItemInfoOutput(itemNumber).Name;
        itemInfo.text = ItemInfo();

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
                    GameManager.instance.itemLvCheck[itemNumber]++;
                    uIManager.weaponLv[i].text = "Lv. " + uIManager.weaponItem[i, 1].ToString();
                    flag = true;
                    ItemCheck(GameManager.instance.itemLvCheck[itemNumber]);
                    break;
                }
            }
            if (flag == false)
            {
                uIManager.weaponImage[uIManager.weaponNumber].sprite = itemImage.sprite;
                uIManager.weaponItem[uIManager.weaponNumber, 0] = itemNumber;
                uIManager.weaponLv[uIManager.weaponNumber].gameObject.SetActive(true);
                uIManager.weaponLv[uIManager.weaponNumber].text = "Lv. " + (++uIManager.weaponItem[uIManager.weaponNumber, 1]).ToString();
                GameManager.instance.itemLvCheck[itemNumber]++;
                uIManager.weaponNumber++;
                subWeaponManager.AddList(subWeaponList[itemNumber]);
                ItemCheck(GameManager.instance.itemLvCheck[itemNumber]);
            }
        }
        else if(DictionaryManager.instance.ItemInfoOutput(itemNumber).ItemType == "support")
        {
            for (int i = 0; i < 6; i++)
            {
                if (uIManager.supportItem[i, 0] == itemNumber)
                {
                    uIManager.supportItem[i, 1]++;
                    GameManager.instance.itemLvCheck[itemNumber]++;
                    uIManager.supportLv[i].text = "Lv. " + uIManager.supportItem[i, 1].ToString();
                    flag = true;
                    ItemCheck(GameManager.instance.itemLvCheck[itemNumber]);
                    break;
                }
            }
            if (flag == false)
            {
                uIManager.supportImage[uIManager.supportNumber].sprite = itemImage.sprite;
                uIManager.supportItem[uIManager.supportNumber, 0] = itemNumber;
                uIManager.supportLv[uIManager.supportNumber].gameObject.SetActive(true);
                uIManager.supportLv[uIManager.supportNumber].text = "Lv. " + (++uIManager.supportItem[uIManager.supportNumber, 1]).ToString();
                GameManager.instance.itemLvCheck[itemNumber]++;
                uIManager.supportNumber++;
                supportItemManager.AddList(supportItemList[itemNumber - 10]);
                ItemCheck(GameManager.instance.itemLvCheck[itemNumber]);
            }    
        }
        else
        {
            string etcname = DictionaryManager.instance.ItemInfoOutput(itemNumber).Name;
            switch (etcname)
            {
                case "Attack+":
                    PlayerManager.instance.Atk += 0.1f;
                    break;
                case "Speed+":
                    GameManager.instance.CharacterSpeed += 0.1f;
                    break;
                case "Critical+":
                    PlayerManager.instance.Cri += 0.05f;
                    break;
                case "Recovery":
                    if ((int)(DictionaryManager.instance.CharacterInfoOutput(GameManager.instance.charNum).Hp - PlayerManager.instance.Hp) <= (int)(DictionaryManager.instance.CharacterInfoOutput(GameManager.instance.charNum).Hp * 0.2f))
                    {
                        PlayerManager.instance.Hp = (int)(DictionaryManager.instance.CharacterInfoOutput(GameManager.instance.charNum).Hp);
                    }
                    else
                    {
                        PlayerManager.instance.Hp += (int)(DictionaryManager.instance.CharacterInfoOutput(GameManager.instance.charNum).Hp * 0.2f);
                    }
                    break;
                case "Coin":
                    UIManager.instance.DropCoin += 200;
                    break;
            }
        }
        GameManager.instance.state = true;
        Destroy(levelUpUIManager.gameObject);
    }

    private string ItemInfo()
    {
        string iteminfo = "";

        iteminfo = DictionaryManager.instance.ItemInformationTextOutput(DictionaryManager.instance.ItemInfoOutput(itemNumber).Name).LVInformation(GameManager.instance.itemLvCheck[itemNumber]);

        return iteminfo;
    }

    private void ItemCheck(int level)
    {
        if (itemNumber == 1)
        {
            for (int i = 0; i < 6; i++)
            {
                if (subWeaponManager.ResearchList(i).GetComponent<TailTypeLaser>() != null)
                {
                    subWeaponManager.ResearchList(i).GetComponent<TailTypeLaser>().itemLV = level;
                    subWeaponManager.ResearchList(i).GetComponent<TailTypeLaser>().Activate();
                    return;
                }
            }
        }
        else if (itemNumber == 2)
        {
            for (int i = 0; i < 6; i++)
            {
                if (subWeaponManager.ResearchList(i).GetComponent<CrawlingMushroom>() != null)
                {
                    subWeaponManager.ResearchList(i).GetComponent<CrawlingMushroom>().itemLV = level;
                    subWeaponManager.ResearchList(i).GetComponent<CrawlingMushroom>().Activate();
                    return;
                }
            }
        }
        else if (itemNumber == 3)
        {
            for (int i = 0; i < 6; i++)
            {
                if (subWeaponManager.ResearchList(i).GetComponent<Mike>() != null)
                {
                    subWeaponManager.ResearchList(i).GetComponent<Mike>().itemLV = level;
                    subWeaponManager.ResearchList(i).GetComponent<Mike>().Activate();
                    return;
                }
            }
        }
        else if (itemNumber == 4)
        {
            for (int i = 0; i < 6; i++)
            {
                if (subWeaponManager.ResearchList(i).GetComponent<DevilsTail>() != null)
                {
                    subWeaponManager.ResearchList(i).GetComponent<DevilsTail>().itemLV = level;
                    subWeaponManager.ResearchList(i).GetComponent<DevilsTail>().Activate();
                    return;
                }
            }
        }
        else if (itemNumber == 5)
        {
            for (int i = 0; i < 6; i++)
            {
                if (subWeaponManager.ResearchList(i).GetComponent<EternityFlame>() != null)
                {
                    subWeaponManager.ResearchList(i).GetComponent<EternityFlame>().itemLV = level;
                    subWeaponManager.ResearchList(i).GetComponent<EternityFlame>().Activate();
                    return;
                }
            }
        }
        else if (itemNumber == 6)
        {
            for (int i = 0; i < 6; i++)
            {
                if (subWeaponManager.ResearchList(i).GetComponent<IkuminTransmitter>() != null)
                {
                    subWeaponManager.ResearchList(i).GetComponent<IkuminTransmitter>().itemLV = level;
                    subWeaponManager.ResearchList(i).GetComponent<IkuminTransmitter>().Activate();
                    return;
                }
            }
        }
        else if (itemNumber == 7)
        {
            for (int i = 0; i < 6; i++)
            {
                if (subWeaponManager.ResearchList(i).GetComponent<SnakeChakram>() != null)
                {
                    subWeaponManager.ResearchList(i).GetComponent<SnakeChakram>().itemLV = level;
                    subWeaponManager.ResearchList(i).GetComponent<SnakeChakram>().Activate();
                    return;
                }
            }
        }
        else if (itemNumber == 8)
        {
            for (int i = 0; i < 6; i++)
            {
                if (subWeaponManager.ResearchList(i).GetComponent<RainBoots>() != null)
                {
                    subWeaponManager.ResearchList(i).GetComponent<RainBoots>().itemLV = level;
                    subWeaponManager.ResearchList(i).GetComponent<RainBoots>().Activate();
                    return;
                }
            }
        }
        else if (itemNumber == 9)
        {
            for (int i = 0; i < 6; i++)
            {
                if (subWeaponManager.ResearchList(i).GetComponent<TrickCard>() != null)
                {
                    subWeaponManager.ResearchList(i).GetComponent<TrickCard>().itemLV = level;
                    subWeaponManager.ResearchList(i).GetComponent<TrickCard>().Activate();
                    return;
                }
            }
        }
        else if (itemNumber == 12)
        {
            for (int i = 0; i < 6; i++)
            {
                if (supportItemManager.ResearchList(i).GetComponent<JewalBox>() != null)
                {
                    supportItemManager.ResearchList(i).GetComponent<JewalBox>().itemLV = level;
                    supportItemManager.ResearchList(i).GetComponent<JewalBox>().Activate();
                    return;
                }
            }
        }
        else if (itemNumber == 14)
        {
            for (int i = 0; i < 6; i++)
            {
                if (supportItemManager.ResearchList(i).GetComponent<SpecialHairpin>() != null)
                {
                    supportItemManager.ResearchList(i).GetComponent<SpecialHairpin>().itemLV = level;
                    supportItemManager.ResearchList(i).GetComponent<SpecialHairpin>().Activate();
                    return;
                }
            }
        }
        else if(itemNumber == 15)
        {
            for (int i = 0; i < 6; i++)
            {
                if (supportItemManager.ResearchList(i).GetComponent<PocketWatchString>() != null)
                {
                    supportItemManager.ResearchList(i).GetComponent<PocketWatchString>().itemLV = level;
                    supportItemManager.ResearchList(i).GetComponent<PocketWatchString>().Activate();
                    return;
                }
            }
        }
        else if (itemNumber == 16)
        {
            for (int i = 0; i < 6; i++)
            {
                if (supportItemManager.ResearchList(i).GetComponent<Chickennuggie>() != null)
                {
                    supportItemManager.ResearchList(i).GetComponent<Chickennuggie>().itemLV = level;
                    supportItemManager.ResearchList(i).GetComponent<Chickennuggie>().Activate();
                    return;
                }
            }
        }
        else if (itemNumber == 17)
        {
            for (int i = 0; i < 6; i++)
            {
                if (supportItemManager.ResearchList(i).GetComponent<CatsBell>() != null)
                {
                    supportItemManager.ResearchList(i).GetComponent<CatsBell>().itemLV = level;
                    supportItemManager.ResearchList(i).GetComponent<CatsBell>().Activate();
                    return;
                }
            }
        }
        else if (itemNumber == 18)
        {
            for (int i = 0; i < 6; i++)
            {
                if (supportItemManager.ResearchList(i).GetComponent<ShieldDeviceTypeHalo>() != null)
                {
                    supportItemManager.ResearchList(i).GetComponent<ShieldDeviceTypeHalo>().itemLV = level;
                    supportItemManager.ResearchList(i).GetComponent<ShieldDeviceTypeHalo>().LevelCheck();
                    return;
                }
            }
        }
        else if (itemNumber == 19)
        {
            for (int i = 0; i < 6; i++)
            {
                if (supportItemManager.ResearchList(i).GetComponent<PuzzleGameCollection>() != null)
                {
                    supportItemManager.ResearchList(i).GetComponent<PuzzleGameCollection>().itemLV = level;
                    supportItemManager.ResearchList(i).GetComponent<PuzzleGameCollection>().Activate();
                    return;
                }
            }
        }
        else if (itemNumber == 20)
        {
            for (int i = 0; i < 6; i++)
            {
                if (supportItemManager.ResearchList(i).GetComponent<StrangePicture>() != null)
                {
                    supportItemManager.ResearchList(i).GetComponent<StrangePicture>().itemLV = level;
                    supportItemManager.ResearchList(i).GetComponent<StrangePicture>().Activate();
                    return;
                }
            }
        }
    }
}