using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSkinManager : MonoBehaviour
{
    [SerializeField] AnimationClip[] characterAnimation;

    public void CharacterSkin()
    {
        gameObject.GetComponent<Animator>().Play(characterAnimation[GameManager.instance.charNum].name);
    }
}
