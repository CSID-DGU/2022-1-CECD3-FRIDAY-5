using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitializeScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AccessToken token = DataManager.i.LoadGameData();
        if(token==null){
            SceneManager.LoadScene("Login");
        }else{
            Backend.i?.ReadUser(token.email, token.pw, (user)=>{
                GameManager.i.SetUser(user);
                SceneManager.LoadScene("Main");
            }); 
        }    
    }
    
}
