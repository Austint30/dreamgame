using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FramerateLimiter : MonoBehaviour
{
    public bool enable = true;
    public int fps = 60;
    // Start is called before the first frame update
    void Awake()
    {
        if (enable){
            QualitySettings.vSyncCount = 0;  // VSync must be disabled
            Application.targetFrameRate = fps;
        }
        else {
            QualitySettings.vSyncCount = 1;
            Application.targetFrameRate = -1;
        }
    }
}
