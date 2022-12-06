using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCreator : MonoBehaviour
{
    public static ItemCreator i;
    public GameObject globe;
    public Camera cam;
    public GameObject activeAlert;
    Bounds bounds;
    Vector3 xaxis= new Vector3(1,0,0);
    Vector3 yaxis = new Vector3(0,1,0);
    Vector3 zaxis;

    GameObject item;

    public bool isActive;

    public void ToggleActive()
    {
        isActive = isActive ?  false : true;

        if(isActive){
            activeAlert.SetActive(true);
        }
    }

    public void SetItem(GameObject item){
        this.item = item;
    }

    void Update()
    {
        if(isActive && Input.touchCount > 0 &&  Input.touches[0].phase != TouchPhase.Moved) 
        {
            if(Input.touches[0].phase != TouchPhase.Ended) return;
            Vector3 touchPos = Input.GetTouch(0).position;


            RaycastHit hitObj;
            Ray ray = Camera.main.ScreenPointToRay(touchPos);

            if(Physics.Raycast(ray, out hitObj, Mathf.Infinity))
            {
                 		
                Debug.DrawRay(ray.origin, hitObj.point, Color.red, 5f);
		        Debug.Log(hitObj.point);
                ItemCreator.i.placeTree(hitObj.point);
            }

        }
    }

    
    // Update is called once per frame
    private void Start() {
        i = this;
        isActive = false;   
        
        zaxis = globe.transform.position - cam.transform.position;
    }

    public void SwitchPlaceMode(GameObject obj){
        Debug.Log(obj.name + "을 샀어요 ");
        SetItem(obj);
        ToggleActive();

    }

    public void placeTree(Vector3 pos){
            if(item != null){
                bounds = item.GetComponent<MeshFilter>().sharedMesh.bounds;
                Vector3 direction = (pos-globe.transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(direction);

                
                if(!isWater(pos.normalized)){
                    pos += direction * (bounds.extents.z * 0.01f);
                    GameObject clone = Instantiate(item, pos.normalized, lookRotation);
                    clone.transform.parent = globe.transform;
                }
            }

            item = null;
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
