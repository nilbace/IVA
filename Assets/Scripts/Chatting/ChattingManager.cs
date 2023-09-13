using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using DG.Tweening;
using static UI_SchedulePopup;

public class ChattingManager : MonoBehaviour
{
    #region URL&StringLists

    const string NameURL = "https://docs.google.com/spreadsheets/d/1WjIWPgya-w_QcNe6pWE_iug0bsF6uwTFDRY8j2MkO3o/export?format=tsv&range=A3:A";
    List<string> ViewersNameList = new List<string>();

    const string GaneChatURL = "https://docs.google.com/spreadsheets/d/1WjIWPgya-w_QcNe6pWE_iug0bsF6uwTFDRY8j2MkO3o/export?format=tsv&range=B3:B";
    List<string> GameChatList = new List<string>();

    #endregion


    List<GameObject> ChatGOs = new List<GameObject>();
    public GameObject ClearChatGO;

    [SerializeField] float _chatScale;

    void Start()
    {
        //�迭�� ����ְ� ��Ȱ��ȭ
        int childCount = gameObject.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Transform childTransform = gameObject.transform.GetChild(i);
            GameObject childObject = childTransform.gameObject;
            ChatGOs.Add(childObject);
            childObject.SetActive(false);
        }
         
        StartCoroutine(RequestListDatasFromSheet());
    }



    IEnumerator RequestListDatasFromSheet()
    {
        //�񵿱� ������� �������� �����͸� �о��
        Coroutine chatCoroutine = StartCoroutine(RequestAndSetDatas(GaneChatURL, GameChatList));
        Coroutine nameCoroutine = StartCoroutine(RequestAndSetDatas(NameURL, ViewersNameList));

        //�� ���� �ڷ�ƾ�� ��� �Ϸ�� ������ ��ٸ�
        yield return chatCoroutine;
        yield return nameCoroutine;

        //ä�� ���� ����
        StartGenerateChattingByType(BroadCastType.Game);
    }

    IEnumerator RequestAndSetDatas(string www, List<string> list)
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

                //9���� �̻��϶� ����ų�
                if (count >= 9 && c == ' ')
                {
                    modifiedLine = modifiedLine.Substring(0, modifiedLine.Length - 1);
                    modifiedLine += "\n";
                    count = 0;
                }
                //11���ڰ� �Ѿ�� ������ �ٹٲ�
                else if(count > 11)
                {
                    modifiedLine += "\n";
                    count = 0;
                }
            }

            // ������ ���ڰ� �ٹٲ� ������ ��� ����
            if (modifiedLine.EndsWith("\n"))
            {
                modifiedLine = modifiedLine.Substring(0, modifiedLine.Length - 1);
            }
            list.Add(modifiedLine);
        }
    }

    //��� Ÿ���� �޾Ƽ� ��� ������
    public void StartGenerateChattingByType(BroadCastType broadCastType)
    {
        switch (broadCastType)
        {
            case BroadCastType.Game:
                StartCoroutine(StartGenerateChatting(GameChatList));
                break;
        }

    }

    //ä�� ������ �ð� ����
    [SerializeField] float minChatDelayTime;
    [SerializeField] float maxChatDelayTime;
    //ä�� ���� ����
    [SerializeField] float SpaceBetweenChats;


    //ä�� ���� ����
    IEnumerator StartGenerateChatting(List<string> messagelist)
    {
        int index = 0;
        
        while(true)
        {
            //�� â ���� ����(������ ������)
            string tempMessage = GetRandomStringFromList(messagelist);
            ClearChatGO.GetComponent<TMPro.TMP_Text>().text = tempMessage;
            yield return new WaitForEndOfFrame();
            float newYoffset = ClearChatGO.GetComponent<RectTransform>().sizeDelta.y + SpaceBetweenChats;
            newYoffset *= _chatScale;

            //�� ������ ��
            ChatGOs[(index + ChatGOs.Count + 1) % ChatGOs.Count].SetActive(false);

            //������ ���� ���� �ø�
            foreach (GameObject go in ChatGOs)
            {
                if (go.activeSelf)
                {
                    var rectTransform = go.GetComponent<RectTransform>();
                    var targetPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y + newYoffset);
                    rectTransform.DOAnchorPos(targetPosition, 0.3f);
                }
            }
            yield return new WaitForSeconds(0.3f);

            //�� �޽��� �ؿ� ����
            yield return StartCoroutine(MakeRandomChat(index, tempMessage));

            index++;
            if (index == ChatGOs.Count) index = 0;

            float temp = Random.Range(minChatDelayTime, maxChatDelayTime);
            yield return new WaitForSeconds(temp);
        }
    }

    //�� ä���� Ŀ���� �ð�
    [SerializeField] float TimeForChatGetBigger;


    [SerializeField] float ChatGenerateYPoz;

    /// <summary>
    /// qwer
    /// </summary>
    /// <param name="index"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    IEnumerator MakeRandomChat(int index, string message)
    {
        Vector3 targetScale = Vector3.one * _chatScale;
        GameObject Go = ChatGOs[index];

        Go.SetActive(true);
        Go.transform.localScale = Vector3.zero;
        Go.GetComponent<RectTransform>().anchoredPosition = new Vector3(-46f, ClearChatGO.transform.GetComponent<RectTransform>().sizeDelta.y/2f* _chatScale + ChatGenerateYPoz, 0);

        Go.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<TMPro.TMP_Text>().text = GetRandomStringFromList(ViewersNameList);
        Go.transform.GetChild(0).GetChild(0).GetComponent<TMPro.TMP_Text>().text = message;
        ClearChatGO.GetComponent<TMPro.TMP_Text>().text = Go.transform.GetChild(0).GetChild(0).GetComponent<TMPro.TMP_Text>().text;

        var tween = Go.transform.DOScale(targetScale, TimeForChatGetBigger);
        yield return tween.WaitForCompletion();
    }

    string GetRandomStringFromList(List<string> list)
    {
        int rand = Random.Range(0, list.Count);
        return list[rand];
    }
}
