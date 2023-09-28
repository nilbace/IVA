using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_WeekEvent : UI_Popup
{
    public static UI_WeekEvent instance;

    enum Buttons
    {
        

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
        //base.Init();

        //Bind<GameObject>(typeof(GameObjects));
        //Bind<Button>(typeof(Buttons));

        //GetGameObject((int)GameObjects.SubContents4).SetActive(false);

        //for (int i = 0; i < 7; i++)
        ////7일들
        //{
        //    int inttemp = i;
        //    Button temp = GetButton(i);
        //    temp.onClick.AddListener(() => ClickDay(inttemp));
        //}

        //GetButton((int)Buttons.BroadCastBTN).onClick.AddListener(BroadCastBTN);
        //GetButton((int)Buttons.RestBTN).onClick.AddListener(RestBTN);
        //GetButton((int)Buttons.GoOutBTN).onClick.AddListener(GoOutBTN);

        //GetButton((int)Buttons.LeftPageBTN).onClick.AddListener(GoLeftPage);
        //GetButton((int)Buttons.RightPageBTN).onClick.AddListener(GoRightPage);

        //ClickDay(0); //기본 월요일 선택
    }



}
