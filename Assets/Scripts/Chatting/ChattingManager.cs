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
    public GameObject[] ChatGOs;

    void Start()
    {
        StartCoroutine(WaitUntilData());
    }



    IEnumerator WaitUntilData()
    {
        // 두 개의 코루틴을 동시에 실행
        Coroutine chatCoroutine = StartCoroutine(ReadData(ChatURL, GameChatList));
        Coroutine nameCoroutine = StartCoroutine(ReadData(NameURL, GameNameList));

        // 두 개의 코루틴이 모두 완료될 때까지 기다림
        yield return chatCoroutine;
        yield return nameCoroutine;

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
            list.Add(line);
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
        foreach(GameObject go in ChatGOs)
        {
            go.SetActive(false);
        }

        int index = 0;
        
        while(true)
        {
            StartCoroutine(MakeRandomChat(index, namelist, messagelist));
            index++;
            if (index == 3) index = 0;
            float temp = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(temp);
        }
    }

    [SerializeField] float targetTime;

    IEnumerator MakeRandomChat(int index, List<string> namelist, List<string> messagelist)
    {
        Vector3 targetScale = Vector3.one * 0.52f;
        GameObject Go = ChatGOs[index];

        Go.SetActive(true);
        Go.transform.localScale = Vector3.zero;

        Go.transform.GetChild(1).GetChild(0).GetComponent<TMPro.TMP_Text>().text = GetRandomStringFromList(namelist);
        Go.transform.GetChild(0).GetChild(0).GetComponent<TMPro.TMP_Text>().text = GetRandomStringFromList(messagelist);

        var tween = Go.transform.DOScale(targetScale, targetTime);
        yield return tween.WaitForCompletion();
    }

    string GetRandomStringFromList(List<string> list)
    {
        int rand = Random.Range(0, list.Count);
        return list[rand];
    }
}
