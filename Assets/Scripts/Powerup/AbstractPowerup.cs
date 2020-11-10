using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPowerup : MonoBehaviour
{
    [Tooltip("The sprite used when the player hasn't collected the powerup yet. Uncollected sprite is made invisible upon initialization.")]
    public SpriteRenderer uncollectedSprite;
    public virtual void Initialize(){
        Animator animator = uncollectedSprite.GetComponent<Animator>();
        if (uncollectedSprite != null && uncollectedSprite.transform.GetComponent<AbstractPowerup>() == null){ // Ensure we are not disabling the gameobject that the powerup script is connected to.
            uncollectedSprite.gameObject.SetActive(false); // Disable sprite visible when not collected by player.
        }
    }
}
