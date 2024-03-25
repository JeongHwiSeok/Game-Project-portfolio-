using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] Text[] resultText;
    [SerializeField] Image characterImage;

    [SerializeField] Slider expSlider;

    [SerializeField] Text lvUP;

    [SerializeField] Button nextButton;

    [SerializeField] int count;

    [SerializeField] int needExp;
    [SerializeField] int exp;

    private void Awake()
    {
        resultText[0].text = "LV : " + UIManager.instance.PlayerLv.ToString();
        resultText[1].text = GameManager.instance.monsterCount.ToString();
        resultText[2].text = UIManager.instance.DropCoin.ToString();
        if (GameManager.instance.charNum == 1)
        {
            resultText[3].text = "Return Ikumin : " + GameManager.instance.ikuminCount.ToString();
        }
        else
        {
            resultText[3].text = "";
        }
        exp = GameManager.instance.monsterCount / 100;

        nextButton.onClick.AddListener(ButtonManager.instance.CharacterSelectBack);
    }

    private void Update()
    {
        Exp();
        expSlider.value = exp / needExp;
        if (exp >= needExp)
        {
            exp -= needExp;
            DataManager.instance.subArray[GameManager.instance.charNum, 1]++;
            count++;
        }
        lvUP.text = count.ToString() + " LV UP!!";
    }

    private void Exp()
    {
        if (DataManager.instance.subArray[GameManager.instance.charNum, 1] < 16)
        {
            needExp = 20 * DataManager.instance.subArray[GameManager.instance.charNum, 1] + 7;
        }
        else if (DataManager.instance.subArray[GameManager.instance.charNum, 1] < 31)
        {
            needExp = 50 * DataManager.instance.subArray[GameManager.instance.charNum, 1] - 38;
        }
        else
        {
            needExp = 90 * DataManager.instance.subArray[GameManager.instance.charNum, 1] - 158;
        }
    }
}
