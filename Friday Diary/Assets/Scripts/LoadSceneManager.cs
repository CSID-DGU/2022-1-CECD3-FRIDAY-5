using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum Scene{
    Login,
    Main,
    SignIn
}
public class LoadSceneManager : MonoBehaviour
{
    public static LoadSceneManager i;

    private void Start() {
        if(i==null) i=this;
    }
    public void LoadScene(Scene scene){
        StartCoroutine(LoadAsynSceneCoroutine(scene.ToString())); 
    }

    IEnumerator LoadAsynSceneCoroutine(string sceneName) {

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        operation.allowSceneActivation = false;

        LoadingWindow.i.StartLoading();
        while(operation.isDone){
            
            yield return null;
        }
        
        LoadingWindow.i.EndLoading();
        operation.allowSceneActivation = true;
    }


    public void ToLogin(){
        LoadScene(Scene.Login);
    }

    public void ToMain(){
        LoadScene(Scene.Main);
    }

    
    public void ToSignIn(){
        LoadScene(Scene.SignIn);
    }
}
