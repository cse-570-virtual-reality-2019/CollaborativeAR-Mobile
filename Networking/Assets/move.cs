using System.Collections;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class move : NetworkBehaviour
{

    public float horozontalSpeed = 0.01f;
    public float verticalSpeed = 0.01f;

    void Update()
    {
        if (!isLocalPlayer)
            return;
        if (Input.touchCount >= 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                float h = horozontalSpeed * touch.deltaPosition.x;
                float v = verticalSpeed * touch.deltaPosition.y;
                transform.Translate(h, 0, v);
            }
        }
    }
}
