using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemManager : MonoBehaviour {
    [SerializeField]
    List<ItemPrefabCollection> emotionItems;


    public static ItemManager i;

    private void Awake() {
        if(i==null) i=this;
        foreach(ItemPrefabCollection c in emotionItems){
            c.Init();
        }   
    }

    public ItemPrefabCollection GetCollection(Emotions em){
        return emotionItems[(int)em];
    }

    public ItemPrefabCollection GetCollection(string em){
        Debug.Log((int)Enum.Parse(typeof(Emotions),em));
        return emotionItems[(int)Enum.Parse(typeof(Emotions),em)];
    }

    public GameObject GetPrefab(string treename){
        foreach(ItemPrefabCollection c in emotionItems){
            foreach(GameObject prefab in c.prefabs){
                if(prefab.name == treename){
                    return prefab;
                }
            }
        }
        return null;
    }
}