using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climb : MonoBehaviour
{
    public Player playerScript;

    void OnTriggerStay2D(Collider2D other){
        if (other.isTrigger && playerScript.climbing == false && other.gameObject.tag == "Climbable" && Input.GetAxisRaw("Vertical") > 0){
            playerScript.climbing = true;
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if (other.gameObject.tag == "Climbable"){
            playerScript.climbing = false;
        }
    }
}
