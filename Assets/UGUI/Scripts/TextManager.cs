using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class ButtonText
{
    public string buttonName;
    public int buttonFontSize;
}

public class TextManager : MonoBehaviour
{
    public TMP_Text [] buttonTexts;
    public ButtonText[] buttonText;

    void Start()
    {
        for (int i = 0; i < buttonText.Length; i++)
        {
            buttonTexts[i].text = buttonText[i].buttonName;
            buttonTexts[i].fontSize = 30;
            buttonTexts[i].fontStyle = TMPro.FontStyles.Bold;
        }
    }
}
