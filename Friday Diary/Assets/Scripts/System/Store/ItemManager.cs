using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {
    [SerializeField]
    List<ItemPrefabCollection> emotionItems;

    public static ItemManager i;

    private void Start() {
        if(i==null) i=this;
        foreach(ItemPrefabCollection c in emotionItems){
            c.Init();
        }   
    }

    public ItemPrefabCollection GetCollection(Emotions em){
        return emotionItems[(int)em];
    }
}