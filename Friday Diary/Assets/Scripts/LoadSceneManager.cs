using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public enum Scene{
    login,
    Main,
    signin
}
public class LoadSceneManager : MonoBehaviour
{
    public static LoadSceneManager i;

    private void Awake() {
        if(i==null) i=this;
    }
    private void Start() {
        LoadingWindow.i.StartLoading();

        if(SceneManager.GetActiveScene().isLoaded){
            LoadingWindow.i.EndLoading(2f,()=>{
                // do nothing
            });
        }
    }

    
    public void LoadScene(Scene scene, Action onFinish){
        if(!LoadingWindow.i.isLoading){
            LoadingWindow.i.StartLoading();
        }
        StartCoroutine(LoadAsynSceneCoroutine(scene.ToString(), onFinish)); 
    }

    IEnumerator LoadAsynSceneCoroutine(string sceneName, Action onFinish) {

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        operation.allowSceneActivation = false;

        while(operation.isDone){
            
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        LoadingWindow.i.EndLoading(()=>{
            operation.allowSceneActivation = true;
        });
        yield return null;
    }


    public void ToLogin(){
        LoadScene(Scene.login, doNothing);
    }

    public void ToMain(){
        LoadScene(Scene.Main, doNothing);
    }

    
    public void ToSignIn(){
        LoadScene(Scene.signin,doNothing);
    }

    public void doNothing(){}
}
