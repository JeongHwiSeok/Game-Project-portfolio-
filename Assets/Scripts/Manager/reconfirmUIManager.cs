using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class reconfirmUIManager : MonoBehaviour
{
    [SerializeField] Button[] button;

    private void Awake()
    {
        button[0].onClick.AddListener(Yes);
        button[1].onClick.AddListener(No);
    }

    private void Yes()
    {
        ButtonManager.instance.CharacterSelectBack();
    }

    private void No()
    {
        Destroy(gameObject);
    }
}
