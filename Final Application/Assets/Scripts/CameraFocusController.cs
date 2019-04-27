using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class CameraFocusController : MonoBehaviour
{
    // Start is called before the first frame update
    private bool mVuforiaStarted = false;
    private bool mFlashEnabled = false;
    
    void Start()
    {
        VuforiaARController vuforia = VuforiaARController.Instance;
        if (vuforia != null)
            vuforia.RegisterVuforiaStartedCallback(StartAfterVuforia);
    }

    private void StartAfterVuforia()
    {
        mVuforiaStarted = true;
        SetAutoFocus();
    }

    private void OnApplicationPause(bool pause)
    {
        if (!pause)
        {
            if (mVuforiaStarted)
            {
                SetAutoFocus();
            }
        }
    }

    private void SetAutoFocus()
    {
        if (CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO))
        {
            Debug.Log("Autofocus set");
        }
        else
        {
            Debug.Log("Device doesnt support autofocus");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
