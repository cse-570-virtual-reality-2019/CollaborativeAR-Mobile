using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Movement : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().material.color = Color.black;
    }
    
    public override void OnStartLocalPlayer()
    {
        
    }

    // Update is called once per frame

    public bool IsLocalPlayer(GameObject gameObject)
    {
        Debug.Log(this.gameObject.tag);
        return hasAuthority && gameObject == this.gameObject;
    }
    
    public void Translate()
    {
        
    }

    public void Rotate()
    {
        
    }

    public void Scale()
    {
        
    }
    
    public void Select()
    {
        
    }
    
    
    void Update()
    {
        
    }
}
