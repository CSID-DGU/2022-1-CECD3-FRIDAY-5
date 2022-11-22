using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Text;

public class SignUpSceneScript : MonoBehaviour
{
    [SerializeField]
    private InputField username, email, password, password2;

    [SerializeField]
    private Text usernameError, emailError, passwordError, equalError; 

    [SerializeField]
    private GameObject userIcon, emailIcon, pwIcon, eqIcon;

    private bool isNameValid, isEmailValid, isPwValid, isPwEqual;


    public void signButtonClick()
    {
        if(isNameValid && isEmailValid && isPwValid && isPwEqual)
        {
            Backend.i.SignUp(email.text, password.text, username.text, onSignUpSuccess);
        }
    }

    public void onSignUpSuccess(string message){

    }

    public void usernameCheck()
    {
        Regex regex = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z]{6,10}$");
 
        bool ismatch = regex.IsMatch(username.text);  //비교 문자열이 정규식에 맞는지 체크
 
        if (!ismatch)
        {
            usernameError.text = "닉네임 입력 형식을 확인해 주세요";
            isNameValid = false;
            userIcon.SetActive(false);
        }
        else
        {
            usernameError.text = " ";
            isNameValid= true;
            userIcon.SetActive(true);
        }
    }
     

    public void emailCheck()
    {
        Regex regex = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z0-9+-_.]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$");
 
        bool ismatch = regex.IsMatch(email.text);  //비교 문자열이 정규식에 맞는지 체크
 
        if (!ismatch)
        {
            emailError.text = "이메일 입력 형식을 확인해 주세요";
            isEmailValid = false;
            emailIcon.SetActive(false);
        }
        else
        {
            emailError.text = " ";
            isEmailValid = true;
            emailIcon.SetActive(true);
        }
    }

    public void passwordCheck()
    {
        Regex regex = new System.Text.RegularExpressions.Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{7,}$");
 
        bool ismatch = regex.IsMatch( password.text);  //비교 문자열이 정규식에 맞는지 체크
 
        if (!ismatch)
        {
            passwordError.text = "비밀번호 입력 형식을 확인해 주세요";
            isPwValid = false;
            pwIcon.SetActive(false);
        }
        else
        {
            passwordError.text = " ";
            isPwValid = true;
            pwIcon.SetActive(true);
        }
    }

    
   public void passwordEqual()
    {
         if(password.text == password2.text)
        {
            equalError.text = " ";
            isPwEqual = true;
            eqIcon.SetActive(true);
        }
        else
        {
            equalError.text = "비밀번호가 일치하지 않습니다";
            isPwEqual = false;
            eqIcon.SetActive(false);
        }       
    }

}
