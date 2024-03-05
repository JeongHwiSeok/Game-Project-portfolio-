using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    }

    #region √ ±‚»≠
    private void Aoi()
    {
        data.AoiInfo[0] = 1;
        data.AoiInfo[1] = 1;

        for (int j = 2; j <= 11; j++)
        {
            data.AoiInfo[j] = 0;
        }
    }

    private void Iku()
    {
        data.IkuInfo[0] = 1;
        data.IkuInfo[1] = 1;

        for (int j = 2; j <= 11; j++)
        {
            data.IkuInfo[j] = 0;
        }
    }

    private void Meno()
    {
        data.MenoInfo[0] = 1;
        data.MenoInfo[1] = 1;

        for (int j = 2; j <= 11; j++)
        {
            data.MenoInfo[j] = 0;
        }
    }

    #endregion

    private void CharacterStatInput()
    {
        for (int i = 1; i < 12; i++)
        {
            data.AoiInfo[i] = subArray[0, i];
            data.IkuInfo[i] = subArray[1, i];
            data.MenoInfo[1] = subArray[2, 1];
        }
    }

    private void CharacterStatOutput()
    {
        for (int i = 1; i < 12; i++)
        {
            subArray[0, i] = data.AoiInfo[i];
            subArray[1, i] = data.IkuInfo[i];
            subArray[2, i] = data.MenoInfo[i];
        }
    }
}
