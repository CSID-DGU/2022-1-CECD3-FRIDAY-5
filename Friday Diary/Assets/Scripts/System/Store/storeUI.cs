using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class storeUI : MonoBehaviour
{
    public GameObject happyPanel;
    public GameObject sadPanel;
    public GameObject angryPanel;
    public GameObject disgustPanel;
    public GameObject fearPanel;
    public GameObject surprisePanel;
    public Image happyBTN, sadBTN, angryBTN, disgustBTN, fearBTN, surpriseBTN;

    public GameObject curActivePanel;
    public Color color ;

    public void Start()
    {
        curActivePanel = happyPanel;
    }

    public void OnHappyBtnClick(){
        if(happyPanel!=null && curActivePanel != happyPanel){   
            curActivePanel.SetActive(false);     
            happyPanel.SetActive(true);
            curActivePanel = happyPanel;
        }

        color = happyBTN.color;
        color.a = 1f;
        happyBTN.color = color;

        setSad();
        setAngry();
        setDisgust();
        setFear();
        setSurprise();
    }

    public void OnSadBtnClick(){
        if(sadPanel!=null && curActivePanel != sadPanel){   
            curActivePanel.SetActive(false);     
            sadPanel.SetActive(true);
            curActivePanel = sadPanel;
        }

        color = sadBTN.color;
        color.a = 1f;
        sadBTN.color = color;

        setHappy();
        setAngry();
        setDisgust();
        setFear();
        setSurprise();
    }

    public void OnAngryBtnClick(){
        if(angryPanel!=null && curActivePanel != angryPanel){     
            curActivePanel.SetActive(false);    
            angryPanel.SetActive(true);
            curActivePanel = angryPanel;
        }
        
        color = angryBTN.color;
        color.a = 1f;
        angryBTN.color = color;

        setHappy();
        setSad();
        setDisgust();
        setFear();
        setSurprise();
    }

    public void OnDisgustBtnClick(){
        if(disgustPanel!=null && curActivePanel != disgustPanel){        
            curActivePanel.SetActive(false); 
            disgustPanel.SetActive(true);
            curActivePanel = disgustPanel;
        }

        color = disgustBTN.color;
        color.a = 1f;
        disgustBTN.color = color;

        setHappy();
        setSad();
        setAngry();
        setFear();
        setSurprise();
    }

    public void OnFearBtnClick(){
        if(fearPanel!=null && curActivePanel != fearPanel){        
            curActivePanel.SetActive(false); 
            fearPanel.SetActive(true);
            curActivePanel = fearPanel;
        }

        color = fearBTN.color;
        color.a = 1f;
        fearBTN.color = color;

        setHappy();
        setSad();
        setAngry();
        setDisgust();
        setSurprise();
    }

    public void OnSurpriseBtnClick(){
        if(surprisePanel!=null && curActivePanel != surprisePanel){        
            curActivePanel.SetActive(false); 
            surprisePanel.SetActive(true);
            curActivePanel = surprisePanel;
        }

        color = surpriseBTN.color;
        color.a = 1f;
        surpriseBTN.color = color;

        setHappy();
        setSad();
        setAngry();
        setDisgust();
        setFear();
    }
    
    public void setHappy(){       
        color = happyBTN.color;
        color.a = 0.5f;
        happyBTN.color = color;
    }

    public void setSad(){
        color = sadBTN.color;
        color.a = 0.5f;
        sadBTN.color = color;
    }

    public void setAngry(){
        color = angryBTN.color;
        color.a = 0.5f;
        angryBTN.color = color;
    }

    public void setDisgust(){
        color = disgustBTN.color;
        color.a = 0.5f;
        disgustBTN.color = color;
    }

    public void setFear(){
        color = fearBTN.color;
        color.a = 0.5f;
        fearBTN.color = color;
    }

    public void setSurprise(){
        color = surpriseBTN.color;
        color.a = 0.5f;
        surpriseBTN.color = color;
    }
}
