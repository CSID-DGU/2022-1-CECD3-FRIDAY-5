using UnityEngine;

[System.Serializable]
public class Response {
    public string message;
    public string data;

    Response(string message, string data){
        this.message = message;
        this.data = data;
    }
}