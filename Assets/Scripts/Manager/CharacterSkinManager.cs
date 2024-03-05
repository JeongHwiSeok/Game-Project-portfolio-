using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSkinManager : MonoBehaviour
{
    [SerializeField] Sprite[] character;
    [SerializeField] RuntimeAnimatorController[] characterController;

    public void CharacterSkin()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = character[GameManager.instance.charNum];
        gameObject.GetComponent<Animator>().runtimeAnimatorController = characterController[GameManager.instance.charNum];
    }
}
