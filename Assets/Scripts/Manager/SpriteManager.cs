using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteManager : Singleton<SpriteManager>
{
    [SerializeField] Sprite[] characterSprite;
    [SerializeField] Sprite[] itemSprite;
    [SerializeField] Sprite[] skillSprite;
    [SerializeField] Sprite[] characterAttackSprite;
    [SerializeField] Sprite[] shopSprite;

    public Sprite CharacterSprite(int num)
    {
        return characterSprite[num];
    }

    public Sprite ItemSprite(int num)
    {
        return itemSprite[num];
    }

    public Sprite CharacterAttack(int num)
    {
        return characterAttackSprite[num];
    }

    public Sprite SkillSprite(int num)
    {
        return skillSprite[num];
    }
}
