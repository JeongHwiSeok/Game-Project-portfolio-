using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DictionaryManager : Singleton<DictionaryManager>
{
    [SerializeField] TextAsset itemDataBase;
    [SerializeField] TextAsset monsterDataBase;
    [SerializeField] TextAsset characterDataBase;
    [SerializeField] TextAsset shopItemDataBase;

    private static readonly Dictionary<int, ItemInfo> itemDictionary = new Dictionary<int, ItemInfo>();
    private static readonly Dictionary<string, MonsterInfo> monsterDictionary = new Dictionary<string, MonsterInfo>();
    private static readonly Dictionary<int, CharacterInfo> characterDictionary = new Dictionary<int, CharacterInfo>();
    private static readonly Dictionary<int, ShopItemInfo> shopItemDictionary = new Dictionary<int, ShopItemInfo>();

    private void Start()
    {
        #region Ä³¸¯ÅÍ µñ¼Å³Ê¸®
        string[] characterLine = characterDataBase.text.Substring(0, characterDataBase.text.Length - 1).Split('\n');

        for (int i = 0; i < characterLine.Length; i++)
        {
            string[] row = characterLine[i].Split('\t');

            CharacterInfo characterInfo;

            if (characterDictionary.TryGetValue(int.Parse(row[0]), out characterInfo) == false)
            {
                characterInfo = new CharacterInfo(int.Parse(row[0]), row[1], float.Parse(row[2]), float.Parse(row[3]), float.Parse(row[4]), float.Parse(row[5]), row[6], row[7], row[8], row[9]);
                characterDictionary.Add(int.Parse(row[0]), characterInfo);
            }
        }
        #endregion

        #region ¾ÆÀÌÅÛ µñ¼Å³Ê¸®
        string[] itemLine = itemDataBase.text.Substring(0, itemDataBase.text.Length - 1).Split('\n');

        for (int i = 0; i < itemLine.Length; i++)
        {
            string[] row = itemLine[i].Split('\t');

            ItemInfo itemInfo;

            if (itemDictionary.TryGetValue(int.Parse(row[0]), out itemInfo) == false)
            {
                itemInfo = new ItemInfo(int.Parse(row[0]), row[1], row[2], int.Parse(row[3]), float.Parse(row[4]), float.Parse(row[5]), float.Parse(row[6]), float.Parse(row[7]), float.Parse(row[8]));
                itemDictionary.Add(int.Parse(row[0]), itemInfo);
            }
        }
        #endregion

        #region ¸ó½ºÅÍ µñ¼Å³Ê¸®
        string[] monsterLine = monsterDataBase.text.Substring(0, monsterDataBase.text.Length - 1).Split('\n');

        for (int i = 0; i < monsterLine.Length; i++)
        {
            string[] row = monsterLine[i].Split('\t');

            MonsterInfo monsterInfo;

            if (monsterDictionary.TryGetValue(row[2], out monsterInfo) == false)
            {
                monsterInfo = new MonsterInfo(int.Parse(row[0]), row[1], float.Parse(row[2]), int.Parse(row[3]));
                monsterDictionary.Add(row[1], monsterInfo);
            }
        }
        #endregion

        #region »óÁ¡¾ÆÀÌÅÛ µñ¼Å³Ê¸®
        string[] shopItemLine = shopItemDataBase.text.Substring(0, shopItemDataBase.text.Length - 1).Split('\n');

        for (int i = 0; i < shopItemLine.Length; i++)
        {
            string[] row = shopItemLine[i].Split('\t');

            ShopItemInfo shopItemInfo;

            if (shopItemDictionary.TryGetValue(int.Parse(row[0]), out shopItemInfo) == false)
            {
                int[] price = new int[10];
                for (int j = 3; j < row.Length; j++)
                {
                    price[j - 3] = int.Parse(row[j]);
                }
                shopItemInfo = new ShopItemInfo(int.Parse(row[0]), row[1], int.Parse(row[2]), price);
                shopItemDictionary.Add(int.Parse(row[0]), shopItemInfo);
            }
        }
        #endregion
    }
    public CharacterInfo CharacterInfoOutput(int number)
    {
        CharacterInfo characterInfo;

        if (characterDictionary.TryGetValue(number, out characterInfo))
        {
            characterInfo = characterDictionary[number];
            return characterInfo;
        }
        else
        {
            return null;
        }
    }
    public ItemInfo ItemInfoOutput(int number)
    {
        ItemInfo itemInfo;

        if(itemDictionary.TryGetValue(number, out itemInfo))
        {
            itemInfo = itemDictionary[number];
            return itemInfo;
        }
        else
        {
            return null;
        }
    }
    public MonsterInfo MonsterInfoOutput(string name)
    {
        MonsterInfo monsterInfo;

        if (monsterDictionary.TryGetValue(name, out monsterInfo))
        {
            monsterInfo = monsterDictionary[name];
            return monsterInfo;
        }
        else
        {
            return null;
        }
    }
    public ShopItemInfo ShopItemInfoOutput(int number)
    {
        ShopItemInfo shopItemInfo;

        if (shopItemDictionary.TryGetValue(number, out shopItemInfo))
        {
            shopItemInfo = shopItemDictionary[number];
            return shopItemInfo;
        }
        else
        {
            return null;
        }
    }
}

public class CharacterInfo
{
    int characterNumber;
    string name;
    float hp;
    float atk;
    float speed;
    float cri;
    string attackName;
    string skill1Name;
    string skill2Name;
    string skill3Name;

    public int CharacterNumber
    {
        get { return characterNumber; }
    }
    public string Name
    {
        get { return name; }
    }
    public float Hp
    {
        get { return hp; }
    }
    public float Atk
    {
        get { return atk; }
    }
    public float Speed
    {
        get { return speed; }
    }
    public float Cri
    {
        get { return cri; }
    }
    public string AttackName
    {
        get { return attackName; }
    }
    public string Skill1Name
    {
        get { return skill1Name; }
    }
    public string Skill2Name
    {
        get { return skill2Name; }
    }
    public string Skill3Name
    {
        get { return skill3Name; }
    }


    public CharacterInfo(int _characterNumber, string _name, float _hp, float _atk, float _speed, float _cri, string _attackName, string _skill1Name, string _skill2Name, string _skill3Name)
    {
        characterNumber = _characterNumber;
        name = _name;
        hp = _hp + DataManager.instance.subArray[_characterNumber, 2] * 10;
        atk = _atk + DataManager.instance.subArray[_characterNumber, 3] * 0.5f;
        speed = _speed + DataManager.instance.subArray[_characterNumber, 4] * 0.1f;
        cri = _cri + DataManager.instance.subArray[_characterNumber, 5] * 1f;
        attackName = _attackName;
        skill1Name = _skill1Name;
        skill2Name = _skill2Name;
        skill3Name = _skill3Name;
    }
}

public class MonsterInfo
{
    int monsterNumber;
    string name;
    float hp;
    int atk;

    public int MonsterNumber
    {
        get { return monsterNumber; }
    }
    public string Name
    {
        get { return name; }
    }
    public float Hp
    {
        get { return hp; }
    }
    public int Atk
    {
        get { return atk; }
    }

    public MonsterInfo(int _monsterNumber, string _name, float _hp, int _atk)
    {
        monsterNumber = _monsterNumber;
        name = _name;
        hp = _hp;
        atk = _atk;
    }
}

public class ItemInfo
{
    int itemNumber;
    string itemType;
    string name;
    int lv;
    float atk;
    float cri;
    float speed;
    float size;
    float knockBack;

    public int ItemNumber
    {
        get { return itemNumber; }
    }
    public string ItemType
    {
        get { return itemType; }
    }
    public string Name
    {
        get { return name; }
    }
    public int Lv
    {
        get { return lv; }
    }
    public float Atk
    {
        get { return atk; }
    }
    public float Cri
    {
        get { return cri; }
    }
    public float Speed
    {
        get { return speed; }
    }
    public float Size
    {
        get { return size; }
    }
    public float KnockBack
    {
        get { return knockBack; }
    }
    public ItemInfo(int _itemNumber, string _itemType, string _name, int _lv, float _atk, float _cri, float _speed, float _size, float _knockBack)
    {
        itemNumber = _itemNumber;
        itemType = _itemType;
        name = _name;
        lv = _lv;
        atk = _atk;
        cri = _cri;
        speed = _speed;
        size = _size;
        knockBack = _knockBack;
    }
}

public class ShopItemInfo
{
    int shopItemNumber;
    string shopItemName;
    int maxLv;
    int[] lvUpPrice;

    public int ShopItemNumber
    {
        get { return shopItemNumber; }
    }
    public string ShopItemName
    {
        get { return shopItemName; }
    }
    public int MaxLv
    {
        get { return maxLv; }
    }
    public int LvUpPrice(int lv)
    {
        return lvUpPrice[lv];
    }

    public ShopItemInfo(int _shopItemNumber, string _shopItemName, int _maxLv, int[] _lvUpPrice)
    {
        shopItemNumber = _shopItemNumber;
        shopItemName = _shopItemName;
        maxLv = _maxLv;
        lvUpPrice = _lvUpPrice;
    }
}
