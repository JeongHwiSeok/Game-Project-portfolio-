using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SafetyOnOffManager : MonoBehaviour
{
    [SerializeField] GameObject prism;
    [SerializeField] GameObject safe;

    [SerializeField] Button prismButton;
    [SerializeField] Button safeButton;

    private void OnEnable()
    {
        prismButton.onClick.AddListener(PrismButtom);
        safeButton.onClick.AddListener(SafeButton);
    }

    private void PrismButtom()
    {
        prism.SetActive(true);
        Instantiate(prism);
        GameManager.instance.ReLoadScene();
        Destroy(gameObject);
    }

    private void SafeButton()
    {
        safe.SetActive(true);
        Instantiate(safe);
        GameManager.instance.ReLoadScene();
        Destroy(gameObject);
    }
}
