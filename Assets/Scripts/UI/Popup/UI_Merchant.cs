using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class UI_Merchant : UI_Popup
{
    public static UI_Merchant instance;
    enum Buttons
    {
        ResultBTN,
    }
    enum Texts
    {
        EventText,
    }

    enum Images
    {
        CutScene,
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
        Bind<Image>(typeof(Images));
        Bind<Button>(typeof(Buttons));
        Bind<TMPro.TMP_Text>(typeof(Texts));


        GetText((int)Texts.EventText);
        GetButton((int)Buttons.ResultBTN).onClick.AddListener(Close);
    }

    void Close()
    {
        Managers.Data._myPlayerData.NowWeek++;
        UI_MainBackUI.instance.UpdateUItexts();
        Managers.Data.SaveData();
        Managers.UI_Manager.ClosePopupUI();
    }
}
