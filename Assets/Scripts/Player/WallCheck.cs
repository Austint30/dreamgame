using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class WallCheck : MonoBehaviour
{
    public Player playerScript;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.isTrigger) return; // Don't react to other triggers
        playerScript.OnWallCheckTriggerEnter(other);
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.isTrigger) return; // Don't react to other triggers
        playerScript.OnWallCheckTriggerExit(other);
    }
}
