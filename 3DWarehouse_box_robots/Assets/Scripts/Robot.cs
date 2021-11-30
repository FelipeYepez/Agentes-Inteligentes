using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Robot : MonoBehaviour
{
    private Material robotMat;
    GameObject child;
    void Start() {
        child = gameObject.transform.GetChild(0).gameObject;
        robotMat = child.GetComponent<MeshRenderer>().material;
    }
    void OnTriggerEnter(Collider collider){
        if(collider.gameObject.tag == "Robot"){
            robotMat.SetFloat("_emissiveIntensity", 0.2f);
            robotMat.SetColor("_baseColor", Color.green);
            robotMat.SetFloat("_baseColorBlendIntensity", 0.5f);
        }
        else if(collider.gameObject.tag == "Box"){
            Destroy(collider.gameObject);
        }
    }
}