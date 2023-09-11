using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_SchedulePopup : UI_Popup
{
    enum Buttons
    {
        MondayBTN,
        TuesdayBTN,
        WednesdayBTN,
        ThursdayBTN,
        FridayBTN,
        SaturdayBTN,
        SundayBTN,

        BroadCastBTN,
        RestBTN,
        GoOutBTN
    }
    enum Texts
    {
        PointText,
        ScoreText,
    }

    enum GameObjects
    {
        Days7,
        Contents3,
        SubContents4
    }

    enum Images
    {
        ItemIcon,
    }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));
        Bind<Button>(typeof(Buttons));

        for(int i = 0; i<7;i++)
            //7일들
        {
            int inttemp = i;
            Button temp = GetButton(i);
            temp.onClick.AddListener( () => ClickDay(inttemp));
        }

        GetButton((int)Buttons.BroadCastBTN).onClick.AddListener(TempBroadCastBTN);
        GetButton((int)Buttons.RestBTN).onClick.AddListener(TempRestBTN);
        GetButton((int)Buttons.GoOutBTN).onClick.AddListener(TempGoOutBTN);

        ClickDay(0); //기본 월요일 선택
    }

    #region ScheduleCheck
    public enum SevenDays {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday,
    }

    public enum ScheduleType
    {
        Null, BroadCast, Rest, GoOut
    }

    SevenDays _nowSelectedDay = SevenDays.Monday;
    ScheduleType[] _SevenDayScheduleState = new ScheduleType[7];
    
    void ClickDay(int i)
    {
        _nowSelectedDay = (SevenDays)i;
        RenewalDayBTNColor();
    }

    public Color Orange;

    void RenewalDayBTNColor()
        //색상 지정용 코드
    {
        for(int i = 0; i<7;i++)
        {
            if(i == (int)_nowSelectedDay)
            {
                GetButton(i).GetComponent<Image>().color = Color.red;
            }
            else if(_SevenDayScheduleState[i] == ScheduleType.Null)
            {
                GetButton(i).GetComponent<Image>().color = Color.white;
            }
            else if(_SevenDayScheduleState[i] == ScheduleType.BroadCast)
            {
                GetButton(i).GetComponent<Image>().color = Color.blue;
            }
            else if(_SevenDayScheduleState[i] == ScheduleType.Rest)
            {
                GetButton(i).GetComponent<Image>().color = Color.green;
            }
            else
            {
                GetButton(i).GetComponent<Image>().color = Orange;
            }
        }
    }

    #region BroadCast
    void TempBroadCastBTN()
    {
        GetGameObject((int)GameObjects.Contents3).SetActive(false);
        GetGameObject((int)GameObjects.SubContents4).SetActive(true);

        _SevenDayScheduleState[(int)_nowSelectedDay] = ScheduleType.BroadCast;
        ChangeNowSelectDayToNearestAndCheckFull();
        RenewalDayBTNColor();
    }

    public enum BroadCastList
    {
        Game, Sing, Chat, Horror, Cook, GameChallenge, NewClothe, 
    }


    #endregion

    void TempRestBTN()
    {
        _SevenDayScheduleState[(int)_nowSelectedDay] = ScheduleType.Rest;
        ChangeNowSelectDayToNearestAndCheckFull();
        RenewalDayBTNColor();
    }

    void TempGoOutBTN()
    {
        _SevenDayScheduleState[(int)_nowSelectedDay] = ScheduleType.GoOut;
        ChangeNowSelectDayToNearestAndCheckFull();
        RenewalDayBTNColor();
    }

    void ChangeNowSelectDayToNearestAndCheckFull()
    {
        for(int i = 0;i<7;i++)
        {
            if(_SevenDayScheduleState[i] == ScheduleType.Null)
            {
                _nowSelectedDay = (SevenDays)i;
                break;
            }
            if(i==6)
            {
                //선택 종료시 작동할 코드
            }
        }
    }

    #endregion
}
