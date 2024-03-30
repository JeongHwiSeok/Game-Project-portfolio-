using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatButton : MonoBehaviour
{
    private void Awake()
    {
        StatUpDown();
    }

    private void StatUpDown()
    {
        if(transform.name == "Up")
        {
            transform.GetComponent<Button>().onClick.AddListener(StatUp);
        }
        else
        {
            transform.GetComponent<Button>().onClick.AddListener(StatDown);
        }
    }

    private void StatUp()
    {
        if (DataManager.instance.subArray[GameManager.instance.charNum, 6] > 0)
        {
            DataManager.instance.subArray[GameManager.instance.charNum, 6]--;
            DataManager.instance.subArray[GameManager.instance.charNum, int.Parse(transform.parent.name)]++;
            DataManager.instance.Save();
        }
    }

    private void StatDown()
    {
        if (DataManager.instance.subArray[GameManager.instance.charNum, int.Parse(transform.parent.name)] > 0)
        {
            DataManager.instance.subArray[GameManager.instance.charNum, 6]++;
            DataManager.instance.subArray[GameManager.instance.charNum, int.Parse(transform.parent.name)]--;
            DataManager.instance.Save();
        }
    }
}
