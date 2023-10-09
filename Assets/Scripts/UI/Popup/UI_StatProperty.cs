using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class UI_StatProperty : UI_Popup
{
    enum Buttons
    {
        CloseBTN,
    }
    enum Texts
    {
        StatTMP,
    }

 
    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<TMPro.TMP_Text>(typeof(Texts));
        GetText((int)Texts.StatTMP).text = UI_MainBackUI.instance.NowSelectStatProperty.ToString();
        GetButton((int)Buttons.CloseBTN).onClick.AddListener(Close);
    }

 

    void Close()
    {
        Managers.UI_Manager.ClosePopupUI();
    }

   
}
