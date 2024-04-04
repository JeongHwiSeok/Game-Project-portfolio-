using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SafetyOnOffManager : MonoBehaviour
{
    [SerializeField] GameObject prism;
    [SerializeField] GameObject safe;

    [SerializeField] GameObject songInformation;

    [SerializeField] Button prismButton;
    [SerializeField] Button safeButton;

    private void OnEnable()
    {
        prismButton.onClick.AddListener(PrismButtom);
        safeButton.onClick.AddListener(SafeButton);
    }

    private void PrismButtom()
    {
        Instantiate(songInformation);
        songInformation.GetComponent<SongInformationPanel>().safeSelect = this.gameObject;
    }

    private void SafeButton()
    {
        Instantiate(safe);
        GameManager.instance.ReLoadScene();
        Destroy(gameObject);
    }
}
