using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class setting : MonoBehaviour
{
   public GameObject notiActive, notiDisactive,loginActive,loginDisactive;


    public void NotificationActivate()
    {
        notiActive.SetActive(false);     
        notiDisactive.SetActive(true);
    }

    public void NotificationDisactivate()
    {
        notiDisactive.SetActive(false);
        notiActive.SetActive(true);     
    }
    

    public void LoginActivate()
    {
        loginActive.SetActive(false);     
        loginDisactive.SetActive(true);
    }

    public void LoginDisactivate()
    {
        loginDisactive.SetActive(false);
        loginActive.SetActive(true);  
    }   
}
