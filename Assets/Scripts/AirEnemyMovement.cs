using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirEnemyMovement : Platform
{
    [Tooltip("The transform that contains the visual elements of the enemy that needs to be rotated back and forth depending on the movement direction.")]
    public Transform spriteContainer;

    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();

        // Rotate the roach in the direction of travel
        spriteContainer.rotation = Quaternion.Euler(spriteContainer.rotation.x, velocity.x >=0 ? 0 : 180, spriteContainer.rotation.z);

        //EnemySound.PlaySound(EnemySound.Sound.RoachSound, MainMenuButtonsScript.sfxVol);
    }
}
