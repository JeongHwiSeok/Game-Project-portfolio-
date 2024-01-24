using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Character()
    {
        Debug.Log("Load Game");
    }

    public void Shop()
    {

    }

    public void Option()
    {
        Debug.Log("Option");
    }

    public void Credit()
    {

    }

    public void Quit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
