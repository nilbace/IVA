using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainBackUI : UI_Scene
{
    enum Texts
    {
        HealthTMP,  //���� �ǰ� ����
        MentalTMP,  //���� ���� ����
        MyMoneyTMP, //���� ���� ���
        MySubsTMP,  // ���� ���� �����ڼ�
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
    /// ����ȭ�� �ؽ�Ʈ�� ����
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
                return "��� " +Managers.Data._myPlayerData.NowWeek.ToString()+"����";
            default:
                return "";
        }
    }

    string GetNowConditionToString(int n)
    {
        string temp = "";
        if (n >= 75)
        {
            temp = "�ǰ�";
        }
        else if (n >= 50)
        {
            temp = "����";
        }
        else if (n >= 25)
        {
            temp = "����";
        }
        else temp = "�ɰ�";

        return temp;
    }

    bool isPopupOpen = false;
    void ShowOrCloseCreateSchedulePopup()
    {
        TMP_Text CreateScheduleTMP = Get<Button>((int)Buttons.CreateScheduleBTN).GetComponentInChildren<TMP_Text>();
        if (isPopupOpen)
        {
            Managers.UI_Manager.ClosePopupUI();
            CreateScheduleTMP.text = "������ �ۼ��ϱ�";
        }
        else
        {
            Managers.UI_Manager.ShowPopupUI<UI_SchedulePopup>();
            CreateScheduleTMP.text = "������ ���ư���";
        }        
        isPopupOpen = !isPopupOpen;
    }
}
