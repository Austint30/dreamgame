using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public int subtractHealth = 1;
    public bool instantKill = false;

    [Header("Knockback")]
    public bool knockPlayerBack = true;
    public float knockBackStrength = 3f;

    private PlayerHealth playerHealth;

    void Start(){}
    
    void OnCollisionEnter2D(Collision2D collision){
        Debug.Log("Touched!");
        HandleTouch(collision);
    }

    void OnTriggerEnter2D(Collider2D collider){
        Debug.Log("Touched!");
        HandleTouch(collider);
    }

    void HandleTouch(Collision2D collision){
        playerHealth = collision.gameObject.GetComponentInParent<PlayerHealth>();
        if (!playerHealth) return;
        if (knockPlayerBack){
            playerHealth.DamageAndKnockback(instantKill ? int.MaxValue : subtractHealth, collision.contacts[0].normal, knockBackStrength);
        }
        else
        {
            playerHealth.Damage(instantKill ? int.MaxValue : subtractHealth);
        }
    }

    void HandleTouch(Collider2D collider){
        playerHealth = collider.GetComponentInParent<PlayerHealth>();
        if (!playerHealth) return;
        if (knockPlayerBack){
            Vector2 knockbackDirection = (collider.transform.position - transform.position).normalized;
            playerHealth.DamageAndKnockback(instantKill ? int.MaxValue : subtractHealth, knockbackDirection, knockBackStrength);
        }
        else
        {
            playerHealth.Damage(instantKill ? int.MaxValue : subtractHealth);
        }
    }

}
