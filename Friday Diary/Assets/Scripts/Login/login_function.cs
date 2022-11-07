using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Text;


public class login_function : MonoBehaviour
{
    public InputField password;
    public InputField email;
    public Text error; 

    public void click()
    {
        StartCoroutine( UnityWebRequestPOST());      
    }

    IEnumerator UnityWebRequestPOST()
    {
        string emailData = email.text;
        string passwordData = password.text;
        string bodyJsonString = " { \"ID\": \""+emailData+"\", \"Text\": \""+passwordData+"\"} ";
        string url = "http://10.70.16.159:8080/diary_create"; //서버주소

        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        
        yield return request.SendWebRequest();
        
        if (request.error == null)
        {
            Debug.Log(request.downloadHandler.text);  
            // 메인화면으로 이동 
        }
        else
        {
            Debug.Log(request.error);
            Debug.Log("error");
            error.text = " 로그인 정보를 확인해 주세요";
            // 로그인 실패 메세지 출력 
        }
        
    }
}
