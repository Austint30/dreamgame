using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public Camera activeCamera;
    public bool autoDetectCamera = true;
    public bool wrapWestEast = true;

    public Vector2 scrollSpeedFactor = new Vector2(0.5f, 0.5f);

    private Vector2 cameraInitialOffset;
    private float initialOrthographicSize;
    private float orthographicDelta;

    // Start is called before the first frame update
    void Start()
    {
        if (autoDetectCamera){
            activeCamera = Camera.main;
        }

        // Find the offset relative to the camera.
        cameraInitialOffset = transform.position - activeCamera.transform.position;
        initialOrthographicSize = activeCamera.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {

        // Change background position depending on camera position
        transform.position = cameraInitialOffset + (activeCamera.transform.position * scrollSpeedFactor);

        // Keep background size in sync with orthographic size of camera
        orthographicDelta = activeCamera.orthographicSize - initialOrthographicSize + 1;
        transform.localScale = (Vector2.one * (activeCamera.orthographicSize - initialOrthographicSize + 1));
    }
}
