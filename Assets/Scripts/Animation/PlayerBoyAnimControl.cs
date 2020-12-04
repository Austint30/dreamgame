using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoyAnimControl : MonoBehaviour
{
    public Animator playerAnimator;
    public Player playerMovement;
    public AnimationCurve walkSpeedCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.5f, 1, 1.3f, 1.3f), new Keyframe(1, 1.5f));

    [Tooltip("Transform that contains all character visual elements. This transform will be flipped right and left depending on which direction the player is moving.")]
    public Transform playerSpritesContainer;
    
    private bool lastMovDirLeft = false;
    private bool hasJumped = true;
    private bool isFalling = false; // TODO: To be implemented with dedicated falling animation
    private bool isMoving = false;
    private Vector3 lastPos;
    
    private void FixedUpdate() {
        isMoving = transform.position != lastPos && Mathf.Abs(playerMovement.horizontalInput) > 0;
        lastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerMovement) return;
        // Walking on ground
        if (playerMovement.isGrounded && !playerMovement.isJumping){
            float walkSpeed = Mathf.Abs(playerMovement.horizontalInput);
            walkSpeed = walkSpeedCurve.Evaluate(walkSpeed);
            if (!isMoving){
                walkSpeed = 0;
            }
            else{
                SoundHub.PlaySound(SoundHub.Sound.PlayerMove);
            }
            playerAnimator.SetFloat("WalkSpeed", walkSpeed);
        }

        playerAnimator.SetBool("Climbing", playerMovement.climbing);
        playerAnimator.SetFloat("ClimbSpeed", playerMovement.movementInput.normalized.magnitude);

        if (playerMovement.isGrounded){
            hasJumped = false;
            isFalling = false;
        }

        if (playerMovement.isGrounded && playerMovement.isJumping){
            hasJumped = true;
        }

        playerAnimator.SetBool("Jumping", !playerMovement.isGrounded);

        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0){
            lastMovDirLeft = Input.GetAxisRaw("Horizontal") < 0;
        }

        if (playerSpritesContainer != null){
            if (!playerMovement.climbing){
                playerSpritesContainer.rotation = Quaternion.Euler(
                    playerSpritesContainer.rotation.x,
                    lastMovDirLeft ? 180 : 0,
                    playerSpritesContainer.rotation.z
                );
            }
            else
            {
                playerSpritesContainer.rotation = Quaternion.Euler(playerSpritesContainer.rotation.x, 0, playerSpritesContainer.rotation.z);
            }
        }

        
    }
}
