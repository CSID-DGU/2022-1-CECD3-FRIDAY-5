using UnityEngine;

[System.Serializable]
public class Response<T> {
    public string message;
    public T data;
}