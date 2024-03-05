using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStatManager : MonoBehaviour
{
    [SerializeField] Text characterName;
    [SerializeField] Text characterAttack;
    [SerializeField] Text attackInfo;
    [SerializeField] Image attackImage;

    private float maxHp;
    private float atk;
    private float spd;
    private float cri;

    [SerializeField] RectTransform[] statBar;

    [SerializeField] Sprite[] weaponImage;

    private void Start()
    {
        characterName.text = " ";
        characterAttack.text = " ";
        attackInfo.text = " ";
        attackImage.sprite = null;
    }

    public void CharacterStat()
    {
        switch (GameManager.instance.charNumber)
        {
            case CharacterNumber.Aoi:
                characterName.text = "Tokimori Aoi";
                characterAttack.text = ": Clock";
                attackInfo.text = "";
                attackImage.sprite = weaponImage[GameManager.instance.charNum];
                attackImage.color = new Color(1,1,1,1);
                break;
            case CharacterNumber.Iku:
                characterName.text = "Hoshifuri Iku";
                characterAttack.text = ": Ikumines";
                attackInfo.text = "";
                attackImage.sprite = weaponImage[GameManager.instance.charNum];
                attackImage.color = new Color(1, 1, 1, 1);
                break;
            case CharacterNumber.Meno:
                characterName.text = "Ibuki Meno";
                characterAttack.text = ": Electric Bullet";
                attackInfo.text = "";
                attackImage.sprite = weaponImage[GameManager.instance.charNum];
                attackImage.color = new Color(1, 1, 1, 1);

                break;
        }
        Stat();
    }

    private void Stat()
    {
        float r = 0;
        float g = 26 / 255f;
        float b = 132 / 255f;

        maxHp = DictionaryManager.instance.CharacterInfoOutput(GameManager.instance.charNum).Hp - DataManager.instance.subArray[GameManager.instance.charNum, 2] * 10;
        atk = DictionaryManager.instance.CharacterInfoOutput(GameManager.instance.charNum).Atk - DataManager.instance.subArray[GameManager.instance.charNum, 3] * 0.5f;
        spd = DictionaryManager.instance.CharacterInfoOutput(GameManager.instance.charNum).Speed - DataManager.instance.subArray[GameManager.instance.charNum, 4] * 0.1f;
        cri = DictionaryManager.instance.CharacterInfoOutput(GameManager.instance.charNum).Cri - DataManager.instance.subArray[GameManager.instance.charNum, 5] * 1f;

        statBar[0].localScale = new Vector3((1.1f * maxHp) / 100, 1, 1);
        statBar[0].gameObject.GetComponent<Image>().color = new Color(r, g, b, 1);
        statBar[1].localScale = new Vector3((1.1f * atk) / 2, 1, 1);
        statBar[1].gameObject.GetComponent<Image>().color = new Color(r, g, b, 1);
        statBar[2].localScale = new Vector3((1.1f * spd) / 2, 1, 1);
        statBar[2].gameObject.GetComponent<Image>().color = new Color(r, g, b, 1);
        statBar[3].localScale = new Vector3((1.1f * cri) / 15, 1, 1);
        statBar[3].gameObject.GetComponent<Image>().color = new Color(r, g, b, 1);
    }
}
