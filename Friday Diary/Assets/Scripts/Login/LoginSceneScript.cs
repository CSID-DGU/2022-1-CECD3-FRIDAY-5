using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Text;
using UnityEngine.SceneManagement;

public class LoginSceneScript : MonoBehaviour
{
    public Text passwordInput, idInput; 
    public void onSignUpBtnclick()
    {
        SceneManager.LoadScene("signin");
    }
    
    public void onLoginBtnclick()
    {
        Backend.i.Login(idInput.text, passwordInput.text, onLoginSuccess); 
    }

    public void onLoginSuccess(User user){
        GameManager.i.SetUser(user);
    }
}
