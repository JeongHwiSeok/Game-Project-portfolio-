using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceFood : MonoBehaviour
{
    [SerializeField] CoolTime coolTime;

    [SerializeField] GameObject spaceFood;
    [SerializeField] GameObject spaceFoodCT;

    [SerializeField] public bool flag;
    [SerializeField] Transform parent;

    [SerializeField] public int itemLV;
    private void Awake()
    {
        flag = false;
        coolTime.itemNum = 13;
        coolTime.CountText("0");
        spaceFoodCT = Instantiate(coolTime.gameObject, GameObject.Find("Canvas").transform.GetChild(5).transform);
        spaceFoodCT.GetComponent<CoolTime>().CountActivate();
        parent = GameObject.Find("Map").transform.GetChild(11).transform;
        StartCoroutine(SummonSpaceFood());
        itemLV = 1;
    }

    public void Activate()
    {
        if (flag)
        {
            switch (itemLV)
            {
                case 1:
                    Debug.Log("healing");
                    PlayerManager.instance.Hp += (int)(DictionaryManager.instance.CharacterInfoOutput(GameManager.instance.charNum).Hp * 0.2f);
                    break;
                case 2:
                    Debug.Log("healing");
                    PlayerManager.instance.Hp += (int)(DictionaryManager.instance.CharacterInfoOutput(GameManager.instance.charNum).Hp * 0.3f);
                    break;
                case 3:
                    Debug.Log("healing");
                    PlayerManager.instance.Hp += (int)(DictionaryManager.instance.CharacterInfoOutput(GameManager.instance.charNum).Hp * 0.5f);
                    break;
            }
            flag = false;
            spaceFoodCT.GetComponent<CoolTime>().CountText("0");
        }
    }

    public void PickUP()
    {
        flag = true;
        spaceFoodCT.GetComponent<CoolTime>().CountText("1");
    }

    private IEnumerator SummonSpaceFood()
    {
        while (true)
        {
            GameObject spacefood = Instantiate(spaceFood, parent);

            float x = Random.insideUnitSphere.x * 10;
            float y = Random.insideUnitSphere.y * 6;

            spacefood.transform.localPosition = new Vector3(x, y + 10, 0);

            spacefood.GetComponent<DropSpaceFood>().Target = new Vector3(x, y, 0);

            spacefood.GetComponent<DropSpaceFood>().GameObjectInput(gameObject);

            spacefood.SetActive(true);

            yield return new WaitForSeconds(10f);
        }
    }
}
