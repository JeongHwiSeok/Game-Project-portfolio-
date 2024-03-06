using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemManager : MonoBehaviour
{
    [SerializeField] List<GameObject> dropItemList;
    [SerializeField] GameObject[] dropItem;

    public static DropItemManager instance
    {
        get;
        private set;
    }
    private void Awake()
    {
        instance = this;
    }

    public void dropAdd(GameObject obj)
    {
        dropItemList.Add(obj);

        if (dropItemList.Count >= 2)
        {
            dropUnison(obj);
        }
    }

    public void removeDropItem(GameObject obj)
    {
        for (int i = 0; i < dropItemList.Count; i++)
        {
            if (obj == dropItemList[i])
            {
                dropItemList.Remove(dropItemList[i]);
            }
        }
    }

    private void dropUnison(GameObject obj)
    {
        GameObject obj2;
        for (int i = 0; i < dropItemList.Count - 1; i++)
        {
            if (Mathf.Abs(dropItemList[i].transform.localPosition.x - obj.transform.localPosition.x) < 0.2f && Mathf.Abs(dropItemList[i].transform.localPosition.y - obj.transform.localPosition.y) < 0.2f)
            {
                if (obj.name == dropItemList[i].name)
                {
                    obj2 = dropItemList[i];

                    dropItemList.Remove(dropItemList[i]);
                    dropItemList.Remove(obj);

                    int k = int.Parse(obj.name.Substring(4,1));

                    Vector3 dropPosition = (obj.transform.localPosition + obj2.transform.localPosition) / 2;

                    Destroy(obj);
                    Destroy(obj2);

                    GameObject newobj;

                    switch (k)
                    {
                        case 1:
                            newobj = Instantiate(dropItem[0], transform);
                            newobj.transform.localPosition = dropPosition;
                            dropAdd(newobj);
                            break;
                        case 2:
                            newobj = Instantiate(dropItem[1], transform);
                            newobj.transform.localPosition = dropPosition;
                            dropAdd(newobj);
                            break;
                        case 3:
                            newobj = Instantiate(dropItem[2], transform);
                            newobj.transform.localPosition = dropPosition;
                            dropAdd(newobj);
                            break;
                    }
                    break;
                }
            }
        }
    }
}
