using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public bool isActive;
    void Start()
    {
        isActive = false;
        
    }

    public void onTriggerBtnClick()
    {
        isActive = isActive ?  false : true;
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
                PlaceTree.i.placeTree(hitObj.point);
            }

        }
        


    }
}
