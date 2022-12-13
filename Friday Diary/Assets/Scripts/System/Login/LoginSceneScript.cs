using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Text;
using UnityEngine.SceneManagement;

public class LoginSceneScript : MonoBehaviour
{
    public InputField passwordInput, idInput; 
    public void onSignUpBtnclick()
    {
        LoadSceneManager.i.ToSignIn();
    }
    
    public void onLoginBtnclick()
    {
        Backend.i?.ReadUser(idInput.text, passwordInput.text, onLoginSuccess); 

        // Backend.i?.ReadUser("test@gmail.com", "a123456", onLoginSuccess); 
    }

    public void onLoginSuccess(User user){
        DataManager.i.SaveGameData(new AccessToken(user.GetId(), user.GetPassword()));
        GameManager.i.SetUser(user);
        Backend.i?.ReadAllObjects(GameManager.i.GetUser().GetId(),(treeList)=>{
                // do nothing
            });
        LoadSceneManager.i.ToMain();
    }
}
