using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrangePicture : MonoBehaviour
{
    [SerializeField] public int itemLV;
    [SerializeField] float normalPow;

    private void Awake()
    {
        itemLV = 1;
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

    private IEnumerator AktUP()
    {
        while (true)
        {
            while (GameManager.instance.state)
            {
                BuffDebuffManager.instance.spAttackBuff = (normalPow * PlayerManager.instance.Hp / (int)DictionaryManager.instance.CharacterInfoOutput(GameManager.instance.charNum).Hp) + 1;

                yield return new WaitForSeconds(5f);
            }
            if (GameManager.instance.monsterSpawn == false)
            {
                yield break;
            }
            yield return null;
        }
    }
}
