using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Common : MonoBehaviour
{

}

public class MyColor{
    public static string happiness = "#FFACB3";
    public static string disgust = "#4BCF9B";
    public static string sadness = "#7ECFE0";
    public static string surprise = "#FFD328";
    public static string angry = "#DE3745";
    public static string fear = "#CCA2DE";

    public static string getColor(Emotions e){
        switch(e){
            case Emotions.happiness:
                return happiness;
            case Emotions.sadness:
                return sadness;
            case Emotions.angry:
                return angry;
            case Emotions.fear:
                return fear;
            case Emotions.surprise:
                return surprise;
            case Emotions.disgust:
                return disgust;
        }

        return null;
    }
}

public enum Emotions{
    happiness = 0,
    sadness = 1,
    angry = 2,
    fear = 3,
    surprise = 4,
    disgust = 5
}