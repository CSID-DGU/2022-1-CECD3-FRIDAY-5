using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using UnityEngine.Networking;

[System.Serializable]
public class StatisticDate // 통계 기간 클래스 
{
    public string  date_start, date_end;


    public StatisticDate(){
    }

    public StatisticDate(string y1,string y2, string m1, string m2, string d1, string d2)
    {
        
        this.date_start = y1+m1.PadLeft(2,'0')+d1.PadLeft(2,'0');
        this.date_end = y2+m2.PadLeft(2,'0')+d2.PadLeft(2,'0');
    }

}

[System.Serializable]

public class EmotionStats 
{
    public double angry, disgust, fear, happiness, neutral, sadness, sum, surprise;

    public EmotionStats(){
    }

    public EmotionStats(double angry, double disgust, double fear, double happiness, double neutral, double sadness, double sum, double surprise)
    {
        this.happiness = happiness;
        this.sadness = sadness;
        this.angry = angry;
        this.disgust = disgust;
        this.fear = fear;
        this.surprise = surprise; 
        this.neutral = neutral;
        this.sum = sum; 
    }
}

public class statistic : MonoBehaviour
{
    public Dropdown year1,year2,month1,month2;
    public InputField date1,date2;
    public Button click;
    public Text show;    
    public Image i0,i1,i2,i3,i4,i5;

    
    // Start is called before the first frame update
    public void onclick()
    {
        StatisticDate newDate = new StatisticDate(year1.options[year1.value].text, year2.options[year2.value].text,
        month1.options[month1.value].text,month2.options[month2.value].text, date1.text, date2.text );
        show.text = string.Format("{0} ~ {1}", newDate.date_start, newDate.date_end);
 
        //StartCoroutine( UnityWebRequestPOST(newDate)); 
        Backend.i.ReadStatistics(GameManager.i.GetUser().GetId(), newDate.date_start,newDate.date_end, OnReadStatSuccess);
    }

    public void OnReadStatSuccess(EmotionStats res){
        Debug.Log(res);
        graphVisualize(res);
    }
    
    public void graphVisualize(EmotionStats res)
    {
        i0.fillAmount = (float)res.happiness;
        i1.fillAmount = (float)res.sadness;
        i2.fillAmount = (float)res.angry;
        i3.fillAmount = (float)res.disgust;
        i4.fillAmount = (float)res.fear;
        i5.fillAmount = (float)res.surprise;
    } 
    
    IEnumerator UnityWebRequestPOST(StatisticDate newDate)
    {
        //string content = inputField.text;
        string url = "http://172.30.1.37:8080/statistic_read"; // back
        // string url = "http://10.60.3.185:5000/Diary"; // model

        string bodyJsonString = JsonUtility.ToJson(newDate);
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
        }
        else
        {
            Debug.Log(request.error);
        }
    }
    
}
