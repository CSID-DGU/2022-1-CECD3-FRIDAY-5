using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using UnityEngine.Networking;

public class statistic : MonoBehaviour
{
    public string y1,y2,m1,m2,d1,d2;
    public Dropdown year1,year2,month1,month2;
    public InputField date1,date2;
    public Button click;
    public Text show;
    
    // Start is called before the first frame update
    public void onclick()
    {
        y1 = year1.options[year1.value].text;
        y2 = year2.options[year2.value].text;
        m1 = month1.options[month1.value].text;
        m2 = month2.options[month2.value].text;
        d1 = date1.text;
        d2 = date2.text;
        show.text = string.Format("{0} {1} {2} ~ {3} {4} {5}", y1, m1, d1, y2, m2, d2);

        // StartCoroutine( UnityWebRequestPOST()); 
    }

    /*
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
    */
}
