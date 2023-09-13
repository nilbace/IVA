using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using DG.Tweening;
using static UI_SchedulePopup;

public class ChattingManager : MonoBehaviour
{
    const string NameURL = "https://docs.google.com/spreadsheets/d/1WjIWPgya-w_QcNe6pWE_iug0bsF6uwTFDRY8j2MkO3o/export?format=tsv&range=A3:A";
    const string ChatURL = "https://docs.google.com/spreadsheets/d/1WjIWPgya-w_QcNe6pWE_iug0bsF6uwTFDRY8j2MkO3o/export?format=tsv&range=B3:B";
    List<string> GameNameList = new List<string>();
    List<string> GameChatList = new List<string>();
    List<GameObject> ChatGOs = new List<GameObject>();
    public GameObject ClearChatGO;

    void Start()
    {

        int childCount = gameObject.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Transform childTransform = gameObject.transform.GetChild(i);
            GameObject childObject = childTransform.gameObject;
            ChatGOs.Add(childObject);
        }
        foreach (GameObject go in ChatGOs)
        {
            go.SetActive(false);
        }
        StartCoroutine(WaitUntilData());
    }



    IEnumerator WaitUntilData()
    {
        //비동기 방식으로 서버에서 데이터를 읽어옴
        Coroutine chatCoroutine = StartCoroutine(ReadData(ChatURL, GameChatList));
        Coroutine nameCoroutine = StartCoroutine(ReadData(NameURL, GameNameList));

        //두 개의 코루틴이 모두 완료될 때까지 기다림
        yield return chatCoroutine;
        yield return nameCoroutine;

        //채팅 생성 시작
        StartGenerateChatting(BroadCastType.Game);
    }

    IEnumerator ReadData(string www, List<string> list)
    {
        UnityWebRequest wwww = UnityWebRequest.Get(www);
        yield return wwww.SendWebRequest();

        string data = wwww.downloadHandler.text;
        string[] lines = data.Substring(0, data.Length - 1).Split('\n');

        foreach(string line in lines)
        {
            string templine = line.Substring(0, line.Length - 1);
            int count = 0;
            string modifiedLine = "";

            foreach (char c in templine)
            {
                modifiedLine += c;

                count++;
                if (count == 10)
                {
                    modifiedLine += "\n";
                    count = 0;
                }
            }

            // 마지막 글자가 줄바꿈 문자인 경우 제거
            if (modifiedLine.EndsWith("\n"))
            {
                modifiedLine = modifiedLine.Substring(0, modifiedLine.Length - 1);
            }
            list.Add(modifiedLine);
        }
    }

    public void StartGenerateChatting(BroadCastType broadCastType)
    {
        switch (broadCastType)
        {
            case BroadCastType.Game:
                StartCoroutine(StartGenerateChatting(GameNameList, GameChatList));
                break;
        }

    }

    [SerializeField] float minTime;
    [SerializeField] float maxTime;
    IEnumerator StartGenerateChatting(List<string> namelist, List<string> messagelist)
    {
        

        int index = 0;
        
        while(true)
        {
            foreach(GameObject go in ChatGOs)
            {
                if(go.activeSelf)
                {
                    var rectTransform = go.GetComponent<RectTransform>();
                    var targetPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y + ClearChatGO.GetComponent<RectTransform>().sizeDelta.y);
                    var tween = rectTransform.DOAnchorPos(targetPosition, 0.3f);
                }
            }

            yield return StartCoroutine(MakeRandomChat(index, namelist, messagelist));
            

            
            ChatGOs[(index + ChatGOs.Count + 1) % ChatGOs.Count].SetActive(false);
            index++;
            if (index == ChatGOs.Count) index = 0;

            float temp = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(temp);
        }
    }

    [SerializeField] float targetTime;
    [SerializeField] float Yoffset;

    IEnumerator MakeRandomChat(int index, List<string> namelist, List<string> messagelist)
    {
        Vector3 targetScale = Vector3.one * 0.52f;
        GameObject Go = ChatGOs[index];

        Go.SetActive(true);
        Go.transform.localScale = Vector3.zero;
        Go.GetComponent<RectTransform>().anchoredPosition = new Vector3(-35f, ClearChatGO.GetComponent<RectTransform>().sizeDelta.y/2f + Yoffset, 0);

        Go.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<TMPro.TMP_Text>().text = GetRandomStringFromList(namelist);
        Go.transform.GetChild(0).GetChild(0).GetComponent<TMPro.TMP_Text>().text = GetRandomStringFromList(messagelist);
        ClearChatGO.GetComponent<TMPro.TMP_Text>().text = Go.transform.GetChild(0).GetChild(0).GetComponent<TMPro.TMP_Text>().text;

        var tween = Go.transform.DOScale(targetScale, targetTime);
        yield return tween.WaitForCompletion();
    }

    string GetRandomStringFromList(List<string> list)
    {
        int rand = Random.Range(0, list.Count);
        return list[rand];
    }
}
