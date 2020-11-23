using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : Portal
{
    public string restrictToTag = "Player"; // Restrict portal access to gameobjects with a certain tag. Otherwise all rigidbody GameObjects will be transferred through.
    [Tooltip("Position the player/object on the floor after travelling through.")]
    public bool positionOnFloor = true;

    protected override void OnLevelLoaded(GameObject passingObject, Portal otherPortal){
        this.otherPortal = otherPortal;
        // Move player to floor
        if (positionOnFloor){
            MoveToFloor(passingObject);
        }
    }
    
    void OnTriggerStay2D(Collider2D other){
        if (
            (restrictToTag.Length > 0 && restrictToTag == other.tag) ||
            (restrictToTag.Length <= 0 && other.GetComponent<Rigidbody>())
        ){
            if (Input.GetButtonDown("Interact")){
                Trigger(other.gameObject);
            }
        }
    }

    void MoveToFloor(GameObject passingObject){
        if (!otherPortal) return;
        Vector3 otherSpawnPos = otherPortal.spawnPosition;
        Vector2 offset = Vector3.zero;
        RaycastHit2D hit = Physics2D.Raycast(otherSpawnPos, Vector2.down, 10f);
        if (hit){
            Collider2D objCollider = passingObject.GetComponent<Collider2D>();
            if (objCollider){
                offset = Vector3.up * (objCollider.bounds.extents.y - 0.05f); // Subtracting by 0.05f fixes the issue where the character slightly falls by a pixel after being placed on the ground.
            }
            passingObject.transform.position = hit.point + offset;
        }
    }
}
