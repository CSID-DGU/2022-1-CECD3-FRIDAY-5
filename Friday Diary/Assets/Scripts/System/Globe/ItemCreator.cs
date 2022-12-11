using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCreator : MonoBehaviour
{
    public static ItemCreator i;
    public GameObject globe;

    public GameObject activeAlert;
    Bounds bounds;
    Vector3 xaxis= new Vector3(1,0,0);
    Vector3 yaxis = new Vector3(0,1,0);

    Vector3 standard = Vector3.zero;


    ItemInfo itemInfo;


    public bool isActive;

    public void ToggleActive()
    {
        isActive = isActive ?  false : true;

        if(isActive){
            activeAlert.SetActive(true);
            GlobeController.instance.ChangeMode(GlobeMode.Plant);
        }
    }

    public void SetItemInfo(ItemInfo item){
        this.itemInfo = item;
    }

    void Update()
    {
        if(isActive && Input.touchCount > 0 &&  Input.touches[0].phase == TouchPhase.Ended) 
        {
            if(GlobeController.instance.isDragging) return;

            if(Input.touches[0].phase != TouchPhase.Ended) return;
            Vector3 touchPos = Input.GetTouch(0).position;


            RaycastHit hitObj;
            Ray ray = Camera.main.ScreenPointToRay(touchPos);

            if(Physics.Raycast(ray, out hitObj, Mathf.Infinity))
            {
                Debug.DrawRay(ray.origin, hitObj.point-ray.origin, Color.red, 5f);
                ItemCreator.i.placeTree(hitObj.point);
            }
 
        }
    }

    private void Awake() {
        i = this;
        SetGlobeObjects(); 
    }
    
    // Update is called once per frame
    private void Start() {
        
        isActive = false;  
        
        
        // zaxis = globe.transform.position - cam.transform.position;
    }
    public void SetGlobeObjects(){
        GameManager.i.SetTreeList(()=>{
            if(GameManager.i.getTreeList()!=null){
                foreach(Tree t in GameManager.i.getTreeList().tree_list){
                    GameObject prefab = ItemManager.i.GetCollection(t.emotion).getPrefab(t.treeid);
                    placeTree(prefab,t.relativePos,t.rotate,t.scale);
                }
            }
        });
    }
    public void SwitchPlaceMode(ItemInfo info){
        // Debug.Log(obj.name + "을 샀어요 ");
        
        SetItemInfo(info);
        Invoke("ToggleActive",0.5f);
    }

    public void placeTree(GameObject item, Vector3 pos, Quaternion rotate, Vector3 scale){
        // pos += globe.transform.position;
        
        Vector3 originalScale = item.transform.localScale;

        Quaternion lookRotation = Quaternion.FromToRotation(Vector3.up, pos);

        GameObject clone = Instantiate(item, pos, lookRotation);   
        clone.transform.localScale = originalScale * 0.1f;
        // Debug.Log(clone.transform.rotation);
        clone.transform.parent = globe.transform;
        clone.transform.localPosition = pos;
    }

    public void placeTree(Vector3 pos){
            if(itemInfo != null){
                // bounds = item.GetComponent<MeshFilter>().sharedMesh.bounds;
                GameObject item = ItemManager.i.GetCollection(itemInfo.emotion).getPrefab(itemInfo.prefabName);
                Vector3 direction = (globe.transform.position- pos);
                pos += direction * (0.012f);
                // Debug.Log(globe.transform.localPosition);
                Quaternion lookRotation = Quaternion.FromToRotation(Vector3.up, -1f*direction);

                Vector3 originalScale = item.transform.localScale;
                GameObject clone = Instantiate(item, pos, lookRotation);    

                clone.transform.localScale = originalScale*0.1f;
                // Debug.Log(clone.transform.rotation);
                clone.transform.parent = globe.transform;

                Tree tree = new Tree();
                tree.positionx = clone.transform.localPosition.x;
                tree.positiony = clone.transform.localPosition.y;
                tree.positionz = clone.transform.localPosition.z;
                tree.relativePos = clone.transform.localPosition;
                tree.rotate = clone.transform.rotation;
                tree.scale = clone.transform.localScale;
                tree.emotion = itemInfo.emotion;
                tree.treeid = itemInfo.prefabName;
                GameManager.i.AddTree(tree);

                GlobeController.instance.ChangeMode(GlobeMode.View);

                // Debug.Log(JsonUtility.ToJson(tree, true));
                int remain = GameManager.i.GetUser().getPoint(itemInfo.emotion) - itemInfo.cost;
                GameManager.i.GetUser().setPoint(itemInfo.emotion, remain);
                Backend.i.CreateObject(itemInfo.emotion,itemInfo.cost,tree,(message)=>{
                    GameManager.i.UpdateUser();
                });
            }

        
            

            itemInfo = null;
            isActive = false;
            activeAlert.SetActive(false);
    }

    bool isWater(Vector3 pos)
    {
        RaycastHit hit;
        if (!Physics.Raycast(globe.transform.position, pos, out hit, 100f))
        {
            return false;
        }
            

        if(hit.transform.gameObject.tag=="water"){
            return true;
        }
        return false;
    }
}
