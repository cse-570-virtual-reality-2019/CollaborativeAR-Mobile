using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject marker = GameObject.FindGameObjectWithTag("Marker");
        transform.SetParent(marker.transform, false);
        //GetComponent<MeshRenderer>().material.color = Color.red;
    }
}
