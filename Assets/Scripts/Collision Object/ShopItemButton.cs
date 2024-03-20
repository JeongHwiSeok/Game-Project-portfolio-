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
        if(DictionaryManager.instance.ShopItemInfoOutput(int.Parse(gameObject.name)).MaxLv == DataManager.instance.data.shopInfo[int.Parse(gameObject.name)])
        {
            transform.GetComponent<Button>().interactable = false;
        }
    }

    private void ButtonClick()
    {
        itemShopManager = transform.parent.parent.parent.GetComponentInParent<ItemShopManager>();
        itemShopManager.itemButtonInfo = transform.GetComponent<Button>();

        itemShopManager.itemImage.sprite = itemImage.sprite;
        itemShopManager.itemName.text = DictionaryManager.instance.ShopItemInfoOutput(int.Parse(gameObject.name)).ShopItemName;

        itemShopManager.itemPrice.text = DictionaryManager.instance.ShopItemInfoOutput(int.Parse(gameObject.name)).LvUpPrice(DataManager.instance.data.shopInfo[int.Parse(gameObject.name)]).ToString();
        if(DataManager.instance.data.shopInfo[int.Parse(gameObject.name)] > 0)
        {
            itemShopManager.itemLvPrice = DictionaryManager.instance.ShopItemInfoOutput(int.Parse(gameObject.name)).LvUpPrice(DataManager.instance.data.shopInfo[int.Parse(gameObject.name)]-1);
        }
    }
}
