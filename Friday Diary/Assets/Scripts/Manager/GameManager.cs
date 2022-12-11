using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager i;
    User user;

    TreeList treeList;
    int escCnt = 0;
    private void Awake() {
        if(i==null) i=this;
        escCnt = 0;
        SetResolution();
        DontDestroyOnLoad(gameObject);
    }

    private void Update() {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            escCnt++;
            if (!IsInvoking("DoubleClick"))
                Invoke("DoubleClick", 0.5f);
       
        }
        else if (escCnt == 2)
        {
            CancelInvoke("DoubleClick");
            Application.Quit();
        }
    }

    void DoubleClick()
    {
        escCnt = 0;
    }
    
    public void AddTree(Tree tree){
        if(treeList==null) treeList = new TreeList();

        treeList.tree_list.Add(tree);
    }
    public void SetTreeList(Action onFinish){
        treeList = DataManager.i.LoadGameData<TreeList>("/trees.json");
        onFinish?.Invoke();
    }

    public TreeList getTreeList()=>treeList;

    public User GetUser() => user;
    public void SetUser(User user){
        this.user = user;
        user.SetPoint();
    }

    public void UpdateUser(){
        Backend.i.ReadUser(user.GetId(),user.GetPassword(), (newUser)=>{
            user = newUser;
            user.SetPoint();
            CalendarController._calendarInstance.ReloadPanel();
            storeUI.i.UpdateStore();
        });
    }

    public void LogOut(){
        DataManager.i.DeleteGameData();
        LoadSceneManager.i.ToLogin();
    }

    public void SetResolution()
    {
        int setWidth = 1440; // 화면 너비
        int setHeight = 2960; // 화면 높이

        //해상도를 설정값에 따라 변경
        //3번째 파라미터는 풀스크린 모드를 설정 > true : 풀스크린, false : 창모드
        Screen.SetResolution(setWidth, setHeight, true);
    }

 


    private void OnApplicationPause(bool pauseStatus) {
        if (pauseStatus){
            DataManager.i.SaveGameData<TreeList>(treeList,"/trees.json");
        }
    }

    private void OnApplicationQuit() {
        DataManager.i.SaveGameData<TreeList>(treeList,"/trees.json");
    }
}
