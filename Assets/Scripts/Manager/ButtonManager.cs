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
            button = new GameObject[3];

            for (int i = 0; i < 3; i++)
            {
                button[i] = GameObject.Find("Canvas").transform.GetChild(8).GetChild(i).gameObject;
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
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        InputManager.instance.keyAction -= ButtonMove;
    }
}
