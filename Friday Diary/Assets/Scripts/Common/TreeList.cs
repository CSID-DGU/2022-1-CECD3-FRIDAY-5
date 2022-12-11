using UnityEngine;
using System;
using System.Collections.Generic;


[System.Serializable]
public class TreeList {
    public List<Tree> tree_list = new List<Tree>();

    public List<Tree> getTreeList() => tree_list;
}



[System.Serializable]
public class Tree {
    public string id;
    public double positionx;
    public double positiony;
    public double positionz;

    public Vector3 relativePos;
    public Quaternion rotate;
    public Vector3 scale;
    public string treeid;
    public string emotion;

    public Tree(){

    }

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

