using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using static UI_SchedulePopup;

public class DataManager
    //정보 저장 및 가공 담당
{
    public PlayerData _myPlayerData;
    
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

    public void SaveData()
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
    public float GetNowWeekBonusMag()
    {
        int temp = _myPlayerData.StartWeek;
        int temp2 = (temp-1) / 4;
        return weekBounsMagnification[temp2];
    }

    List<OneDayScheduleData> oneDayDatasList = new List<OneDayScheduleData>();
   
    public OneDayScheduleData GetOneDayDataByName(RestType restType)
    {
        OneDayScheduleData temp = new OneDayScheduleData();
        foreach(OneDayScheduleData one in oneDayDatasList)
        {
            if (one.restType == restType) temp = one;
        }
        return temp;
    }

    public OneDayScheduleData GetOneDayDataByName(BroadCastType restType)
    {
        OneDayScheduleData temp = new OneDayScheduleData();
        foreach (OneDayScheduleData one in oneDayDatasList)
        {
            if (one.broadcastType == restType) temp = one;
        }
        return temp;
    }

    public OneDayScheduleData GetOneDayDataByName(GoOutType restType)
    {
        OneDayScheduleData temp = new OneDayScheduleData();
        foreach (OneDayScheduleData one in oneDayDatasList)
        {
            if (one.goOutType == restType) temp = one;
        }
        return temp;
    }

    public IEnumerator RequestAndSetDatas(string www)
    {
        UnityWebRequest wwww = UnityWebRequest.Get(www);
        yield return wwww.SendWebRequest();

        string data = wwww.downloadHandler.text;
        string[] lines = data.Substring(0, data.Length - 1).Split('\n');

        Queue<string> tempfloatList = new Queue<string>();

        foreach (string line in lines)
        {
            string templine = line.Substring(0, line.Length - 1);
            tempfloatList.Enqueue(templine);
        }

        for (int i = 0; i < 5; i++)
        {
            weekBounsMagnification[i] = float.Parse(tempfloatList.Dequeue());
        }

        for(int i = 0;i<(int)BroadCastType.MaxCount; i++)
        {
            OneDayScheduleData temp = new OneDayScheduleData();
            temp.scheduleType = ScheduleType.BroadCast;
            temp.broadcastType = (BroadCastType)i;
            temp.KorName = tempfloatList.Dequeue();
            temp.infotext = tempfloatList.Dequeue();
            temp.FisSubsUpValue = float.Parse(tempfloatList.Dequeue());
            temp.PerSubsUpValue = float.Parse(tempfloatList.Dequeue());
            temp.HealthPointChangeValue = float.Parse(tempfloatList.Dequeue());
            temp.MentalPointChageValue = float.Parse(tempfloatList.Dequeue());
            temp.InComeMag = float.Parse(tempfloatList.Dequeue());
            temp.MoneyCost = int.Parse(tempfloatList.Dequeue());
            for(int j = 0;j<6; j++)
            {
                temp.Six_Stats[j] = int.Parse(tempfloatList.Dequeue());
            }
            oneDayDatasList.Add(temp);
        }
        for (int i = 0; i <  (int)RestType.MaxCount; i++)
        {
            OneDayScheduleData temp = new OneDayScheduleData();
            temp.scheduleType = ScheduleType.Rest;
            temp.restType = (RestType)i;
            temp.KorName = tempfloatList.Dequeue();
            temp.infotext = tempfloatList.Dequeue();
            temp.FisSubsUpValue = float.Parse(tempfloatList.Dequeue());
            temp.PerSubsUpValue = float.Parse(tempfloatList.Dequeue());
            temp.HealthPointChangeValue = float.Parse(tempfloatList.Dequeue());
            temp.MentalPointChageValue = float.Parse(tempfloatList.Dequeue());
            temp.InComeMag = float.Parse(tempfloatList.Dequeue());
            temp.MoneyCost = int.Parse(tempfloatList.Dequeue());
            for (int j = 0; j < 6; j++)
            {
                temp.Six_Stats[j] = int.Parse(tempfloatList.Dequeue());
            }
            oneDayDatasList.Add(temp);
        }
        for (int i = 0; i < (int)GoOutType.MaxCount; i++)
        {
            OneDayScheduleData temp = new OneDayScheduleData();
            temp.scheduleType = ScheduleType.GoOut;
            temp.goOutType = (GoOutType)i;
            temp.KorName = tempfloatList.Dequeue();
            temp.infotext = tempfloatList.Dequeue();
            temp.FisSubsUpValue = float.Parse(tempfloatList.Dequeue());
            temp.PerSubsUpValue = float.Parse(tempfloatList.Dequeue());
            temp.HealthPointChangeValue = float.Parse(tempfloatList.Dequeue());
            temp.MentalPointChageValue = float.Parse(tempfloatList.Dequeue());
            temp.InComeMag = float.Parse(tempfloatList.Dequeue());
            temp.MoneyCost = int.Parse(tempfloatList.Dequeue());
            for (int j = 0; j < 6; j++)
            {
                temp.Six_Stats[j] = int.Parse(tempfloatList.Dequeue());
            }
            oneDayDatasList.Add(temp);
        }

        Debug.Log("시작 가능");
    }
}

[System.Serializable]
public class PlayerData
{
    public int StartWeek;
    public int nowSubCount;
    public int nowGoldAmount;
    public int nowHealthStatus;
    public int nowMentalStatus;
    public int[] SixStat;

    public PlayerData()
    {
        StartWeek = 1;
        nowSubCount = 0;
        nowGoldAmount = 0;
        nowHealthStatus = 100;
        nowMentalStatus = 100;
        SixStat = new int[6];
    }

    public int GetHighestStat()
    {
        int temp = 0;
        for(int i = 0;i<6;i++)
        {
            if(temp < SixStat[i])
            {
                temp = SixStat[i];
            }
        }

        return temp;
    }

    public Define.StatName GetHigestStatName()
    {
        Define.StatName temp = Define.StatName.Game;
        int temp2 = 0;

        for(int i = 0;i<6;i++)
        {
            if(temp2 < SixStat[i])
            {
                temp2 = SixStat[i];
                temp = (Define.StatName)i;
            }
        }
        return temp;
    }

}


