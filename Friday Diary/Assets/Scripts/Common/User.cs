
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
    

    public User(){
    }

    public User(string id, string name, string password)
    {
        this.id = id;
        this.name = name;
        this.password = password;
    }
}