using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UI.Image;

public class graph : MonoBehaviour
{
    public Image i0,i1,i2,i3,i4,i5;
    public Button click;
    float happy=40, sad=10, angry=20, disgust=10, fear=5, surprise=5, neutral=10;

    public void graphbutton()
    {

        i0.fillAmount = happy/100;
        i1.fillAmount = sad/100;
        i2.fillAmount = angry/100;
        i3.fillAmount = disgust/100;
        i4.fillAmount = fear/100;
        i5.fillAmount = surprise/100;
    } 

}
