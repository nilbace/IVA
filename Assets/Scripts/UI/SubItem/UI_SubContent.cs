using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using static UI_SchedulePopup;

public class UI_SubContent : UI_Base
{
    public ScheduleData thisSubSchedleData;
    public Button thisBTN;

    enum Texts
    { Text }

    string _name;

    private void Awake()
    {
        Init();
    }

    public override void Init()
    {
        thisBTN = GetComponent<Button>();
        Bind<TMP_Text>(typeof(Texts));
    }

    public void SetInfo(ScheduleData scheduleData)
    {
        thisBTN.onClick.RemoveAllListeners();
        thisSubSchedleData = scheduleData;
        GetText(0).text = thisSubSchedleData.KorName;
        thisBTN.onClick.AddListener(SetSchedule);
    }

    void SetSchedule()
    {
        UI_SchedulePopup.instance.SetDaySchedule(thisSubSchedleData);
    }
}
