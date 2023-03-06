using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Security.Cryptography;


public class Common : MonoBehaviour
{

}

public class Crypto{
    public static string SHA256Hash(string data) {

        SHA256 sha = new SHA256Managed ();
        byte[] hash = sha.ComputeHash (Encoding.ASCII.GetBytes (data));
        StringBuilder stringBuilder = new StringBuilder();
        foreach (byte b in hash) {
            stringBuilder.AppendFormat ("{0:x2}", b);
        }
        return stringBuilder.ToString ();
    }
}

public class MyColor{
    public static string happiness = "#FFACB3";
    public static string disgust = "#4BCF9B";
    public static string sadness = "#7ECFE0";
    public static string surprise = "#FFD328";
    public static string angry = "#DE3745";
    public static string fear = "#CCA2DE";
    public static string neutral = "#BEBEBE";


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

        return neutral;
    }
}

public enum Emotions{
    happiness = 0,
    sadness = 1,
    angry = 2,
    fear = 3,
    surprise = 4,
    disgust = 5,
    neutral= 6
}