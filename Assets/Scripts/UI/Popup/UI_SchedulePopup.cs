using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_SchedulePopup : UI_Popup
    //스케쥴 관리와 방송 정보에 대한 정보가 담겨있는 스크립트
{
    public static UI_SchedulePopup instance; 

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
        GoOutBTN,

        LeftPageBTN,
        RightPageBTN,

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
        SubContents4,
        Sub0, Sub1, Sub2, Sub3, 
    }

    enum Images
    {
        ItemIcon,
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
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

        GetGameObject((int)GameObjects.SubContents4).SetActive(false);

        for (int i = 0; i<7; i++)
            //7일들
        {
            int inttemp = i;
            Button temp = GetButton(i);
            temp.onClick.AddListener( () => ClickDay(inttemp));
        }

        GetButton((int)Buttons.BroadCastBTN).onClick.AddListener(BroadCastBTN);
        GetButton((int)Buttons.RestBTN).onClick.AddListener(TempRestBTN);
        GetButton((int)Buttons.GoOutBTN).onClick.AddListener(TempGoOutBTN);

        GetButton((int)Buttons.LeftPageBTN).onClick.AddListener(GoLeftPage);
        GetButton((int)Buttons.RightPageBTN).onClick.AddListener(GoRightPage);

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
    ScheduleData[] _SevenDayScheduleDatas = new ScheduleData[7];
    
    void ClickDay(int i)
    {
        _nowSelectedDay = (SevenDays)i;
        RenewalDayBTNColor();
    }

    public Color Orange;

    void RenewalDayBTNColor()
        //색상 지정용 함수
    {
        for(int i = 0; i<7;i++)
        {
            if(i == (int)_nowSelectedDay)
            {
                GetButton(i).GetComponent<Image>().color = Color.red;
            }
            else if(_SevenDayScheduleDatas[i] == null)
            {
                GetButton(i).GetComponent<Image>().color = Color.white;
            }
            else if(_SevenDayScheduleDatas[i].scheduleType == ScheduleType.BroadCast)
            {
                GetButton(i).GetComponent<Image>().color = Color.blue;
            }
            else if(_SevenDayScheduleDatas[i].scheduleType == ScheduleType.Rest)
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
    //방송 정보에 대한 스크립트

    [System.Serializable]
    [CreateAssetMenu(fileName = "ScheduleData", menuName = "Scriptable/ScheduleData", order = int.MaxValue)]
    public class ScheduleData : ScriptableObject
    {
        public string KorName;
        public ScheduleType scheduleType;
        public BroadCastType broadcastType;
        public RestType restType;
        public GoOutType goOutType;
        public string infotext;
    }

    public enum BroadCastType
    {
        Game, Sing, Chat, Horror, Cook, GameChallenge, NewClothe, MaxCount
    }

    public enum RestType
    { 
        home, chicken, MaxCount
    }

    public enum GoOutType
    {
        songAcademy, GameAcademy, MaxCount
    }

    List<ScheduleData> nowSelectScheduleTypeList = new List<ScheduleData>();

    void BroadCastBTN()
    {
        GetGameObject((int)GameObjects.Contents3).SetActive(false);
        GetGameObject((int)GameObjects.SubContents4).SetActive(true);
        ChooseScheduleTypeAndFillList(ScheduleType.BroadCast);

    }
    void TempRestBTN()
    {
        ChangeNowSelectDayToNearestAndCheckFull();
        RenewalDayBTNColor();
    }

    void TempGoOutBTN()
    {
        ChangeNowSelectDayToNearestAndCheckFull();
        RenewalDayBTNColor();
    }

    void ChooseScheduleTypeAndFillList(ScheduleType type)
    {
        nowSelectScheduleTypeList.Clear();
        switch (type)
        {
            case ScheduleType.BroadCast:
                string path = "ScheduleDatas/BroadCast/";
                for (int i = 0; i < (int)BroadCastType.MaxCount; i++)
                {
                    string temppath = path + Enum.GetName(typeof(BroadCastType), (BroadCastType)i);
                    ScheduleData tempdata = Managers.Resource.Load<ScheduleData>(temppath);
                    nowSelectScheduleTypeList.Add(tempdata);
                }
                
                break;

            case ScheduleType.Rest:

                break;

            case ScheduleType.GoOut:
                break;
        }
        _nowpage = 0;
        Renewal4SubContentsBTN();
    }

    int _nowpage = 0; int _MaxPage;


    void Renewal4SubContentsBTN()
    {
        _MaxPage = nowSelectScheduleTypeList.Count / 4;

        if(_nowpage == 0)
        {
            GetButton((int)Buttons.LeftPageBTN).interactable = false;
        }
        else
        {
            GetButton((int)Buttons.LeftPageBTN).interactable = true;
        }

        if(_nowpage == _MaxPage)
        {
            GetButton((int)Buttons.RightPageBTN).interactable = false;
        }
        else
        {
            GetButton((int)Buttons.RightPageBTN).interactable = true;
        }

        for (int i = 0;i<4;i++)
        {
            if (isIndexExist(i))
            {
                GetGameObject(3 + i).
                    GetOrAddComponent<UI_SubContent>().SetInfo(nowSelectScheduleTypeList[nowIndex(i)]);
                GetGameObject(3 + i).GetComponent<Button>().interactable = true;
            }
            else
                GetGameObject(3 + i).GetComponent<Button>().interactable = false;
        }

    }

    void GoLeftPage()
    {
        _nowpage--;
        Renewal4SubContentsBTN();
    }

    void GoRightPage()
    {
        _nowpage++;
        Renewal4SubContentsBTN();
    }

    bool isIndexExist(int i)
    {
        int temp = i + (4 * _nowpage);
        if (nowSelectScheduleTypeList.Count-1 >= temp)
            return true;
        else
            return false;
    }

    int nowIndex(int i)
    {
        return i + (4 * _nowpage);
    }

    public void SetDaySchedule(ScheduleData data)
    {
        _SevenDayScheduleDatas[(int)_nowSelectedDay] = data;
        ChangeNowSelectDayToNearestAndCheckFull();
    }
    #endregion

    void ChangeNowSelectDayToNearestAndCheckFull()
    {
        for(int i = 0;i<7;i++)
        {
            if(_SevenDayScheduleDatas[i] == null)
            {
                _nowSelectedDay = (SevenDays)i;
                break;
            }
            if(i==6)
            {
                StartCoroutine(StartSchedule());
            }
        }

        RenewalDayBTNColor();
        GetGameObject((int)GameObjects.Contents3).SetActive(true);
        GetGameObject((int)GameObjects.SubContents4).SetActive(false);
    }

    IEnumerator StartSchedule()
    {
        for (int i =0; i<7; i++)
        {
            Debug.Log("Action 실행");
            Debug.Log("스케쥴 종료");
            yield return new WaitForSeconds(3f);
        }
        Debug.Log("결산 팝업창");
        Debug.Log("랜덤 이벤트 발생");
        Debug.Log("컷씬 발생");
    }

    void CarryOutOneDayWork(BroadCastType broadCastType)
    {
        switch (broadCastType)
        {
            case BroadCastType.Game:
                break;
            case BroadCastType.Sing:
                break;
            case BroadCastType.Chat:
                break;
            case BroadCastType.Horror:
                break;
            case BroadCastType.Cook:
                break;
            case BroadCastType.GameChallenge:
                break;
            case BroadCastType.NewClothe:
                break;
            case BroadCastType.MaxCount:
                break;
            default:
                break;
        }

        #endregion



    }
}