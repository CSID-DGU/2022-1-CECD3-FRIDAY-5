using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyboardHandler : MonoBehaviour
{


    [SerializeField]
    TextMeshProUGUI printInput;

    private void Update() {
        
    }

    public void Show(GameObject inputField){
        InputField input =inputField.GetComponent<InputField>();
        if(input.contentType==InputField.ContentType.Password){
            HidePrev();
            if(input.text.Length > printInput.text.Length)
                addText(input.text[printInput.text.Length]);
            else{
                deleteText();
            }

        } else if(input.contentType==InputField.ContentType.EmailAddress){
            updateText(input.text);
        }
    }
    
    public void init(GameObject inputField){
        InputField input =inputField.GetComponent<InputField>();
        printInput.text = input.text;

        string hidden = "";
        if(input.contentType==InputField.ContentType.Password){
           for(int i=0;i<printInput.text.Length;i++){
                hidden += "*";
        }
            printInput.text = hidden;
        } 
    }

    public void updateText(string str){
        printInput.text = str;
    }

    public void addText(char c){
        printInput.text += c;
    }

    public void deleteText(){
        if(printInput.text.Length >0)
            printInput.text= printInput.text.Remove(printInput.text.Length-1);
    }


    public  void HidePrev(){
        string hidden = "";
        for(int i=0;i<printInput.text.Length;i++){
            hidden += "*";
        }
        printInput.text = hidden;
    }



}
