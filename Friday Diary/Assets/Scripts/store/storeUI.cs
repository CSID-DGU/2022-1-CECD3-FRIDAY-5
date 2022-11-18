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
    public Image bt;

    public GameObject curActivePanel;

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
    }

    public void OnSadBtnClick(){
        if(sadPanel!=null && curActivePanel != sadPanel){   
            curActivePanel.SetActive(false);     
            sadPanel.SetActive(true);
            curActivePanel = sadPanel;
        }
    }

    public void OnAngryBtnClick(){
        if(angryPanel!=null && curActivePanel != angryPanel){     
            curActivePanel.SetActive(false);    
            angryPanel.SetActive(true);
            curActivePanel = angryPanel;
        }
    }

    public void OnDisgustBtnClick(){
        if(disgustPanel!=null && curActivePanel != disgustPanel){        
            curActivePanel.SetActive(false); 
            disgustPanel.SetActive(true);
            curActivePanel = disgustPanel;
        }
    }

    public void OnFearBtnClick(){
        if(fearPanel!=null && curActivePanel != fearPanel){        
            curActivePanel.SetActive(false); 
            fearPanel.SetActive(true);
            curActivePanel = fearPanel;
        }
    }

    public void OnSurpriseBtnClick(){
        if(surprisePanel!=null && curActivePanel != surprisePanel){        
            curActivePanel.SetActive(false); 
            surprisePanel.SetActive(true);
            curActivePanel = surprisePanel;
        }
    }
    

}
