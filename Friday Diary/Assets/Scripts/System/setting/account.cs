using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class account : MonoBehaviour
{
    public GameObject accountPopUp;

    public void onclickAccountBTN()
    {
        accountPopUp.SetActive(true);
    }

    public void onclickYesBTN()
    {
        // 계정 정보 삭제 
        // 메인씬으로 이동
    }

    public void onclickNoBTN()
    {
        accountPopUp.SetActive(false);
    }
}
