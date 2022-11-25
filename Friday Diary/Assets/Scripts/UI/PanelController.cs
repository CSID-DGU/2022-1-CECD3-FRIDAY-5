using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 하단바 버튼 클릭에 따라 화면 switch
public class PanelController : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject writePanel;
    public GameObject calendarPanel;
    public GameObject statisticsPanel;
    public GameObject friendPanel;

    private GameObject curActivePanel;

    private void Start()
    {
        curActivePanel = mainPanel;
    }

    public void OnMainBtnClick(){
        if(mainPanel!=null && curActivePanel != mainPanel){   
            curActivePanel.SetActive(false);     
            mainPanel.SetActive(true);
            curActivePanel = mainPanel;
        }
    }
    public void OnWriteBtnClick(){
        if(writePanel!=null && curActivePanel != writePanel){   
            curActivePanel.SetActive(false);     
            writePanel.SetActive(true);
            curActivePanel = writePanel;
        }
    }

    public void OnCalendarBtnClick(){
        if(calendarPanel!=null && curActivePanel != calendarPanel){     
            curActivePanel.SetActive(false);    
            calendarPanel.SetActive(true);
            curActivePanel = calendarPanel;
        }
    }

    public void OnStatisticsBtnClick(){
        if(statisticsPanel!=null && curActivePanel != statisticsPanel){        
            curActivePanel.SetActive(false); 
            statisticsPanel.SetActive(true);
            curActivePanel = statisticsPanel;
        }
    }

    public void OnFriendBtnClick(){
        if(friendPanel!=null && curActivePanel != friendPanel){        
            curActivePanel.SetActive(false); 
            friendPanel.SetActive(true);
            curActivePanel = statisticsPanel;
        }
    }

}
