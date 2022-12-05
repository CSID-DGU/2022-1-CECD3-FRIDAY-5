using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using UnityEngine.SceneManagement;

public class account : MonoBehaviour
{
    public GameObject accountPopUp, deletePopUp; 

    public void onclickAccountBTN()
    {
        accountPopUp.SetActive(true);
    }

    public void onclickYesBTN()
    {
        Backend.i.DeleteUser( onDeletesuccess );
    }

    public void onclickNoBTN()
    {
        accountPopUp.SetActive(false);
    }

    public void onDeletesuccess ( string x )
    { 
        accountPopUp.SetActive(false);
        deletePopUp.SetActive(true);
    }

    public void onclickConfirmBTN ()
    {
        SceneManager.LoadScene("login");
    }
}
