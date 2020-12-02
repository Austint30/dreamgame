using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisibility : MonoBehaviour
{
    
    BoxCollider2D box;
    SpriteRenderer rend;

    // Start is called before the first frame update
    void Start()
    {
        box = this.gameObject.GetComponent<BoxCollider2D>();
        rend = this.gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            box.enabled = !box.enabled;
            rend.enabled = !rend.enabled;
           

        }
    }
}
