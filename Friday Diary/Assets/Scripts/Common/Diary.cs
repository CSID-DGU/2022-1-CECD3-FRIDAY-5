using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Diary
{
    public string id;    
    public string text;
    public string targetDate;
    public double happiness, sadness, disgust, angry, surprise, fear, neutral;
    public DiaryResult diaryResult;
    public void InitDiaryResult(){
        diaryResult = new DiaryResult(this.happiness, this.sadness, this.disgust, this.angry, this.surprise, this.fear, this.neutral);
    }
}

[System.Serializable]
public class DiaryResult{
    public double happiness, sadness, disgust, angry, surprise, fear, neutral;
    public DiaryResult(){}
    public DiaryResult(double happiness, double sadness, double disgust, double angry, double surprise, double fear, double neutral){
        this.happiness = happiness;
        this.sadness = sadness;
        this.disgust = disgust;
        this.angry = angry;
        this.surprise = surprise;
        this.fear = fear;
        this.neutral = neutral;
    }
}