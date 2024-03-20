using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePanelManager : MonoBehaviour
{
    [SerializeField] GameObject reconfirmUI;
    [SerializeField] GameObject optionUI;

    [SerializeField] Button[] pausePanelButton;

    private void Awake()
    {
        pausePanelButton[0].onClick.AddListener(Resume);
        pausePanelButton[1].onClick.AddListener(Option);
        pausePanelButton[3].onClick.AddListener(MainMenu);
    }

    private void Resume()
    {
        GameManager.instance.state = true;
        gameObject.SetActive(false);
    }

    private void Option()
    {
        Instantiate(optionUI);
    }

    private void MainMenu()
    {
        Instantiate(reconfirmUI);
    }
}
