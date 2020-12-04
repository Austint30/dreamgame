using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ParallaxSprite : MonoBehaviour
{
    public Vector2 parallaxSpeed = new Vector2(0.5f, 0.5f);
    public bool centerToCamera = false;
    private Vector3 initOffset;
    // Start is called before the first frame update
    void Start()
    {
        initOffset = transform.position - Camera.main.transform.position;
        if (centerToCamera){
            initOffset = Vector3.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = (Camera.main.transform.position + initOffset) * parallaxSpeed;
    }
}
