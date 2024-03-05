using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    private void Awake()
    {
        SkillUpDown();
    }

    private void SkillUpDown()
    {
        if (transform.name == "Up")
        {
            transform.GetComponent<Button>().onClick.AddListener(SkillUp);
        }
        else
        {
            transform.GetComponent<Button>().onClick.AddListener(SkillDown);
        }
    }

    private void SkillUp()
    {
        if (DataManager.instance.subArray[GameManager.instance.charNum, 10] > 0)
        {
            DataManager.instance.subArray[GameManager.instance.charNum, 10]--;
            DataManager.instance.subArray[GameManager.instance.charNum, int.Parse(transform.parent.name)]++;
        }
    }

    private void SkillDown()
    {
        if (DataManager.instance.subArray[GameManager.instance.charNum, int.Parse(transform.parent.name)] > 0)
        {
            DataManager.instance.subArray[GameManager.instance.charNum, 10]++;
        }
    }
}
