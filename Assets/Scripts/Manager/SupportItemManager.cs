using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportItemManager : MonoBehaviour
{
    [SerializeField] List<GameObject> supportItemList;

    public void AddList(GameObject obj)
    {
        GameObject supportItem = Instantiate(obj, transform);

        supportItemList.Add(supportItem);
    }

    public GameObject ResearchList(int num)
    {
        return supportItemList[num];
    }

    public int ListCount()
    {
        return supportItemList.Count;
    }
}
