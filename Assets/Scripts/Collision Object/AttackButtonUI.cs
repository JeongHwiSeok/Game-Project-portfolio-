using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackButtonUI : MonoBehaviour
{
    [SerializeField] Image attackImage;
    [SerializeField] Text attackName;
    [SerializeField] Text attackInfo;

    [SerializeField] public LevelUpUIManager levelUpUIManager;

    private bool flag;

    private void Awake()
    {
        attackImage.sprite = SpriteManager.instance.CharacterAttack(GameManager.instance.charNum);
        attackName.text = DictionaryManager.instance.CharacterInfoOutput(GameManager.instance.charNum).AttackName;
        attackInfo.text = ItemInfo();
        transform.GetComponentInChildren<Button>().onClick.AddListener(itemInput);
    }

    private void itemInput()
    {
        GameManager.instance.attackLV++;
        AoiWeapon.instance.AttackLVUP();

        GameManager.instance.state = true;
        Destroy(levelUpUIManager.gameObject);
    }

    private string ItemInfo()
    {
        string attackinfo = "";

        attackinfo = DictionaryManager.instance.AttackInformationTextOutput(GameManager.instance.charNum).AttackLvInformation(GameManager.instance.attackLV - 1);

        return attackinfo;
    }
}
