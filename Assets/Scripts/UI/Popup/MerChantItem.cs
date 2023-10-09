using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class MerChantItem : MonoBehaviour
{
    public TMPro.TMP_Text NameTmp;
    public TMPro.TMP_Text InfoTmp;
    Item _thisItem;

    public void Setting(Item item)
    {
        _thisItem = item;

        if(_thisItem.Cost <= Managers.Data._myPlayerData.nowGoldAmount && !IsBought(_thisItem))
        {
            NameTmp.text = _thisItem.ItemName + "     /" + _thisItem.Cost;
            InfoTmp.text = "°ÔÀÓ : " + _thisItem.SixStats[0].ToString() + " /";
            InfoTmp.text += "³ë·¡ : " + _thisItem.SixStats[1].ToString() + " /";
            InfoTmp.text += "ÀúÃª : " + _thisItem.SixStats[2].ToString() + "\n";
            InfoTmp.text += "±Ù·Â : " + _thisItem.SixStats[3].ToString() + " /";
            InfoTmp.text += "¸àÅ» : " + _thisItem.SixStats[4].ToString() + " /";
            InfoTmp.text += "Çà¿î : " + _thisItem.SixStats[5].ToString();
            GetComponent<Button>().interactable = true;
        }
        else
        {
            NameTmp.text = "±¸¸Å ºÒ°¡";
            InfoTmp.text = "";
            GetComponent<Button>().interactable = false;
        }

        GetComponent<Button>().onClick.AddListener(Buy);
    }

    bool IsBought(Item item)
    {
        foreach(string temp in Managers.Data._myPlayerData.BoughtItems)
        {
            if (temp == item.ItemName) return true;
        }
        return false;
    }

    void Buy()
    {
        Managers.Data._myPlayerData.nowGoldAmount -= _thisItem.Cost;

        for(int i = 0;i<6;i++)
        {
            Managers.Data._myPlayerData.SixStat[i] += _thisItem.SixStats[i];
        }

        Managers.Data._myPlayerData.BoughtItems.Add(_thisItem.ItemName);
        Managers.Data.SaveData();
        UI_Merchant.instance.UpdateTexts();
    }
    
}
