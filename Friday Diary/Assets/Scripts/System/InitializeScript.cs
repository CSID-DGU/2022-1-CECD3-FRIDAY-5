using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitializeScript : MonoBehaviour
{
    
    void Start()
    {
        /*
        // 유저 데이터 읽어오기   
        Backend.i?.ReadUser(token.email, token.pw, (user)=>{
            GameManager.i.SetUser(user);

            Backend.i?.ReadAllObjects(GameManager.i.GetUser().GetId(),(treeList)=>{

                // if(treeList != null) GameManager.i.SetTreeList(treeList);
                LoadSceneManager.i.ToMain();
            });
            
        },(message)=>{
            LoadSceneManager.i.ToLogin();
        }); */

            LoadSceneManager.i.ToLogin();
     }   
        
}
