using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager i;
    User user;

    private void Awake() {
        if(i==null) i=this;
        
        SetResolution();
        DontDestroyOnLoad(gameObject);
    }

    public User GetUser() => user;
    public void SetUser(User user){
        this.user = user;
        user.SetPoint();
    }

    public void UpdateUser(){
        Backend.i.ReadUser(user.GetId(),user.GetPassword(), (newUser)=>{
            user = newUser;
            user.SetPoint();
        });
    }

    public void SetResolution()
    {
        int setWidth = 1440; // 화면 너비
        int setHeight = 2960; // 화면 높이

        //해상도를 설정값에 따라 변경
        //3번째 파라미터는 풀스크린 모드를 설정 > true : 풀스크린, false : 창모드
        Screen.SetResolution(setWidth, setHeight, true);
    }
}
