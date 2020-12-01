using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPowerup : MonoBehaviour
{
    [Tooltip("The sprite used when the player hasn't collected the powerup yet. Powerup icon is made invisible upon initialization.")]
    public SpriteRenderer powerupIcon;
    public virtual void Initialize(){
        Animator animator = powerupIcon.GetComponent<Animator>();
        if (powerupIcon != null && powerupIcon.transform.GetComponent<AbstractPowerup>() == null){ // Ensure we are not disabling the gameobject that the powerup script is connected to.
            powerupIcon.gameObject.SetActive(false); // Disable sprite visible when not collected by player.
        }
    }
}
