using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Script.Responses;
using UnityEngine;
using UnityEngine.Networking;

public class UnityWebRequestManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    void Start(){

        StartCoroutine(GetRequest("http://dreamlo.com/lb/5d8ddbcad1041303eced691c/json"));
    }

    IEnumerator GetRequest(string uri){
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                var responseJson = webRequest.downloadHandler.text;
                var response = JsonConvert.DeserializeObject<LeaderBoardResponse>(responseJson);
//                if(response==null)
//                    print("null");
//                gameManager.GetScor(leaderBoardResponse);
//                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
            }
        }
    }
}
