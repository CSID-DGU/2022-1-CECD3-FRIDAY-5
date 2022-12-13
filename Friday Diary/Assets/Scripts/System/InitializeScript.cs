using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitializeScript : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Start()
    {
        /*
            Backend.i?.ReadUser(token.email, token.pw, (user)=>{
                GameManager.i.SetUser(user);

                Backend.i?.ReadAllObjects(GameManager.i.GetUser().GetId(),(treeList)=>{

                    // if(treeList != null) GameManager.i.SetTreeList(treeList);
                    LoadSceneManager.i.ToMain();
                });
                
            },(message)=>{
                LoadSceneManager.i.ToLogin();
            }); 
*/
            LoadSceneManager.i.ToLogin();
        }   
        
}
