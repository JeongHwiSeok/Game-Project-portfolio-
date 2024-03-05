using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonManager : Singleton<ButtonManager>
{
    [SerializeField] public GameObject[] button;
    [SerializeField] public int buttonNumber;

    [SerializeField] public int mainSceneButtonCount;
    [SerializeField] public int characterSelectSceneButtonCount;
    [SerializeField] public int shopSceneButtonCount;
    [SerializeField] public int characterSceneButtonCount;


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        //InputManager.instance.keyAction += ButtonMove;
    }

    private void Start()
    {
        mainSceneButtonCount = 6;
        characterSelectSceneButtonCount = 6;
        shopSceneButtonCount = 4;
        characterSceneButtonCount = 3;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        #region Main Scene
        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            button = new GameObject[mainSceneButtonCount];

            for(int i = 0; i < mainSceneButtonCount; i++)
            {
                button[i] = GameObject.Find("Canvas").transform.GetChild(4).GetChild(i).gameObject;
            }

            button[0].GetComponent<Button>().onClick.AddListener(Play);
            button[1].GetComponent<Button>().onClick.AddListener(Shop);
            button[2].GetComponent<Button>().onClick.AddListener(Option);
            button[3].GetComponent<Button>().onClick.AddListener(Character);
            button[4].GetComponent<Button>().onClick.AddListener(Credit);
            button[5].GetComponent<Button>().onClick.AddListener(Quit);

            button[0].GetComponent<Button>().Select();
        }
        #endregion

        #region Character Select Scene
        if (SceneManager.GetActiveScene().name == "Character Select")
        {
            button = new GameObject[characterSelectSceneButtonCount];

            for (int i = 0; i < characterSelectSceneButtonCount - 1; i++)
            {
                button[i] = GameObject.Find("Canvas").transform.GetChild(7).GetChild(i).gameObject;
            }
            button[characterSelectSceneButtonCount - 1] = GameObject.Find("Canvas").transform.GetChild(8).gameObject;

            //for (int i = 0; i < characterSelectButtonCount - 2; i++)
            //{
            //    button[i].GetComponent<Button>().onClick.AddListener(CharacterSelect);
            //}

            button[characterSelectSceneButtonCount - 3].GetComponent<Button>().onClick.AddListener(CharacterScene);
            button[characterSelectSceneButtonCount - 2].GetComponent<Button>().onClick.AddListener(Next);
            button[characterSelectSceneButtonCount - 1].GetComponent<Button>().onClick.AddListener(CharacterSelectBack);
        }
        #endregion

        #region Shop Scene
        if (SceneManager.GetActiveScene().name == "ShopScene")
        {
            button = new GameObject[shopSceneButtonCount];

            button[0] = GameObject.Find("Canvas").transform.GetChild(5).gameObject;
            button[1] = GameObject.Find("Canvas").transform.GetChild(4).GetChild(0).gameObject;
            button[2] = GameObject.Find("Canvas").transform.GetChild(4).GetChild(1).gameObject;
            button[3] = GameObject.Find("Canvas").transform.GetChild(4).GetChild(2).gameObject;

            button[0].GetComponent<Button>().onClick.AddListener(ShopBack);
            button[2].GetComponent<Button>().onClick.AddListener(ShopInterfaceOpen);
        }
        #endregion

        #region CharacterScene
        if (SceneManager.GetActiveScene().name == "CharacterScene")
        {
            button = new GameObject[characterSceneButtonCount];

            for (int i = 0; i < characterSceneButtonCount; i++)
            {
                button[i] = GameObject.Find("Canvas").transform.GetChild(i+4).gameObject;
            }

            button[0].GetComponent<Button>().onClick.AddListener(CharacterBack);
            button[1].GetComponent<Button>().onClick.AddListener(CharacterLeft);
            button[2].GetComponent<Button>().onClick.AddListener(CharacterRight);
        }
        #endregion

        buttonNumber = 0;
    }

    #region MainScene
    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Character()
    {
        SceneManager.LoadScene(3);
    }

    public void Shop()
    {
        SceneManager.LoadScene(4);
    }

    public void Option()
    {
        
    }

    public void Credit()
    {

    }

    public void Quit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
    #endregion

    #region CharacterSelectScene
    public void CharacterSelect()
    {
        button[characterSelectSceneButtonCount - 2].GetComponent<Button>().Select();
        buttonNumber = characterSelectSceneButtonCount - 2;
    }

    public void Next()
    {
        SceneManager.LoadScene("PlayScene");
    }

    public void CharacterScene()
    {
        SceneManager.LoadScene(3);
    }

    public void CharacterSelectBack()
    {
        SceneManager.LoadScene("MainScene");
    }
    #endregion

    #region ShopScene
    private void ShopBack()
    {
        if(GameObject.Find("Canvas").transform.GetChild(3).gameObject.activeSelf == true)
        {
            GameObject.Find("Canvas").transform.GetChild(3).gameObject.SetActive(false);
            GameObject.Find("Canvas").transform.GetChild(4).gameObject.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    private void ShopInterfaceOpen()
    {
        GameObject.Find("Canvas").transform.GetChild(4).gameObject.SetActive(false);
        GameObject.Find("Canvas").transform.GetChild(3).gameObject.SetActive(true);
    }
    #endregion

    #region CharacterScene
    private void CharacterBack()
    {
        SceneManager.LoadScene(0);
    }

    private void CharacterLeft()
    {
        GameObject.Find("Canvas").transform.GetChild(3).GetComponent<InterfaceChanger>().LeftChange();
    }

    private void CharacterRight()
    {
        GameObject.Find("Canvas").transform.GetChild(3).GetComponent<InterfaceChanger>().RightChange();
    }
    #endregion

    //private void ButtonMove()
    //{
    //    if(SceneManager.GetActiveScene().name == "MainScene")
    //    {

    //    }

    //    if (SceneManager.GetActiveScene().name == "Character Select")
    //    {
    //        if (buttonNumber >= characterSelectButtonCount - 2)
    //        {
    //            if (Input.GetKeyDown(KeyCode.Mouse0))
    //            {
    //                button[buttonNumber].GetComponent<Button>().Select();
    //            }
    //            else if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && buttonNumber < characterSelectButtonCount - 1)
    //            {
    //                buttonNumber++;
    //            }
    //            else if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && buttonNumber > characterSelectButtonCount - 2)
    //            {
    //                buttonNumber--;
    //            }
    //            else if (Input.GetKeyDown(KeyCode.Escape))
    //            {
    //                buttonNumber = 0;
    //                button[buttonNumber].GetComponent<Button>().Select();   
    //            }
    //        }
    //    }
    //}

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        //InputManager.instance.keyAction -= ButtonMove;
    }
}
