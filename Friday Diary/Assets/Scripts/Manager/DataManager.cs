using System.IO;
using UnityEngine;
using System;

public class DataManager : MonoBehaviour
{

    // ---싱글톤으로 선언--- //
    public static DataManager i;

    private void Start()
    {
        if(i==null) i=this;
        DontDestroyOnLoad(gameObject);
    }

    // --- 게임 데이터 파일이름 설정 ("원하는 이름(영문).json") --- //

    // 불러오기
    public AccessToken LoadGameData()
    {
        string filePath = Application.persistentDataPath + @"\data.json";

        // 저장된 게임이 있다면
        if (File.Exists(filePath))
        {
            // 저장된 파일 읽어오고 Json을 클래스 형식으로 전환해서 할당
            string FromJsonData = File.ReadAllText(filePath);
            return JsonUtility.FromJson<AccessToken>(FromJsonData);
        }

        return null;
    }

    public T LoadGameData<T>(string url)
    {
        string filePath = Application.persistentDataPath + url;

        // 저장된 파일이 있다면
        if (File.Exists(filePath))
        {
            // 저장된 파일 읽어오고 Json을 클래스 형식으로 전환해서 할당
            string FromJsonData = File.ReadAllText(filePath);
            Debug.Log(FromJsonData);
            return JsonUtility.FromJson<T>(FromJsonData);
        }

        return default(T);
    }
    public void DeleteGameData(){
        string filePath = Application.persistentDataPath + @"\data.json";

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }


    // 저장하기
    public void SaveGameData(AccessToken token)
    {
        // 클래스를 Json 형식으로 전환 (true : 가독성 좋게 작성)
        string jsonData = JsonUtility.ToJson(token, true);

        File.WriteAllText(Application.persistentDataPath + @"\data.json", jsonData.ToString());
    }

    public void SaveGameData<T>(T obj, string url)
    {
        // 클래스를 Json 형식으로 전환 (true : 가독성 좋게 작성)
        string jsonData = JsonUtility.ToJson(obj, true);

        File.WriteAllText(Application.persistentDataPath + url, jsonData.ToString());
    }

}

[Serializable] 
public class AccessToken
{
    public string email;
    public string pw;
    public string accessToken; 

    public AccessToken(string email, string pw){
        this.email = email;
        this.pw =pw;
    }
}