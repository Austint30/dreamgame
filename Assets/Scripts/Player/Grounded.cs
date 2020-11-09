using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : MonoBehaviour
{
    public GameObject Player;
    private Player playerScript;

    // Start is called before the first frame update
    void Start()
    {
       playerScript = Player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        playerScript.groundObj = collider.gameObject;
        if (collider.tag == "Ground")
        {
            playerScript.isGrounded = true;
        }
        if (collider.tag == "Platform")
        {
            playerScript.isGrounded = true;
            Player.transform.parent = collider.gameObject.transform;
        }
    }


    void OnTriggerExit2D(Collider2D collider)
    {
        playerScript.groundObj = collider.gameObject;
        if (collider.tag == "Ground")
        {
            playerScript.isGrounded = false;
        }
        if(collider.tag == "Platform")
        {
            Platform platformScript = collider.gameObject.GetComponent<Platform>();
            Rigidbody2D playerRb = Player.GetComponent<Rigidbody2D>();
            playerScript.isGrounded = false;
            Player.transform.parent = null;
        }
    }
}
