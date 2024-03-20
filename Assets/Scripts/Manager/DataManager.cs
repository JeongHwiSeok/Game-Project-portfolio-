using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public enum CharacterNumber
{
    Aoi,
    Iku,
    Meno
}

public class DataManager : Singleton<DataManager>
{
    public UserData data = new UserData();

    public CharacterNumber character;
    public int[,] subArray = new int[40, 12];

    public int characterMax = 3;

    private void Start()
    {
        try
        {
            Load();
        }
        catch (Exception)
        {
            FirstSave();
        }
    }

    public void FirstSave()
    {
        Aoi();
        Iku();
        Meno();
        
        data.canvasScalerSize[0] = 1280;
        data.canvasScalerSize[1] = 720;

        for (int i = 0; i < 3; i++)
        {
            data.volume[i] = 1;
        }

        string json = JsonUtility.ToJson(data);

        //byte[] bytes = System.Text.Encoding.UTF8.GetBytes(json);

        //string code = System.Convert.ToBase64String(bytes);

        File.WriteAllText(Application.persistentDataPath + "/GameData.json", json);
    }

    public void Save()
    {
        CharacterStatInput();

        string json = JsonUtility.ToJson(data);

        //byte[] bytes = System.Text.Encoding.UTF8.GetBytes(json);

        //string code = System.Convert.ToBase64String(bytes);

        File.WriteAllText(Application.persistentDataPath + "/GameData.json", json);
    }

    public void Load()
    {
        string jsonData = File.ReadAllText(Application.persistentDataPath + "/GameData.json");

        //byte[] bytes = System.Convert.FromBase64String(jsonData);

        //string code = System.Text.Encoding.UTF8.GetString(bytes);

        data = JsonUtility.FromJson<UserData>(jsonData);

        CharacterStatOutput();

        GameManager.instance.canvasScaler = new Vector2(data.canvasScalerSize[0], data.canvasScalerSize[1]);
        GameObject.Find("Canvas").GetComponent<CanvasScaler>().referenceResolution = GameManager.instance.canvasScaler;

        for (int i = 0; i < 3; i++)
        {
            GameManager.instance.volume[i] = data.volume[i];
        }
    }

    #region ÃÊ±âÈ­
    private void Aoi()
    {
        data.AoiInformation[0] = 1;
        data.AoiInformation[1] = 1;

        for (int j = 2; j <= 11; j++)
        {
            data.AoiInformation[j] = 0;
        }
    }

    private void Iku()
    {
        data.IkuInformation[0] = 1;
        data.IkuInformation[1] = 1;

        for (int j = 2; j <= 11; j++)
        {
            data.IkuInformation[j] = 0;
        }
    }

    private void Meno()
    {
        data.MenoInformation[0] = 1;
        data.MenoInformation[1] = 1;

        for (int j = 2; j <= 11; j++)
        {
            data.MenoInformation[j] = 0;
        }
    }

    #endregion

    private void CharacterStatInput()
    {
        for (int i = 1; i < 12; i++)
        {
            data.AoiInformation[i] = subArray[0, i];
            data.IkuInformation[i] = subArray[1, i];
            data.MenoInformation[1] = subArray[2, 1];
        }
    }

    private void CharacterStatOutput()
    {
        for (int i = 1; i < 12; i++)
        {
            subArray[0, i] = data.AoiInformation[i];
            subArray[1, i] = data.IkuInformation[i];
            subArray[2, i] = data.MenoInformation[i];
        }
    }
}
