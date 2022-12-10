using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using UnityEngine.Networking;


public class username : MonoBehaviour
{
    public Text name;
    
    void Start()
    {
        
        name.text = GameManager.i.GetUser().GetName();
    }

}
