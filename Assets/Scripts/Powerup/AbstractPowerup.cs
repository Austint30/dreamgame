using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPowerup : MonoBehaviour
{
    [Tooltip("The sprite used when the player hasn't collected the powerup yet. Uncollected sprite is made invisible upon initialization.")]
    public SpriteRenderer uncollectedSprite;
    public virtual void Initialize(){
        if (uncollectedSprite != null){
            uncollectedSprite.enabled = false; // Disable sprite visible when not collected by player
        }
    }
}
