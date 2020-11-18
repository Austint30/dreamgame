using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Portal))]
public class DoorTrigger : MonoBehaviour
{
    private Portal portal;
    public string restrictToTag = "Player"; // Restrict portal access to gameobjects with a certain tag. Otherwise all rigidbody GameObjects will be transferred through.

    void Start(){
        portal = GetComponent<Portal>();
    }
    
    void OnTriggerStay2D(Collider2D other){
        if (
            (restrictToTag.Length > 0 && restrictToTag == other.tag) ||
            (restrictToTag.Length <= 0 && other.GetComponent<Rigidbody>())
        ){
            if (Input.GetButtonDown("Jump")){
                portal.Trigger(other.gameObject);
            }
        }
    }
}
