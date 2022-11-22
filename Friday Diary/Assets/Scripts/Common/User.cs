
[System.Serializable]
public class User {
    public string id, password;
    public string name;
    int happinessPt;
    int disgustPt;
    int sadnessPt;
    int angryPt;
    int surprisePt;
    int fearPt;

    public string GetId() => id;
    public string GetName() => name;
    public string GetPassword() => password;
    
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