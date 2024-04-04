using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SongInformationPanel : MonoBehaviour
{
    [SerializeField] public GameObject safeSelect;

    [SerializeField] GameObject prism;

    [SerializeField] Button backButton;
    [SerializeField] Button okButton;

    private void Awake()
    {
        backButton.onClick.AddListener(BackButton);
        okButton.onClick.AddListener(OkButton);
    }

    private void BackButton()
    {
        Destroy(gameObject);
    }

    private void OkButton()
    {
        Instantiate(prism);
        GameManager.instance.ReLoadScene();
        Destroy(safeSelect);
        Destroy(gameObject);
    }
}
