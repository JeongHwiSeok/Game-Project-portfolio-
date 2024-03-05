using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShopManager : MonoBehaviour
{
    [SerializeField] GameObject coinImage;

    [SerializeField] public Image itemImage;
    [SerializeField] public Text itemName;
    [SerializeField] public Text itemInformation;
    [SerializeField] public Text itemPrice;

    [SerializeField] Button[] button;

    [SerializeField] public Button itemButtonInfo;

    [SerializeField] Text coin;
    [SerializeField] int shopCoin;
    [SerializeField] public int itemLvPrice;
    private int rerollNumber;

    private void OnEnable()
    {
        rerollNumber = 15;
        coinImage.SetActive(true);
        button[0].onClick.AddListener(Buy);
        button[1].onClick.AddListener(Sell);
        shopCoin = DataManager.instance.data.shopCoin;
        coin.text = shopCoin.ToString();
    }

    private void Buy()
    {
        if(itemButtonInfo.name == rerollNumber.ToString())
        {
            for (int i = 0; i < DataManager.instance.data.shopInfo.Length; i++)
            {
                int maxLv = DataManager.instance.data.shopInfo[i];
                for (int j = 0; j < maxLv; j++)
                {
                    shopCoin += DictionaryManager.instance.ShopItemInfoOutput(i).LvUpPrice(j);
                    DataManager.instance.data.shopInfo[i]--;
                }
            }
        }
        else if(int.Parse(itemPrice.text) <= shopCoin)
        {
            shopCoin -= int.Parse(itemPrice.text);
            DataManager.instance.data.shopInfo[int.Parse(itemButtonInfo.gameObject.name)]++;
            DataManager.instance.Save();
        }
    }

    private void Sell()
    {
        if(DataManager.instance.data.shopInfo[int.Parse(itemButtonInfo.gameObject.name)] > 0)
        {
            DataManager.instance.data.shopInfo[int.Parse(itemButtonInfo.gameObject.name)]--;
            shopCoin += itemLvPrice;
            DataManager.instance.Save();
        }
    }

    private void OnDisable()
    {
        coinImage.SetActive(false);
    }
}
