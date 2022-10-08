using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;

public class HttpRequest : MonoBehaviour
{

    public Text inputField;

    // 테스트 버튼용
    public void OnGetBtnClick()
    {
        StartCoroutine(UnityWebRequestGET());
    }

    // GEt
    public IEnumerator UnityWebRequestGET()
    {
        string url = "http://10.70.23.54:5000/Diary";

		// UnityWebRequest에 내장되있는 GET 메소드를 사용한다.
        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();  // 응답이 올때까지 대기한다.

        if (www.error == null)  // 에러가 나지 않으면 동작.
        {
            Debug.Log(www.downloadHandler.text);
        }
        else
        {
            Debug.Log("error");
        }
    }

    // 테스트 버튼용
    public void OnPostBtnClick()
    {
        StartCoroutine(UnityWebRequestPOST());
    }

    // POST
    IEnumerator UnityWebRequestPOST()
    {
        string content = inputField.text;
        string url = "http://10.70.29.54:5000/Diary";
        string bodyJsonString = "{ \"Text\": \""+content+"\"}";

        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        
        if (request.error == null)
        {
            Debug.Log(request.downloadHandler.text);   
        }
        else
        {
            Debug.Log("error");
        }
    }
}
