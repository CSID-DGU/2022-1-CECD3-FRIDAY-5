
using System;
[System.Serializable]
public class User {
    public string id, password;
    public string name;
    int happinessPt,sadnessPt,angryPt,fearPt, surprisePt,disgustPt;

    public double happinessmoney, sadnessmoney,angrymoney, fearmoney, surprisemoney, disgustmoney;


    public string GetId() => id;
    public string GetName() => name;
    public string GetPassword() => password;
    
    public void SetPoint(){
        happinessPt = Convert.ToInt32(happinessmoney);
        sadnessPt = Convert.ToInt32(sadnessmoney);
        angryPt = Convert.ToInt32(angrymoney);
        fearPt = Convert.ToInt32(fearmoney);
        surprisePt = Convert.ToInt32(surprisemoney);
        disgustPt = Convert.ToInt32(disgustmoney);    
    }

    public int getPoint(Emotions em){
        switch(em){
            case Emotions.happiness:
                return happinessPt;
            case Emotions.sadness:
                return sadnessPt;
            case Emotions.angry:
                return angryPt;
            case Emotions.fear:
                return fearPt;
            case Emotions.surprise:
                return surprisePt;
            case Emotions.disgust:
                return disgustPt;
        }

        return -1;
    }

    public override string ToString(){
        return "\"id\": " + id + "\n\"passsword\": " + password + "\n\"name\": " + name + "\n\"happinessPt\": " + happinessPt + " \"disgustPt\": " + disgustPt +" \"sadnessPt\": " + sadnessPt +" \"angryPt\": " + angryPt +" \"surprisePt\": " + surprisePt +" \"fearPt\": " + fearPt;
    }
}

public enum UserInfo{
    id,
    password,
    happinessPt,
    disgustPt,
    sadnessPt,
    surprisePt,
    fearPt,
    angryPt
}