using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIPopUp : MonoBehaviour
{
    public static UIPopUp i;
    [SerializeField]
    GameObject popup;

    Text title;
    Text content;


    private void Start() {
        if(i==null) i = this;

        title = popup.transform.GetChild(1).GetComponent<Text>();
        content = popup.transform.GetChild(2).GetComponent<Text>();
    }


    public void SetText(string title, string text){
        this.title.text = title;
        this.content.text = text;
    }

    public void Show(){
        popup?.SetActive(true);
    }

    public void OnBtnClick(){
        popup?.SetActive(false);
    }
}
