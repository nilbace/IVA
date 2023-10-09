using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DataManager;
using static Define;
using static REventManager;

public class UI_RandomEvent : UI_Popup
{
    public static UI_RandomEvent instance;
    EventData _eventData;
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
        _eventData = Managers.RandEvent.GetProperEvent();
        Bind<Image>(typeof(Images));
        Bind<Button>(typeof(Buttons));
        Bind<TMPro.TMP_Text>(typeof(Texts));

        Debug.Log(_eventData.EventName);

        GetText((int)Texts.EventText).text = _eventData.EventInfoString;
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
