using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainBackUI : UI_Scene
{
    public GameObject SchedulePopup;

    enum Texts
    {
        HealthTMP,  //현재 건강 상태
        MentalTMP,  //현재 정신 상태
        MyMoneyTMP, //현재 보유 골드
        MySubsTMP,  // 현재 보유 구독자수
    }

    enum Buttons
    {
        CreateScheduleBTN
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

        CreateScheduleBTN.onClick.AddListener(ShowCreateSchedulePopup);

        InitTextComponents();
    }

    private void InitTextComponents()
        //모든 Text받아와서 글자 설정하기
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
                return Managers.Data.GetConditionByString(true);
            case Texts.MentalTMP:
                return Managers.Data.GetConditionByString(false);
            case Texts.MyMoneyTMP:
                return Managers.Data._myPlayerData.nowGoldAmount.ToString();
            case Texts.MySubsTMP:
                return Managers.Data._myPlayerData.nowSubCount.ToString();
            default:
                return "";
        }
    }

    void ShowCreateSchedulePopup()
    {
        SchedulePopup.SetActive(true);
    }
}
