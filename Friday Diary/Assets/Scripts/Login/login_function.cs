using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Text;


[System.Serializable]
public class Login  
{
    public string id, password;

    public Login(){
    }

    public Login(string id, string password)
    {
        this.id = id;
        this.password = password;
    }
}

public class login_function : MonoBehaviour
{
    public InputField passwordInput, idInput; 
    public Text error; 

    public void click()
    {
        string idST = idInput.text;
        string passwordST = passwordInput.text;
        Login newLogin= new Login(idST,passwordST);
        StartCoroutine( UnityWebRequestPOST(newLogin));      
    }

    IEnumerator UnityWebRequestPOST(Login newLogin)
    {
        string url = "http://15.164.6.142:8080/member_read "; //서버주소
        string bodyJsonString = JsonUtility.ToJson(newLogin);
        Debug.Log(bodyJsonString);

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
