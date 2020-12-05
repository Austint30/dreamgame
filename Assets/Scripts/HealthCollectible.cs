using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public int addHealth = 1;
    private PlayerHealth playerHealth;

    void OnTriggerEnter2D(Collider2D collider){
        if (collider.tag != "Player") return;
        Debug.Log("Touched!");
        playerHealth = collider.GetComponentInParent<PlayerHealth>();
        if (!playerHealth) return;
        playerHealth.Heal(addHealth);
        Destroy(gameObject);
    }
}
