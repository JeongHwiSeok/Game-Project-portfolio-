using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceChanger : MonoBehaviour
{
    [SerializeField] GameObject characterInterface;
    [SerializeField] Transform parent;
    public bool flag = true;

    public void LeftChange()
    {
        if (GameManager.instance.charNum == DataManager.instance.characterMax - 1 && flag)
        {
            flag = false;
            GameManager.instance.charNum = 0;
            GameObject second = Instantiate(characterInterface, parent);
            second.transform.localPosition = new Vector3(1500, 0, 0);
            second.GetComponent<CharacterInformationManager>().target = Vector3.zero;
            transform.GetComponentInChildren<CharacterInformationManager>().target = new Vector3(-1500, 0, 0);
        }
        else if (flag)
        {
            flag = false;
            GameManager.instance.charNum++;
            GameObject second = Instantiate(characterInterface, parent);
            second.transform.localPosition = new Vector3(1500, 0, 0);
            second.GetComponent<CharacterInformationManager>().target = Vector3.zero;
            transform.GetComponentInChildren<CharacterInformationManager>().target = new Vector3(-1500, 0, 0);
        }
    }
    public void RightChange()
    {
        if (GameManager.instance.charNum == 0 && flag)
        {
            flag = false;
            GameManager.instance.charNum = 2;
            GameObject second = Instantiate(characterInterface, parent);
            second.transform.localPosition = new Vector3(-1500, 0, 0);
            second.GetComponent<CharacterInformationManager>().target = Vector3.zero;
            transform.GetComponentInChildren<CharacterInformationManager>().target = new Vector3(1500, 0, 0);
        }
        else if (flag)
        {
            flag = false;
            GameManager.instance.charNum--;
            GameObject second = Instantiate(characterInterface, parent);
            second.transform.localPosition = new Vector3(-1500, 0, 0);
            second.GetComponent<CharacterInformationManager>().target = Vector3.zero;
            transform.GetComponentInChildren<CharacterInformationManager>().target = new Vector3(1500, 0, 0);
        }
    }
}
