using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
    //정보 저장 및 가공 담당
{
    #region DataManage

    #endregion

    #region AboutStreaming
    public int StartWeek;
    public int nowSubCount;
    public int nowGoldAmount;

    #endregion

    #region EllaStats
    public int GamimgStat;
    public int SingingStat;
    public int ChattingStat;
    public int HealthyStat;
    public int MentalStat;
    public int LuckStat;

    #endregion
    public void SaveAllStat()
    {
        PlayerPrefs.SetInt("StartWeek", StartWeek);
        PlayerPrefs.SetInt("nowSubCount", nowSubCount);
        PlayerPrefs.SetInt("nowGoldAmount", nowGoldAmount);
        PlayerPrefs.SetInt("GamimgStat", GamimgStat);
        PlayerPrefs.SetInt("SingingStat", SingingStat);
        PlayerPrefs.SetInt("ChattingStat", ChattingStat);
        PlayerPrefs.SetInt("HealthyStat", HealthyStat);
        PlayerPrefs.SetInt("MentalStat", MentalStat);
        PlayerPrefs.SetInt("LuckStat", LuckStat);
    }

    public void LoadAllStat()
    {
        StartWeek = PlayerPrefs.GetInt("StartWeek", StartWeek);
        nowSubCount = PlayerPrefs.GetInt("nowSubCount", nowSubCount);
        nowGoldAmount = PlayerPrefs.GetInt("nowGoldAmount", nowGoldAmount);
        GamimgStat = PlayerPrefs.GetInt("GamimgStat", GamimgStat);
        SingingStat = PlayerPrefs.GetInt("SingingStat", SingingStat);
        ChattingStat = PlayerPrefs.GetInt("ChattingStat", ChattingStat);
        HealthyStat = PlayerPrefs.GetInt("HealthyStat", HealthyStat);
        MentalStat = PlayerPrefs.GetInt("MentalStat", MentalStat);
        LuckStat = PlayerPrefs.GetInt("LuckStat", LuckStat);
    }

}
