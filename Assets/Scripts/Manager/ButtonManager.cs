using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : Singleton<ButtonManager>
{
    [SerializeField] public GameObject[] button;
    [SerializeField] public int buttonNumber;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        InputManager.instance.keyAction += ButtonMove;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            button = new GameObject[6];

            for(int i = 0; i < 6; i++)
            {
                button[i] = GameObject.Find("Canvas").transform.GetChild(4).GetChild(i).gameObject;
            }
        }

        if(SceneManager.GetActiveScene().name == "Character Select")
        {
            button = new GameObject[5];

            for (int i = 0; i < 5; i++)
            {
                button[i] = GameObject.Find("Canvas").transform.GetChild(7).GetChild(i).gameObject;
            }
        }

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
            if(Input.GetKeyDown(KeyCode.Mouse0))
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
            else if(Input.GetKeyDown(KeyCode.Return)) // 수정 필요 (엔터 입력하면 다시 작동됨) - 버튼에 이벤트 함수로 등록
            {
                Debug.Log("Enter");
                button[3].GetComponent<Button>().Select();
            }
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        InputManager.instance.keyAction -= ButtonMove;
    }
}
