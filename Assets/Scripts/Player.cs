using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _horizontalInput;
    [SerializeField]
    private float _verticalForce;

    public bool isGrounded = false;
    private bool canDoubleJump;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        _horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(new Vector3(_horizontalInput, 0, 0) * _speed * Time.deltaTime);
    }
    void Jump()
    {
        if (isGrounded)
        {
            canDoubleJump = true;
        }
        if (Input.GetButtonDown("Jump"))
        //if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            if (isGrounded)
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, _verticalForce), ForceMode2D.Impulse);
            }

            else
            {
                if (canDoubleJump)
                {
                    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, _verticalForce), ForceMode2D.Impulse);
                    canDoubleJump = false;
                }
            }
        }
    }
}
