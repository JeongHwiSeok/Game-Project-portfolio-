using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrangePicture : MonoBehaviour
{
    [SerializeField] public int itemLV;
    [SerializeField] float pow;
    [SerializeField] float normalPow;

    private void Awake()
    {
        itemLV = 1;
    }

    private void Update()
    { 
        pow = normalPow * PlayerManager.instance.Hp / (int)DictionaryManager.instance.CharacterInfoOutput(GameManager.instance.charNum).Hp;
        PlayerManager.instance.spAtk = (pow + 1);
    }

    public void Activate()
    {
        switch (itemLV)
        {
            case 1:
                normalPow = 0.1f;
                break;
            case 2:
                normalPow = 0.2f;
                break;
            case 3:
                normalPow = 0.3f;
                break;
        }
    }
}
