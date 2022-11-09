using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GlobeMode{
    View = 0,
    Plant        
}
public class GlobeController : MonoBehaviour
{
    public static GlobeController instance;
    GlobeMode currentMode;

    void Start()
    {   
        instance = this;
        currentMode = GlobeMode.View;
    }

    void Update()
    {   
        if(currentMode == GlobeMode.View)
        {
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Moved)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
                RaycastHit hit;
    
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.tag == "Globe")
                    {
                        Vector3 mPosDelta = Input.GetTouch(0).deltaPosition;
                        if(Vector3.Dot(transform.up, Vector3.up)>=0)
                        {
                            transform.Rotate(transform.up, -Vector3.Dot(mPosDelta, Camera.main.transform.right)*0.05f,Space.World);
                        }
                        else
                        {
                            transform.Rotate(transform.up, Vector3.Dot(mPosDelta, Camera.main.transform.right)*0.05f,Space.World);
                        }
                        
                        transform.Rotate(Camera.main.transform.right, Vector3.Dot(mPosDelta, Camera.main.transform.up)*0.05f,Space.World);
                    }
                }
            }


        }
    }
}
