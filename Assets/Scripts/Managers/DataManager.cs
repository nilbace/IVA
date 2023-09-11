using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class DataManager
    //정보 저장 및 가공 담당
{
    public PlayerData _myPlayerData;

    public string GetConditionByString(bool isHealth)
    {
        if(isHealth)
        {
            EllaCondition temp = _myPlayerData.HealthCondition;
            string temp2 = Enum.GetName(typeof(EllaCondition), temp);
            return temp2;
        }
        else
        {
            EllaCondition temp = _myPlayerData.MentalCondition;
            string temp2 = Enum.GetName(typeof(EllaCondition), temp);
            return temp2;
        }
    }
    
    public void Init()
    {
        LoadData();
    }

    #region Part_DataSave&Load

    void LoadData()
    {
        string path;
        if (Application.platform == RuntimePlatform.Android)
        {
            path = Path.Combine(Application.persistentDataPath, "PlayerData.json");
        }
        else
        {
            path = Path.Combine(Application.dataPath, "PlayerData.json");
        }

        if (!File.Exists(path))
        {
            _myPlayerData = new PlayerData();
            SaveData();
        }

        FileStream fileStream = new FileStream(path, FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string jsonData = Encoding.UTF8.GetString(data);

        _myPlayerData = JsonUtility.FromJson<PlayerData>(jsonData);
    }

    void SaveData()
    {
        string path;
        if (Application.platform == RuntimePlatform.Android)
        {
            path = Path.Combine(Application.persistentDataPath, "PlayerData.json");
        }
        else
        {
            path = Path.Combine(Application.dataPath, "PlayerData.json");
        }
        string jsonData = JsonUtility.ToJson(_myPlayerData, true);

        FileStream fileStream = new FileStream(path, FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }

    public void SaveToCloud()
    {
        SaveData();
    }

    public void LoadFromCloud()
    {

    }

    #endregion
}
public enum EllaCondition
{
    Healthy, Warning, Danger, Terrible
}
[System.Serializable]
public class PlayerData
{
    public int StartWeek;
    public int nowSubCount;
    public int nowGoldAmount;
    public EllaCondition HealthCondition;
    public EllaCondition MentalCondition;
    public int GamimgStat;
    public int SingingStat;
    public int ChattingStat;
    public int HealthyStat;
    public int MentalStat;
    public int LuckStat;

    public PlayerData()
    {
        StartWeek = 1;
        nowSubCount = 0;
        nowGoldAmount = 0;
        HealthCondition = EllaCondition.Healthy;
        MentalCondition = EllaCondition.Healthy;
        GamimgStat = 0;
        SingingStat = 0;
        HealthyStat = 0;
        MentalStat = 0;
        LuckStat = 0;
    }
}