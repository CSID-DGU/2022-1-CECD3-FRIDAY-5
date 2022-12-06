using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingWindow : MonoBehaviour
{
    GameObject loadPanel;
    Image imageBox;

    [SerializeField]
    GameObject prefab;

    [SerializeField]
    List<Sprite> images;

    public static LoadingWindow i;

    private void Awake() {
        if(i==null) i=this;
        InitLoadPanel();
    }


    public void StartLoading(){
        if(loadPanel==null){
            InitLoadPanel();
        }

        StartCoroutine(Loading());
    }

    public void InitLoadPanel(){
        Vector3 originalScale = prefab.transform.localScale;
        loadPanel = Instantiate(prefab, new Vector3(0f,0f,0f), Quaternion.identity, GameObject.Find("Canvas").transform);
        loadPanel.transform.localScale = originalScale;
        loadPanel.transform.localPosition = new Vector3(0f,0f,0f);

        imageBox = loadPanel.transform.GetChild(0).GetComponent<Image>();
        loadPanel.SetActive(false);
    }
    
    IEnumerator Loading(){
        loadPanel.SetActive(true);
        while(true){
        foreach(Sprite newSprite in images){
            // 이미지 바꾸고
            imageBox.sprite = newSprite;
            imageBox.color = new Color(1f,1f,1f,0f);
            Color fadecolor = imageBox.color;

            
            float start = 0f;
            float end = 1f;
            float time = 0f;
            float FadeTime = 0.7f;
            
            // 알파값 0->1
            while(fadecolor.a <1f ){
                time += Time.deltaTime / FadeTime;
                fadecolor .a = Mathf.Lerp(start, end, time);
                imageBox.color = fadecolor ;

                yield return null;
            }
            
            start = 1f;
            end = 0f;
            time = 0f;
            // 알파값 1->0
            while(fadecolor.a > 0f ){
                time += Time.deltaTime / FadeTime;
                fadecolor .a = Mathf.Lerp(start, end, time);
                imageBox.color = fadecolor ;

                yield return null;
            }
        }
        }
    }

    public void EndLoading(){
        StopCoroutine(Loading());
        loadPanel.SetActive(false);
    }
}
