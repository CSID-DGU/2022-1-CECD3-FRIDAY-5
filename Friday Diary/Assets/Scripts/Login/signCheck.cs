using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.Networking;
using System.Text;


[System.Serializable]
public class Signin  
{
    public string id, password,name;

    public Signin(){
    }

    public Signin(string id, string password, string name)
    {
        this.id = id;
        this.password = password;
        this.name = name; 
    }
}
public class signCheck : MonoBehaviour
{
    public InputField username, email, password, password2;
    public Button signButton;
    public Text usernameError, emailError, passwordError, equalError; 


    public void signButtonClick()
    {
        if(usernameCheck( username , usernameError)==true && emailCheck( email, emailError )==true && passwordCheck(password, passwordError)==true && passwordEqual(password,password2,equalError)==true )
        {
            Signin newSignin= new Signin(email.text, password.text, username.text);
            StartCoroutine( UnityWebRequestPOST(newSignin));
            Debug.Log("회원가입 성공");

        }
        else
        {
            Debug.Log("회원가입 실패");
        }
    }

    public bool usernameCheck( InputField username, Text usernameError )
    {
        Regex regex = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z]{6,10}$");
 
        bool ismatch = regex.IsMatch( username.text);  //비교 문자열이 정규식에 맞는지 체크
 
        if (!ismatch)
        {
            usernameError.text = "닉네임 입력 형식을 확인해 주세요";
            return false;
        }
        else
        {
            usernameError.text = " ";
            return true;
        }
    }
     
   public bool passwordEqual( InputField password, InputField password2 , Text equalError)
    {
         if(password.text == password2.text)
        {
            equalError.text = " ";
            return true; 
        }
        else
        {
            equalError.text = "비밀번호가 일치하지 않습니다";
            return false;
        }       
    }

    public bool emailCheck( InputField email , Text emailError)
    {
        Regex regex = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z0-9+-_.]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$");
 
        bool ismatch = regex.IsMatch( email.text);  //비교 문자열이 정규식에 맞는지 체크
 
        if (!ismatch)
        {
            emailError.text = "이메일 입력 형식을 확인해 주세요";
            return false;
        }
        else
        {
            emailError.text = " ";
            return true;
        }
    }

    public bool passwordCheck( InputField password, Text passwordError )
    {
        Regex regex = new System.Text.RegularExpressions.Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{7,}$");
 
        bool ismatch = regex.IsMatch( password.text);  //비교 문자열이 정규식에 맞는지 체크
 
        if (!ismatch)
        {
            passwordError.text = "비밀번호 입력 형식을 확인해 주세요";
            return false;
        }
        else
        {
            passwordError.text = " ";
            return true;
        }
    }

    IEnumerator UnityWebRequestPOST(Signin newSignin)
    {
        string url = "http://15.164.6.142:8080/member_create"; //서버주소
        string bodyJsonString = JsonUtility.ToJson(newSignin);
        Debug.Log(bodyJsonString);

        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        
        if (request.error == null)
        {
            Debug.Log(request.downloadHandler.text);  
        }
        else
        {
            Debug.Log(request.error);
            Debug.Log("error");
        } 
    }

}
