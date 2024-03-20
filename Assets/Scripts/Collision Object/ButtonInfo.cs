using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInfo : MonoBehaviour
{
    [SerializeField] Button characterButton;
    [SerializeField] CharacterStatManager characterStatManager;
    [SerializeField] CharacterSkinManager characterSkinManager;

    public void CharacterButton()
    {
        switch (transform.gameObject.name)
        {
            case "Tokimori Aoi":
                GameManager.instance.charNumber = CharacterNumber.Aoi;
                break;
            case "Hoshifuri Iku":
                GameManager.instance.charNumber = CharacterNumber.Iku;
                break;
            case "Ibuki Meno":
                GameManager.instance.charNumber = CharacterNumber.Meno;
                break;
        }
        GameManager.instance.CharacterNumberCheck();
        characterStatManager.CharacterStat();
        characterSkinManager.CharacterSkin();
    }
}
