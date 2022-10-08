using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndRotate : MonoBehaviour
{
    Vector3 mPrevPos = Vector3.zero;
    Vector3 mPosDelta = Vector3.zero;

    void Update()
    {
        if(Input.mousePosition.y >= 140f &&  Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Globe")
                {
                    mPosDelta = Input.mousePosition - mPrevPos;
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

        mPrevPos = Input.mousePosition;

    }
}
