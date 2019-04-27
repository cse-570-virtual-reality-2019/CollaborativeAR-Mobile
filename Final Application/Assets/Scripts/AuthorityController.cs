using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AuthorityController : NetworkBehaviour
{
    public GameObject cubePrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public bool IsLocalPlayer()
    {
        Debug.Log(this.gameObject.tag);
        return isLocalPlayer;
    }

    public void Spawn()
    {
        if (isLocalPlayer)
        {
            Debug.Log("Is Local Player");
            CmdSpawner();
        }
        Debug.Log("Not Local");
    }

    [Command]
    void CmdSpawner()
    {
        Debug.Log("Spawning");
        GameObject cube =
            (GameObject) Instantiate(cubePrefab, cubePrefab.transform.position, cubePrefab.transform.rotation);
        NetworkServer.SpawnWithClientAuthority(cube, this.gameObject);
        Debug.Log("Spawned Object");
    }
}
