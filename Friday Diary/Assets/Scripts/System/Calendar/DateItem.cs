using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DateItem : MonoBehaviour
{
    [SerializeField]
    GameObject diaryHist;
    [SerializeField]
    TextMeshProUGUI date;
    [SerializeField]
    Text yearlabel;
    [SerializeField]
    Text monthlabel;

    [SerializeField]
    TextMeshProUGUI content;
    [SerializeField]
    Text happiness;

     [SerializeField]
    Text sadness;
     [SerializeField]
    Text angry;
     [SerializeField]
    Text surprise;
     [SerializeField]
    Text disgust;
     [SerializeField]
    Text fear;

    public void onClick(){
        
        if(GameManager.i.GetUser()==null && !gameObject.transform.GetChild(1).GetComponent<Image>().enabled){
            return;
        }
        
        string userid = GameManager.i.GetUser().GetId();
        string day= gameObject.transform.GetChild(0).GetComponent<Text>().text.PadLeft(2,'0');
        string year = yearlabel.text.Substring(0, yearlabel.text.Length - 1);
        string month = monthlabel.text.Substring(0, monthlabel.text.Length - 1).PadLeft(2,'0');

        string targetDate = year+month+day;
        date.text = year +"년 " + month+"월 " + day+"일";
        Backend.i.ReadDiary(userid, targetDate, (diary)=>{
                diary.InitDiaryResult();
                diaryHist.SetActive(true);
                content.text = diary.text;
                Debug.Log(diary.text);
            
                happiness.text = "행복 "+Math.Round(diary.happiness*100,1);
                sadness.text = "슬픔 "+Math.Round(diary.sadness*100,1);
                angry.text = "분노 "+Math.Round(diary.angry*100,1);
                surprise.text = "놀람 "+Math.Round(diary.surprise*100,1);
                disgust.text = "혐오 "+Math.Round(diary.disgust*100,1);
                fear.text = "공포 "+Math.Round(diary.fear*100,1);
        });


    }

}
