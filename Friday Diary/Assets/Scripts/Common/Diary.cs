using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Diary
{
    public string id;    
    public string text;
    public string targetDate;
    public DiaryResult diaryResult;
    public Diary(){
        id ="user";
        text = "일기";
    }

    public Diary(string id, string text)
    {
        this.id = id;
        if(text.Contains("\n")){
            text = text.Replace("\n","\n\n");
        }
        this.text = text;
    }
}

[System.Serializable]
public class DiaryResult{
    public double happiness, sadness, disgust, angry, surprise, neutral;
}