using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager i;
    User user;

    private void Awake() {
        if(i==null) i=this;
        
        DontDestroyOnLoad(gameObject);
    }

    public User GetUser() => user;
    public void SetUser(User user){
        this.user = user;
    }

    public void UpdateUser(){
        Backend.i.ReadUser(user.GetId(),user.GetPassword(), (newUser)=>{
            user = newUser;
            user.SetPoint();
        });
    }
}
