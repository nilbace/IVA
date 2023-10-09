using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainBackUI : UI_Scene
{
    enum Texts
    {
        HealthTMP,  //현재 건강 상태
        MentalTMP,  //현재 정신 상태
        MyMoneyTMP, //현재 보유 골드
        MySubsTMP,  // 현재 보유 구독자수
        NowWeekTMP,
    }

    enum Buttons
    {
        CreateScheduleBTN
    }

    public static UI_MainBackUI instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Init();
    }

    private void Init()
    {
        base.Init();
        Bind<TMPro.TMP_Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));

        Button CreateScheduleBTN = Get<Button>((int)Buttons.CreateScheduleBTN);

        CreateScheduleBTN.onClick.AddListener(ShowOrCloseCreateSchedulePopup);

        UpdateUItexts();
    }

    /// <summary>
    /// 메인화면 텍스트들 갱신
    /// </summary>
    public void UpdateUItexts()
    {
        foreach (Texts textType in System.Enum.GetValues(typeof(Texts)))
        {
            TMPro.TMP_Text tmpText = Get<TMPro.TMP_Text>((int)textType);
            tmpText.text = GetInitialTextForType(textType);
        }
    }

    private string GetInitialTextForType(Texts textType)
    {
        switch (textType)
        {
            case Texts.HealthTMP:
                return GetNowConditionToString(Managers.Data._myPlayerData.nowHealthStatus);
            case Texts.MentalTMP:
                return GetNowConditionToString(Managers.Data._myPlayerData.nowMentalStatus);
            case Texts.MyMoneyTMP:
                return Managers.Data._myPlayerData.nowGoldAmount.ToString();
            case Texts.MySubsTMP:
                return Managers.Data._myPlayerData.nowSubCount.ToString();
            case Texts.NowWeekTMP:
                return "방송 " +Managers.Data._myPlayerData.NowWeek.ToString()+"주차";
            default:
                return "";
        }
    }

    string GetNowConditionToString(int n)
    {
        string temp = "";
        if (n >= 75)
        {
            temp = "건강";
        }
        else if (n >= 50)
        {
            temp = "주의";
        }
        else if (n >= 25)
        {
            temp = "위험";
        }
        else temp = "심각";

        return temp;
    }

    bool isPopupOpen = false;
    void ShowOrCloseCreateSchedulePopup()
    {
        TMP_Text CreateScheduleTMP = Get<Button>((int)Buttons.CreateScheduleBTN).GetComponentInChildren<TMP_Text>();
        if (isPopupOpen)
        {
            Managers.UI_Manager.ClosePopupUI();
            CreateScheduleTMP.text = "스케쥴 작성하기";
        }
        else
        {
            Managers.UI_Manager.ShowPopupUI<UI_SchedulePopup>();
            CreateScheduleTMP.text = "방으로 돌아가기";
        }        
        isPopupOpen = !isPopupOpen;
    }
}
