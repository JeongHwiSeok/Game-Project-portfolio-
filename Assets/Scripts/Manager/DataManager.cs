using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : Singleton<DataManager>
{
    public UserData data = new UserData();

    public int Lv;
    public int hpLv;
    public int atkLv;
    public int spdLv;
    public int criLv;
    public int Stpt;
    public int skill1;
    public int skill2;
    public int skill3;
    public int skillSt;
    public int Exp;
    public int coin;

    private void Start()
    {
        if(data.firstCheck == false)
        {
            Save();
        }
        else
        {
            Load();
        }    
    }

    public void Save()
    {
        if (data.firstCheck == false)
        {
            for (int i = 0; i < 3; i++)
            {
                data.characterInfo[i, 0] = 1;

                for (int j = 1; j <= 5; j++)
                {
                    data.characterInfo[i, j] = 1;
                }

                data.characterInfo[i, 6] = 0;

                for (int j = 7; j <= 9; j++)
                {
                    data.characterInfo[i, j] = 1;
                }

                data.characterInfo[i, 10] = 0;
                data.characterInfo[i, 11] = 0;
            }
            data.shopCoin = 0;

            data.firstCheck = true;
        }

        string json = JsonUtility.ToJson(data);

        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(json);

        string code = System.Convert.ToBase64String(bytes);

        File.WriteAllText(Application.persistentDataPath + "/GameData.json", code);
    }

    public void Load()
    {
        string jsonData = File.ReadAllText(Application.persistentDataPath + "/GameData.json");

        byte[] bytes = System.Convert.FromBase64String(jsonData);

        string code = System.Text.Encoding.UTF8.GetString(bytes);

        data = JsonUtility.FromJson<UserData>(code);
    }
}
