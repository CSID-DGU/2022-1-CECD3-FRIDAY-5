using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Diary
{
    public string ID;    
    public string Text;

    public Diary(){
        ID ="user";
        Text = "일기";
    }
    public Diary(string Id, string Text)
    {
        this.ID = Id;
        this.Text = Text;
    }
}
