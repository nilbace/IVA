using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using static UI_SchedulePopup;

public class DataManager
    //���� ���� �� ���� ���
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


    float[] weekBounsMagnification = new float[5];
    public class OneDayDatas
    {
        public string Name;
        public int FixSubIncrease;
        public int PerSubIncrease;
        public int HealthMinAmount;
        public int MentalMinAmount;
        public int IncomeMagnificant;

        public OneDayDatas(string name = "", int fixSubIncrease=0, int perSubIncrease=0, int healthMinAmount=0, int mentalMinAmount=0, int incomeMagnificant=0)
        {
            Name = name;
            FixSubIncrease = fixSubIncrease;
            PerSubIncrease = perSubIncrease;
            HealthMinAmount = healthMinAmount;
            MentalMinAmount = mentalMinAmount;
            IncomeMagnificant = incomeMagnificant;
        }
    }
    OneDayDatas[] oneDayDatasArray = new OneDayDatas[(int)BroadCastType.MaxCount];
    //���� ������ ����
    //�̸��� ���� �ε��� ã�� �Լ� �߰� ����

    public IEnumerator RequestAndSetDatas(string www)
    {
        UnityWebRequest wwww = UnityWebRequest.Get(www);
        yield return wwww.SendWebRequest();

        string data = wwww.downloadHandler.text;
        string[] lines = data.Substring(0, data.Length - 1).Split('\n');

        Queue<float> tempfloatList = new Queue<float>();

        foreach (string line in lines)
        {
            string templine = line.Substring(0, line.Length - 1);
            float temp = float.Parse(templine);
            tempfloatList.Enqueue(temp);
        }

        for (int i = 0; i < 5; i++)
        {
            weekBounsMagnification[i] = tempfloatList.Dequeue();
        }

        for(int i = 0;i<(int)BroadCastType.MaxCount; i++)
        {
            OneDayDatas temp = new OneDayDatas();
            temp.Name = Enum.GetName(typeof(BroadCastType), i);
            temp.FixSubIncrease = (int)tempfloatList.Dequeue();
            temp.PerSubIncrease = (int)tempfloatList.Dequeue();
            temp.HealthMinAmount = (int)tempfloatList.Dequeue();
            temp.MentalMinAmount = (int)tempfloatList.Dequeue();
            temp.IncomeMagnificant = (int)tempfloatList.Dequeue();
            oneDayDatasArray[i] = temp;
        }

    }
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


