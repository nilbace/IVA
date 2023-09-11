using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    #region Schedule
    public Action StartWeekSchedule = null;

    public void ResetStartWeekScheduleAction()
    {
        StartWeekSchedule = null;
    }

    

    #endregion
}
