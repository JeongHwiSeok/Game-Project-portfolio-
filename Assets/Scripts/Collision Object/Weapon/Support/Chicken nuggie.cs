using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chickennuggie : MonoBehaviour
{
    [SerializeField] int maxCount;
    [SerializeField] public int itemLV;

    public static Chickennuggie instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        itemLV = 1;
        instance = this;
    }

    private void Update()
    {
        if (GameManager.instance.cnCount >= maxCount)
        {  
            if ((int)(DictionaryManager.instance.CharacterInfoOutput(GameManager.instance.charNum).Hp - PlayerManager.instance.Hp) <= (int)(DictionaryManager.instance.CharacterInfoOutput(GameManager.instance.charNum).Hp * 0.01f))
            {
                PlayerManager.instance.Hp = (int)(DictionaryManager.instance.CharacterInfoOutput(GameManager.instance.charNum).Hp);
                GameManager.instance.cnCount = 0;
            }
            else
            {
                if (DictionaryManager.instance.CharacterInfoOutput(GameManager.instance.charNum).Hp * 0.01f < 1)
                {
                    PlayerManager.instance.Hp += 1;
                }
                else
                {
                    PlayerManager.instance.Hp += (int)(DictionaryManager.instance.CharacterInfoOutput(GameManager.instance.charNum).Hp * 0.01f);
                }
                GameManager.instance.cnCount = 0;
            }
        }
    }

    public void Activate()
    {
        switch (itemLV)
        {
            case 1:
                maxCount = 10;
                break;
            case 2:
                maxCount = 7;
                break;
            case 3:
                maxCount = 5;
                break;
        }
    }
}
