using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 7f;

    public bool isJumping {
        get {
            return _isJumping;
        }
    }

    public Vector2 lastObjectVelocity {
        get {
            return _lastObjectVelocity;
        }
    }

    public int currentJumps {
        get {
            return _currJumps;
        }
    }

    public float horizontalInput {
        get {
            return _horizontalInput;
        }
    }

    [SerializeField]
    private float _horizontalInput;
    [SerializeField]
    private float groundJumpHeight = 2f;
    [SerializeField]
    private float airJumpHeight = 4f;
    [SerializeField]
    private float gravityScale = 3f;
    [SerializeField]
    private float terminalVelocity = 20f;
    public int maxJumps = 2;
    [System.NonSerialized]
    public bool disableInput = false;
    [System.NonSerialized]
    public GameObject groundObj;

    public bool isGrounded = false;

    private bool _isJumping = false;
    private Vector2 _lastObjectVelocity = Vector2.zero;
    private int _currJumps = 0;

    private Rigidbody2D _rb;
    private bool _hasDoubleJumped = false;
    private bool hasJumped = false;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = gravityScale;
        gameObject.tag = "Player";
    }

    // Update is called once per frame
    void Update()
    {
        if (!disableInput)
            HandleJumping();
        _horizontalInput = Input.GetAxis("Horizontal");

        //handle quit game at any point in the game, so as to not get stuck
        if(Input.GetKeyDown(KeyCode.Q)){
            Application.Quit();
        }
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
            else if (groundObj != null && groundObj.GetComponent<Rigidbody2D>()){ // TODO: Figure out a way not to call GetComponent every FixedUpdate
                Rigidbody2D groundRb = groundObj.GetComponent<Rigidbody2D>();
                _lastObjectVelocity = new Vector2(groundRb.velocity.x, groundRb.velocity.y);
            }

            // Ground is assumed to be static
            else
            {
                _lastObjectVelocity = Vector2.zero;
            }
        }
        if (!disableInput)
            HandleMoving();
        // TODO: Add terminal velocity
    }

    void HandleMoving(){
        Vector3 translation = new Vector3(_horizontalInput, 0, 0) * speed * Time.deltaTime;

        // TODO: Implement more stable horizontal movement on angles surfaces
        if (isGrounded){
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, 1f);
            Debug.DrawLine(transform.position, transform.position + Vector3.down * 1f);
            Quaternion hitAngle = Quaternion.FromToRotation(Vector3.forward, hit.normal);
            // Debug.Log(Mathf.Abs(Quaternion.Angle(hitAngle, Quaternion.Euler(0, 0, 0))));
            // if (Mathf.Abs(Quaternion.Angle(hitAngle, Quaternion.Euler(0, 0, 0)) <  ))
            translation = hitAngle * translation;
        }
        transform.Translate(translation);
    }

    void HandleJumping()
    {
        if (Input.GetButtonUp("Jump")){
            _isJumping = false;
        }
        if (Input.GetButtonDown("Jump"))
        {
            _isJumping = true;
            Jump();
        }
        else if (isGrounded && !_isJumping){
            _currJumps = 0;
            hasJumped = false;
        }
        if (!isGrounded && !_isJumping){ // Allows player to control height of jump
            if (_rb.velocity.y > 0){
                _rb.gravityScale = gravityScale * 4;
            }
            else
            {
                _rb.gravityScale = gravityScale;
            }
        }
    }

    void Jump(){
        _currJumps++;
        hasJumped = true;
        if (isGrounded){
            _rb.velocity = new Vector2(_lastObjectVelocity.x, _lastObjectVelocity.y + HeightToVelocity(groundJumpHeight));
        }
        // Air jumping cancels existing horizontal velocity in opposite direction
        //
        else if (_currJumps <= maxJumps && hasJumped)
        {
             float horizontalVel;

            // If horizontalInput and rigidbody velocity are the same sign, horizontal velocity is preserved. Otherwise velocity is cancelled.
            // If player jumps off of a fast moving platform and wants to move the opposite direction in the air, this will make it possible upon
            // double-jumping to move the opposite direction of the motion of the platform.
            horizontalVel = ((_horizontalInput >= 0) ^ (_rb.velocity.x < 0)) ? _rb.velocity.x : 0f;
            _rb.velocity = new Vector2(horizontalVel, _lastObjectVelocity.y + HeightToVelocity(airJumpHeight));
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
