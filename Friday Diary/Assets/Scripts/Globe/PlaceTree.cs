using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceTree : MonoBehaviour
{
    public static PlaceTree i;
    public GameObject tree;
    public GameObject globe;
    public Camera cam;
    Bounds bounds;
    Vector3 xaxis= new Vector3(1,0,0);
    Vector3 yaxis = new Vector3(0,1,0);
    Vector3 zaxis;

    // Update is called once per frame
    private void Start() {
        i = this;
        bounds = tree.GetComponent<MeshFilter>().sharedMesh.bounds;
        zaxis = globe.transform.position - cam.transform.position;
    }


/*
    void placeTree(){
        // float radius = globe.transform.GetChild(0).GetComponent<SphereCollider>().radius;
        // float radius = globe.GetComponent<SphereCollider>().radius;
        // float scale = globe.transform.localScale.x;
        // radius *= scale;
        float radius = 0.7f;
        Debug.Log(radius);


        // 서울, 도쿄, 벤쿠버, 워싱턴, 멜버른
        float[,] locations = new float[5,2]
        {{37.5f,127.0f}, {35.68f,139.69f}, {49.3f,-123.1f}, {38.9f,-77.0f}, {-37.81f,144.96f}};
        string[] name = new string[5]
        {"Seoul","Tokyo","Vancouver", "Washington", "Melbourne"};

        for(int i=0;i<5;i++){
            float latitude = locations[i,0];
            float longitude = locations[i,1];

            float z = radius * Mathf.Cos(latitude);
            float x = radius * Mathf.Sin(latitude);
            float y = radius * Mathf.Sin(longitude);
           
            Vector3 position = new Vector3(x,y,z);

            Vector3 direction = (position-globe.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            
            if(!isWater(position.normalized)){
                position += direction * (bounds.extents.z * 0.01f);
                GameObject clone = Instantiate(tree, position.normalized, lookRotation);
                clone.gameObject.name = name[i];
            }
            
            //float temp = radius - bounds.extents.z;
            //if(temp < 0f) temp *= -1f;
            //position += (direction * temp);


            // clone.transform.localScale = new Vector3(0.1f,0.1f,0.1f); 
        }

    }
    */

    public void placeTree(Vector3 pos){

            Vector3 direction = (pos-globe.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            
            if(!isWater(pos.normalized)){
                pos += direction * (bounds.extents.z * 0.01f);
                GameObject clone = Instantiate(tree, pos.normalized, lookRotation);
                clone.transform.parent = globe.transform;
            }

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
