using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GlobeMode{
    View = 0,
    Touched = 1,
    Plant = 2  
}
public class GlobeController : MonoBehaviour
{
    public static GlobeController instance;
    GlobeMode currentMode;

    int nTouch = 0;

    // 지구 회전
    float spinSpeed = 10f;

    // zoom in & out
    float fieldOfView = 60f; 
    float prevDist = 0f;       
    float touchDist = 0f;
    

    void Start()
    {   
        instance = this;
        currentMode = GlobeMode.View;
    }

    void Update()
    {   
        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime, Space.World);
        
        if(currentMode == GlobeMode.View)
        {
            nTouch = Input.touchCount;

            // 터치가 두 개이고, 두 터치중 하나라도 이동한다면 카메라의 fieldOfView를 조정합니다.
            if (nTouch == 2 && (Input.touches[0].phase == TouchPhase.Moved || Input.touches[1].phase == TouchPhase.Moved))
            {

                float fDis = 0f;
                touchDist = (Input.touches[0].position - Input.touches[1].position).sqrMagnitude;
    
                fDis = (touchDist - prevDist) * 0.01f;
    
                // 이전 두 터치의 거리와 지금 두 터치의 거리의 차이를 FleldOfView를 차감합니다.
                fieldOfView -= fDis;
    
                // 최대는 100, 최소는 20으로 더이상 증가 혹은 감소가 되지 않도록 합니다.
                fieldOfView = Mathf.Clamp(fieldOfView, 20.0f, 100.0f);
    
                // 확대 / 축소가 갑자기 되지않도록 보간합니다.
                Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, fieldOfView, Time.deltaTime * 5);
    
                prevDist = touchDist;
            }
            else if (nTouch == 1 && Input.touches[0].phase == TouchPhase.Moved)
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
        else{
            
        }
    }
}
