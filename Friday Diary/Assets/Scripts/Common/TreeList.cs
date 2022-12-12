using UnityEngine;
using System;
using System.Collections.Generic;


[Serializable]
public class TreeList {
    public List<Tree> tree_list;

}



[Serializable]
public class Tree {
    public string treeid;
    public string treename;
    public string id;
    public double positionx;
    public double positiony;
    public double positionz;
    


    /*
    public Tree(double x, double y, double z, string treeid){
        id = GameManager.i.GetUser().GetId();
        positionx = x;
        positiony = y;
        positionz = z;
        this.treeid = treeid;
    }

    public double getx()=>positionx;

    public double gety()=>positiony;

    public double getz()=>positionz;
    */
}

