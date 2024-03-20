using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] Text[] resultText;
    [SerializeField] Image characterImage;

    [SerializeField] Slider expSlider;

    [SerializeField] Button nextButton;

    private void Awake()
    {
        resultText[3].transform.parent.gameObject.SetActive(false);

        resultText[0].text = "LV : " + UIManager.instance.PlayerLv.ToString();
        //resultText[1].text = 킬 카운트
        resultText[2].text = UIManager.instance.DropCoin.ToString();
        if (GameManager.instance.charNum == 1)
        {
            resultText[3].transform.parent.gameObject.SetActive(true);
            // resultText[3].text = 회수된 이쿠민
        }

        DataManager.instance.Save();

        nextButton.onClick.AddListener(ButtonManager.instance.CharacterSelectBack);
    }

    private void Update()
    {
        
    }
}
