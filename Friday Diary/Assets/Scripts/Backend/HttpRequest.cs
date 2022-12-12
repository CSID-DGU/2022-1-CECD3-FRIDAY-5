using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;
using System;

public class HttpRequest : MonoBehaviour
{
    public static HttpRequest i;
    
    private void Awake() {
        if(i==null) i=this;
    }

    public void Get<T>(string url, Action<T> onSuccess, Action<string> onFailed)
    {
        StartCoroutine(UnityWebRequestGET(url, onSuccess, onFailed)) ;
    }

    // Get
    private IEnumerator UnityWebRequestGET<T>(string url, Action<T> onSuccess, Action<string> onFailed)
    {
		// UnityWebRequest에 내장되있는 GET 메소드를 사용한다.
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();  // 응답이 올때까지 대기한다.

        if (request.error == null)  // 통신 성공
        {
            Response<T> res = JsonUtility.FromJson<Response<T>>(request.downloadHandler.text);
            if(res.message.Contains("fail")){ // 처리 실패
                onFailed?.Invoke(res.message);
            }
            else{ // 처리 성공
                onSuccess?.Invoke(res.data);
            }
        }
        else // 통신 실패
        {
            onFailed?.Invoke(request.error);
            // Debug.Log(request.error);
        }
    }


    public void Post<T>(string url, string jsonString, Action<T> onSuccess,  Action<string> onFailed)
    {
        StartCoroutine(UnityWebRequestPOST(url, jsonString, onSuccess, onFailed));
    }

    // POST
    private IEnumerator UnityWebRequestPOST<T>(string url, string jsonString, Action<T> onSuccess, Action<string> onFailed)
    {   
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonString);
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        
        if (request.error == null) // 통신 성공
        {
            Response<T> res = JsonUtility.FromJson<Response<T>>(request.downloadHandler.text);
            Debug.Log(request.downloadHandler.text);
            if(url.Contains("tree_list")){
                TreeList trees = JsonUtility.FromJson<TreeList>(request.downloadHandler.text);
                 
                GameManager.i.SetTreeList(trees);
                onSuccess?.Invoke(res.data);
            }
            else if(res.message.Contains("fail")){ // 처리 실패
                onFailed?.Invoke(res.message);
            }
            else{ // 처리 성공
                // Debug.Log(res.data);
                onSuccess?.Invoke(res.data);
            }
        }
        else // 통신 실패
        {
            onFailed?.Invoke(request.error);
            // Debug.Log(request.error);
        }
    }
}
