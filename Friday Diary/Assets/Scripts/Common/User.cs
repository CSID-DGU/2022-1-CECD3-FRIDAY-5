
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
        happinessPt = Convert.ToInt32(happinessmoney*100);
        sadnessPt = Convert.ToInt32(sadnessmoney*100);
        angryPt = Convert.ToInt32(angrymoney*100);
        fearPt = Convert.ToInt32(fearmoney*100);
        surprisePt = Convert.ToInt32(surprisemoney*100);
        disgustPt = Convert.ToInt32(disgustmoney*100);    
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

    public int getPoint(string em){
        switch(em){
            case "happiness":
                return happinessPt;
            case "sadness":
                return sadnessPt;
            case "angry":
                return angryPt;
            case "fear":
                return fearPt;
            case "surprise":
                return surprisePt;
            case "disgust":
                return disgustPt;
        }

        return -1;
    }

    
    public int setPoint(string em, int money){
        switch(em){
            case "happiness":
                this.happinessmoney = money;
               break;
            case "sadness":
                this.happinessmoney = money;
               break;
            case "angry":
                this.happinessmoney = money;
               break;
            case "fear":
                this.happinessmoney = money;
               break;
            case "surprise":
                this.happinessmoney = money;
               break;
            case "disgust":
                this.happinessmoney = money;
               break;
        }

        return -1;
    }
    
    public int setPoint(Emotions em, int money){
        switch(em){
            case Emotions.happiness:
               this.happinessmoney = money;
               break;
            case Emotions.sadness:
                this.sadnessmoney = money;
                break;
            case Emotions.angry:
                this.angrymoney = money;
                break;
            case Emotions.fear:
                this.fearmoney = money;
                break;
            case Emotions.surprise:
                this.surprisemoney = money;
                break;
            case Emotions.disgust:
                this.disgustmoney = money;
                break;
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