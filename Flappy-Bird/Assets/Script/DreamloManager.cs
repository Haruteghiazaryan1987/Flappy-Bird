using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Script.Responses;
using UnityEngine;
using UnityEngine.Networking;

public class DreamloManager : MonoBehaviour
{
    private string privateCode = "xuicse0lpUSa5lQrNPQKxwzzyng45fDU6CHbnUr2PhKg";
    private string publicCode = "5d8ddbcad1041303eced691c";
    private string webURL = "http://dreamlo.com/lb/";
    public event Action<LeaderBoardResponse> OnLeaderboard; 
    public void AddScore(string name, int score){
        StartCoroutine(SaveScore(name, score));
    }
    public void LoadLeaderboard(){
        StartCoroutine(loadLeaderboard());
    }
    IEnumerator SaveScore(string name, int score){
        using (var webRequest=UnityWebRequest.Get(webURL+privateCode+"/add/"+$"{name}/"+score))
        {
            yield return webRequest.SendWebRequest();
            if (!string.IsNullOrEmpty(webRequest.error))
            {
                Debug.LogError(webRequest.error);
                yield break;
            }
        }
    }

    private IEnumerator loadLeaderboard(){
        using (var webRequest = UnityWebRequest.Get(webURL + publicCode + "/json"))
        {
            yield return webRequest.SendWebRequest();
            var json = webRequest.downloadHandler.text;
            if (!string.IsNullOrEmpty(webRequest.error))
            {
                Debug.LogError(webRequest.error);
                yield break;
            }

            var response = JsonConvert.DeserializeObject<LeaderBoardResponse>(json);
            OnLeaderboard?.Invoke(response);
        }
    }
}
