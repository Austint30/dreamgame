using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climb : MonoBehaviour
{
    public Player playerScript;
    private Rigidbody2D playerRb;
    private bool dontReattachAutomatically = false;

    void Start(){
        playerRb = playerScript.GetComponent<Rigidbody2D>();
    }

    void OnTriggerStay2D(Collider2D other){
        if (!other.isTrigger || other.gameObject.tag != "Climbable") return;

        // Attach player to ladder when pressing up
        if (playerScript.climbing == false && Input.GetAxisRaw("Vertical") > 0){
            playerScript.climbing = true;
        }

        // Attach to ladder when falling
        else if (playerScript.climbing == false && playerRb != null && playerRb.velocity.y < 0 && !dontReattachAutomatically){
            playerScript.climbing = true;
        }

        // Dismount from ladder when down arrow is pressed and player is jumping
        else if (playerScript.climbing == true && Input.GetButtonDown("Jump") && Input.GetAxisRaw("Vertical") < 0){
            playerScript.climbing = false;
            dontReattachAutomatically = true;
        }
        
    }

    void OnTriggerExit2D(Collider2D other){
        if (other.gameObject.tag == "Climbable"){
            playerScript.climbing = false;
            dontReattachAutomatically = false;
        }
    }
}
