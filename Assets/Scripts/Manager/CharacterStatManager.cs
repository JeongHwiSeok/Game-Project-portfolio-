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

    [SerializeField] Text[] skillName;
    [SerializeField] Text[] skillInfo;
    [SerializeField] Image[] skillImage;

    [SerializeField] Text[] stat;

    private void Start()
    {
        characterName.text = " ";
        characterAttack.text = " ";
        attackInfo.text = " ";
        for (int i = 0; i < 3; i++)
        {
            skillName[i].text = "";
            skillInfo[i].text = "";
        }
        attackImage.sprite = null;
    }

    public void CharacterStat()
    {
        characterName.text = DictionaryManager.instance.CharacterInfoOutput(GameManager.instance.charNum).Name;
        characterAttack.text = DictionaryManager.instance.CharacterInfoOutput(GameManager.instance.charNum).AttackName;
        attackInfo.text = DictionaryManager.instance.AttackInformationTextOutput(GameManager.instance.charNum).AttackInformation;
        attackImage.sprite = SpriteManager.instance.CharacterAttack(GameManager.instance.charNum);
        attackImage.color = new Color(1, 1, 1, 1);
        for (int i = 0; i < 3 ; i++)
        {
            skillImage[i].sprite = SpriteManager.instance.SkillSprite((3 * GameManager.instance.charNum) + i);
            skillImage[i].color = new Color(1, 1, 1, 1);
        }
        Stat();
        SkillName();
        SkillInformation();
    }

    private void Stat()
    {   
        maxHp = DictionaryManager.instance.CharacterInfoOutput(GameManager.instance.charNum).Hp - DataManager.instance.subArray[GameManager.instance.charNum, 2] * 10;
        atk = DictionaryManager.instance.CharacterInfoOutput(GameManager.instance.charNum).Atk - DataManager.instance.subArray[GameManager.instance.charNum, 3] * 0.5f;
        spd = DictionaryManager.instance.CharacterInfoOutput(GameManager.instance.charNum).Speed - DataManager.instance.subArray[GameManager.instance.charNum, 4] * 0.1f;
        cri = DictionaryManager.instance.CharacterInfoOutput(GameManager.instance.charNum).Cri - DataManager.instance.subArray[GameManager.instance.charNum, 5] * 1f;

        stat[0].text = ": " + maxHp.ToString();
        stat[1].text = ": " + atk.ToString();
        stat[2].text = ": " + spd.ToString();
        stat[3].text = ": " + cri.ToString();
    }

    private void SkillName()
    {
        skillName[0].text = DictionaryManager.instance.CharacterInfoOutput(GameManager.instance.charNum).Skill1Name;
        skillName[1].text = DictionaryManager.instance.CharacterInfoOutput(GameManager.instance.charNum).Skill2Name;
        skillName[2].text = DictionaryManager.instance.CharacterInfoOutput(GameManager.instance.charNum).Skill3Name;
    }

    private void SkillInformation()
    {
        skillInfo[0].text = DictionaryManager.instance.SkillInformationTextOutput(GameManager.instance.charNum).Skill1Information;
        skillInfo[1].text = DictionaryManager.instance.SkillInformationTextOutput(GameManager.instance.charNum).Skill2Information;
        skillInfo[2].text = DictionaryManager.instance.SkillInformationTextOutput(GameManager.instance.charNum).Skill3Information;
    }
}
