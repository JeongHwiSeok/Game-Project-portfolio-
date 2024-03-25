using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInformationManager : MonoBehaviour
{
    [SerializeField] Image characterImage;

    [SerializeField] Sprite[] characterMainImage;
    [SerializeField] Sprite[,] characterSkill;

    [SerializeField] Image[] characterSkillImage;
    [SerializeField] Text[] characterSkillName;  // Ä³¸¯ÅÍ µñ¼Å³Ê¸® Ãß°¡
    [SerializeField] Text[] characterSkillInformation;

    [SerializeField] Text[] point;

    [SerializeField] Text lv;

    [SerializeField] Text[] statLV;
    [SerializeField] Text[] stat;

    private int[] statPoint;

    private int characterNumber;
    public Vector3 target;

    private void Awake()
    {
        characterSkill = new Sprite[3, 3];
        target = Vector3.zero;
        statPoint = new int[2];
        int k = 0;
        for (int i = 0; i < DataManager.instance.characterMax; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                characterSkill[i, j] = SpriteManager.instance.SkillSprite(k++);
            }
        }
        characterNumber = GameManager.instance.charNum;
    }

    private void OnEnable()
    {
        statPoint[0] = DataManager.instance.subArray[characterNumber, 6];
        statPoint[1] = DataManager.instance.subArray[characterNumber, 10];

        point[0].text = statPoint[0].ToString();
        point[1].text = statPoint[1].ToString();

        characterImage.sprite = characterMainImage[characterNumber];

        lv.text = DataManager.instance.subArray[characterNumber, 1].ToString();

        for (int i = 0; i < stat.Length; i++)
        {
            stat[i].text = DataManager.instance.subArray[characterNumber, i + 2].ToString();
        }

        for (int i = 0; i < 3; i++)
        {
            characterSkillImage[i].sprite = characterSkill[characterNumber, i];
        }
        characterSkillName[0].text = DictionaryManager.instance.CharacterInfoOutput(characterNumber).Skill1Name;
        characterSkillName[1].text = DictionaryManager.instance.CharacterInfoOutput(characterNumber).Skill2Name;
        characterSkillName[2].text = DictionaryManager.instance.CharacterInfoOutput(characterNumber).Skill3Name;

        characterSkillInformation[0].text = DictionaryManager.instance.SkillInformationTextOutput(characterNumber).Skill1Information;
        characterSkillInformation[1].text = DictionaryManager.instance.SkillInformationTextOutput(characterNumber).Skill2Information;
        characterSkillInformation[2].text = DictionaryManager.instance.SkillInformationTextOutput(characterNumber).Skill3Information;
    }

    private void Update()
    {
        for (int i = 0; i < stat.Length; i++)
        {
            stat[i].text = DataManager.instance.subArray[characterNumber, i + 2].ToString();
        }
        if (target != Vector3.zero)
        {
            if (transform.localPosition.x > 1200 || transform.localPosition.x < -1200)
            {
                Destroy(transform.gameObject);
            }
            else
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, target, Time.deltaTime * 3);
            }
        }
        else
        {
            if(transform.localPosition.x >-1 && transform.localPosition.x < 1)
            {
                transform.localPosition = Vector3.zero;
            }
            transform.localPosition = Vector3.Lerp(transform.localPosition, target, Time.deltaTime * 3);
        }
    }

    private void OnDestroy()
    {
        transform.GetComponentInParent<InterfaceChanger>().flag = true;
    }

    private void StatUp()
    {
        
    }

    private void StatDown()
    {

    }

    private void SkillUp()
    {

    }

    private void SkillDown()
    {

    }
}
