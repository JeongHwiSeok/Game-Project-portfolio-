using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemButton : MonoBehaviour
{
    [SerializeField] ItemShopManager itemShopManager;

    [SerializeField] Image itemImage;
    [SerializeField] Text itemLv;

    private void Awake()
    {
        itemLv = transform.GetChild(2).GetComponent<Text>();
        itemLv.text = DataManager.instance.data.shopInfo[int.Parse(gameObject.name)].ToString();
    }

    private void OnEnable()
    {
        transform.GetComponent<Button>().onClick.AddListener(ButtonClick);
        itemImage = transform.GetChild(0).GetComponent<Image>();
    }

    private void Update()
    {
        if (DataManager.instance.data.shopInfo[int.Parse(gameObject.name)] == DictionaryManager.instance.ShopItemInfoOutput(int.Parse(gameObject.name)).MaxLv)
        {
            itemLv.text = "Max";
        }
        else
        {
            itemLv.text = DataManager.instance.data.shopInfo[int.Parse(gameObject.name)].ToString();
        }
    }

    private void ButtonClick()
    {
        itemShopManager = transform.parent.parent.parent.GetComponentInParent<ItemShopManager>();
        itemShopManager.itemButtonInfo = transform.GetComponent<Button>();

        itemShopManager.itemImage.sprite = itemImage.sprite;
        itemShopManager.itemName.text = DictionaryManager.instance.ShopItemInfoOutput(int.Parse(gameObject.name)).ShopItemName;

        itemShopManager.itemPrice.text = DictionaryManager.instance.ShopItemInfoOutput(int.Parse(gameObject.name)).LvUpPrice(DataManager.instance.data.shopInfo[int.Parse(gameObject.name)]).ToString();
        itemShopManager.itemNumber = int.Parse(gameObject.name);

        itemShopManager.itemInformation.text = DictionaryManager.instance.ShopItemInfoOutput(int.Parse(gameObject.name)).ShopItemInformation;
    }
}
