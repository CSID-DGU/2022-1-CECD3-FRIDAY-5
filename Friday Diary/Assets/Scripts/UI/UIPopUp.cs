using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIPopUp : MonoBehaviour
{
    public static UIPopUp i;
    
    GameObject popup;

    [SerializeField]
    GameObject prefab;

    Text title;

    Text content;

    private void Awake() {
        if(i==null) i = this;
    }

    private void Start() {
        if(popup==null){
            Vector3 originalScale = prefab.transform.localScale;
            popup = Instantiate(prefab, new Vector3(0f,0f,0f), Quaternion.identity, GameObject.Find("Canvas").transform);
            popup.transform.localScale = originalScale;
            popup.transform.localPosition = new Vector3(0f,0f,0f);

            title = popup.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();
            content = popup.transform.GetChild(0).GetChild(1).GetComponent<Text>();

            popup.SetActive(false);
        }
    }

    public void SetText(string title, string text){
        this.title.text = title;
        this.content.text = text;
    }

    public void Show(){
        popup?.SetActive(true);
    }

    public void Close(){
        popup?.SetActive(false);
    }
}
