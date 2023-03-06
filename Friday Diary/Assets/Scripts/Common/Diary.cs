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

        if(neutral == 1.0f){
            return Emotions.neutral;
        }

        Emotions res = Emotions.happiness;
        double maxVal = happiness;

        if(sadness != 0 && sadness >= maxVal){
            if(sadness==maxVal){
                if(Random.Range(0f,1f) > 0.5f){
                    maxVal=sadness;
                    res = Emotions.sadness;
                }
            }else{
                maxVal=sadness;
                    res = Emotions.sadness;
            }
        }
        if(disgust != 0 && disgust > maxVal){
            if(disgust==maxVal){
                if(Random.Range(0f,1f) > 0.5f){
                    maxVal=disgust;
                    res = Emotions.disgust;
                }
            }
            else{
                maxVal=disgust;
                res = Emotions.disgust;
            }
        }


        if(angry != 0 && angry > maxVal){
            if(angry==maxVal){
                if(Random.Range(0f,1f) > 0.5f){
                    maxVal=angry;
                    res = Emotions.angry;
                }
            }
            else{
                     maxVal=angry;
                    res = Emotions.angry;               
            }
        }


        if(surprise != 0 && surprise > maxVal){
            if(surprise==maxVal){
                if(Random.Range(0f,1f) > 0.5f){
                    maxVal=surprise;
                    res = Emotions.surprise;
                }
            }
            else{
                                    maxVal=surprise;
                    res = Emotions.surprise;
            }
        }


        if(fear != 0 && fear > maxVal){
            if(fear==maxVal){
                if(Random.Range(0f,1f) > 0.5f){
                    maxVal=fear;
                    res = Emotions.fear;
                }
            }
            else{
                       maxVal=fear;
                    res = Emotions.fear;             
            }
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
