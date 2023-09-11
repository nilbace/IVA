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
        PointButton,
    }
    enum Texts
    {
        PointText,
        ScoreText,
    }

    enum GameObjects
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday,
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

        for (int i = 0; i < 7; i++)
        {
            SetDayText(i);
        }

    }

    private void SetDayText(int dayIndex)
    {
        string dayName = Enum.GetName(typeof(GameObjects), (GameObjects)dayIndex);
        TMP_Text dateText = GetGameObject(dayIndex).transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        dateText.text = dayName;
    }
}
