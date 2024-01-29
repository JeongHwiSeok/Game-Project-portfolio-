using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : Singleton<ButtonManager>
{
    [SerializeField] public GameObject[] button;
    [SerializeField] public int buttonNumber;

    [SerializeField] public int mainSceneButtonCount;
    [SerializeField] public int characterSelectButtonCount;


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        InputManager.instance.keyAction += ButtonMove;
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
        }
        #endregion

        #region Character Select Scene
        if (SceneManager.GetActiveScene().name == "Character Select")
        {
            button = new GameObject[characterSelectButtonCount];

            for (int i = 0; i < characterSelectButtonCount; i++)
            {
                button[i] = GameObject.Find("Canvas").transform.GetChild(7).GetChild(i).gameObject;
            }

            button[0].GetComponent<Button>().onClick.AddListener(CharacterSelect);
            button[1].GetComponent<Button>().onClick.AddListener(CharacterSelect);
            button[2].GetComponent<Button>().onClick.AddListener(CharacterSelect);
            button[3].GetComponent<Button>().onClick.AddListener(Character);
            button[4].GetComponent<Button>().onClick.AddListener(Next);
        }
        #endregion

        buttonNumber = 0;
        button[buttonNumber].GetComponent<Button>().Select();
    }

    #region MainScene
    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Character()
    {
        
    }

    public void Shop()
    {

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

    #region
    public void CharacterSelect()
    {
        button[3].GetComponent<Button>().Select();
    }

    public void Next()
    {
        SceneManager.LoadScene("PlayScene");
    }
    #endregion

    private void ButtonMove()
    {
        if(SceneManager.GetActiveScene().name == "MainScene")
        {
            #region 버튼넘버 카운터 계산
            if (Input.GetKeyDown(KeyCode.RightArrow) && buttonNumber < 5)
            {
                buttonNumber++;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && buttonNumber > 0)
            {
                buttonNumber--;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) && buttonNumber > 1)
            {
                buttonNumber -= 2;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && buttonNumber < 4)
            {
                buttonNumber += 2;
            }
            #endregion
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                button[buttonNumber].GetComponent<Button>().Select();
            }
        }

        if (SceneManager.GetActiveScene().name == "Character Select")
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                button[buttonNumber].GetComponent<Button>().Select();
            }
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        InputManager.instance.keyAction -= ButtonMove;
    }
}
