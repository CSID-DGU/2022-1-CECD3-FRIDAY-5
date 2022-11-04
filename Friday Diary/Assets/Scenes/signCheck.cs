using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class signCheck : MonoBehaviour
{
    public InputField username;
    public InputField email;
    public InputField password;
    public InputField password2;
    public Button signButton;
    public Text usernameError;
    public Text emailError;
    public Text passwordError; 
    public Text equalError; 


    public void signButtonClick()
    {
        if(usernameCheck( username , usernameError)==true && emailCheck( email, emailError )==true && passwordCheck(password, passwordError)==true && passwordEqual(password,password2,equalError)==true )
        {
            Debug.Log("회원가입 성공!!!");
            //UnityWebRequestPOST();

        }
        else
        {
            Debug.Log("회원가입 실패ㅠㅠ");
            // 백엔드 통신 --> 데이터 전송 
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


}
