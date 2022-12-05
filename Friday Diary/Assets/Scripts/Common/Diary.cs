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

    public bool hasText(){
        if(text == null || text == ""){
            return false;
        }
        return true;
    }

    public Emotions GetMaxEmotionType(){
        Emotions res = Emotions.happiness;
        double maxVal = happiness;

        if(sadness > maxVal){
            maxVal=sadness;
            res = Emotions.sadness;
        }
        if(disgust > maxVal){
            maxVal=disgust;
            res = Emotions.disgust;
        }


        if(angry > maxVal){
            maxVal=angry;
            res = Emotions.angry;
        }


        if(surprise > maxVal){
            maxVal=surprise;
            res = Emotions.surprise;
        }


        if(fear > maxVal){
            maxVal=fear;
            res = Emotions.fear;
        }

        return res;
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
