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
    [SerializeField]
    private int maxJumps = 2;
    [SerializeField]
    private float jumpHeight = 2;

    public bool isGrounded = false;

    [System.NonSerialized]
    public GameObject groundObj;
    private Rigidbody2D _rb;
    private Vector2 _lastObjectVelocity = Vector2.zero;
    private int _currentJumps = 0;
    private bool isJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleJumping();
        _horizontalInput = Input.GetAxis("Horizontal");
    }

    void FixedUpdate(){
        if (isGrounded){
            Platform platformScript = groundObj.GetComponent<Platform>();

            // Match velocity of moving platforms
            if (platformScript != null){
                Debug.Log("Platform velocity: " + platformScript.velocity);
                _lastObjectVelocity = platformScript.velocity;
            }
            // Match the velocity of objects with rigidbodies
            else if (groundObj != null && groundObj.GetComponent<Rigidbody2D>()){ // TODO: Figure out a way not to call GetComponent every FiedUpdate
                Rigidbody2D groundRb = groundObj.GetComponent<Rigidbody2D>();
                _lastObjectVelocity = new Vector2(groundRb.velocity.x, groundRb.velocity.y);
            }

            // Ground is assumed to be static
            else
            {
                _lastObjectVelocity = Vector2.zero;
            }
        }
        transform.Translate(new Vector3(_horizontalInput, 0, 0) * _speed * Time.deltaTime);

    }

    void HandleJumping()
    {
        if (isGrounded)
        {
            if (!isJumping){
                _currentJumps = 0;
            }
            if (Input.GetButtonDown("Jump"))
                Jump();
            else if (Input.GetButtonUp("Jump")){
                isJumping = false;
                CancelJump();
            }
            isJumping = false;
        }
    }

    // Cancel a jump early while character is moving in the upward direction
    void CancelJump(){
        if (_rb.velocity.y > 0){
            _rb.velocity = new Vector2(_lastObjectVelocity.x, _lastObjectVelocity.y);
        }
    }

    void Jump(){
        isJumping = true;
        _currentJumps++;
        if (_currentJumps < maxJumps){
            _rb.velocity = new Vector2(_lastObjectVelocity.x, _lastObjectVelocity.y + HeightToVelocity(jumpHeight));
        }
    }

    // Calculates how strong the velocity has to be to reach a specific height
    // Allows game to change gravity without
    // Uses the kinematic formula vi = sqrt(2gy)
    private float HeightToVelocity(float height){
        float grav = Physics2D.gravity.y * _rb.gravityScale;
        float vel = Mathf.Sqrt(2 * Mathf.Abs(grav) * height);
        return vel;
    }
}
