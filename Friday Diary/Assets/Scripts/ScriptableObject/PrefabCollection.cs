using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[ CreateAssetMenu( fileName = "PrefabCollection", menuName = "Scriptable Object Asset/PrefabCollection" )]
public class PrefabCollection : ScriptableObject
{
    public string collectionName;
    public List<GameObject> prefabs;
    Dictionary<string,GameObject> prefabDict;
    Dictionary<string, Sprite> thumbnails;

    public void Init(){
        prefabDict = new Dictionary<string, GameObject>();

        foreach(GameObject p in prefabs){
            prefabDict.Add(p.name, p);
            Texture2D preview =AssetPreview.GetMiniThumbnail(p);

            thumbnails.Add(p.name, Sprite.Create(preview,new Rect(0, 0, preview.width, preview.height), new Vector2(0.5f, 0.5f)));
        }
    }

    public GameObject getPrefab(string key){
        if(prefabDict.ContainsKey(key)){
            return prefabDict[key];
        }

        return null;
    }

    public Sprite getThumbnail(string key){
        if(thumbnails.ContainsKey(key)){
            return thumbnails[key];
        }

        return null;
    }
}
