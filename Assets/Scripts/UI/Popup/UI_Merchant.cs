using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class UI_Merchant : UI_Popup
{
    public static UI_Merchant instance;
    public GameObject ItemBTN;
    List<Item> itemList = new List<Item>();

    enum Buttons
    {
        CloseBTN,
    }
    enum Texts
    {
        EventText,
    }

    enum Images
    {
        CutScene,
    }

    enum GameObjects
    {
        Content,
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
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));

        UpdateTexts();

        GetButton((int)Buttons.CloseBTN).onClick.AddListener(Close);
    }

    void Close()
    {
        Managers.Data._myPlayerData.NowWeek++;
        UI_MainBackUI.instance.UpdateUItexts();
        Managers.Data.SaveData();
        Managers.UI_Manager.ClosePopupUI();
    }

    public void UpdateTexts()
    {
        Transform parent = GetGameObject((int)GameObjects.Content).transform;
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }

        foreach (Item temp in Managers.Data.ItemList)
        {
            if (temp.EntWeek == Managers.Data._myPlayerData.NowWeek)
            {
                itemList.Add(temp);
            }
        }


        for (int i = 0; i < itemList.Count; i++)
        {
            GameObject Go = Instantiate(ItemBTN, parent, false);
            Go.GetComponent<MerChantItem>().Setting(itemList[i]);
        }
    }
}
