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
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.isGrounded && !playerMovement.isJumping){
            float walkSpeed = Mathf.Abs(playerMovement.horizontalInput);
            walkSpeed = walkSpeedCurve.Evaluate(walkSpeed);

            playerAnimator.SetFloat("WalkSpeed", walkSpeed);
        }

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
        playerSpritesContainer.rotation = Quaternion.Euler(
            playerSpritesContainer.rotation.x,
            lastMovDirLeft ? 180 : 0,
            playerSpritesContainer.rotation.z
        );
    }
}
