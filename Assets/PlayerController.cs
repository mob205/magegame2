using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{
    [Header("General")]
    public LayerMask PlayerLayer;

    [Header("Vertical")]
    public float GroundDistance = .05f;
    public float GroundingForce = -1.5f;
    public float JumpBuffer = .2f;
    public float CoyoteTime = .15f;
    public float JumpPower = 36;
    public float FallAccel = 110;
    public float JumpEndEarlyGravMod = 3;
    public float TerminalVelocity = 40;

    [Header("Horizontal")]
    public float VertAccel = 120;
    public float MaxVertSpeed = 14;
    public float AirDecel = 110;
    public float GroundDecel = 60;
    
    private Rigidbody2D _rb;
    private BoxCollider2D _col;
    private FrameInput _frameInput;
    private Vector2 _frameVelocity; 

    bool _grounded;
    bool _coyoteable;
    bool _endedJumpEarly;
    bool _canProcessJump;
    bool _hasBufferedJump;

    float _frameUngrounded;
    float _jumpPressTime;
    float _time;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<BoxCollider2D>();

        Physics2D.queriesStartInColliders = false;
    }
    private void Update()
    {
        _time += Time.deltaTime;
        GatherInput();
    }
    private void FixedUpdate()
    {
        CheckGrounded();

        ProcessJump();
        ProcessHorizontal();
        HandleGravity();

        _rb.velocity = _frameVelocity;
        
    }
    private void ProcessHorizontal()
    {
        // No movement - slow down
        if(_frameInput.Move.x == 0)
        {
            var decel = _grounded ? GroundDecel : AirDecel;
            _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, 0, decel * Time.fixedDeltaTime);
        }
        else
        {
            _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, _frameInput.Move.x * MaxVertSpeed, VertAccel * Time.fixedDeltaTime);
        }
    }
    private void GatherInput()
    {
        _frameInput = new FrameInput
        {
            JumpDown = Input.GetButtonDown("Jump"),
            JumpHeld = Input.GetButton("Jump"),
            Move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"))
        };

        if (_frameInput.JumpDown)
        {
            _canProcessJump = true;
            _jumpPressTime = _time;
        }
    }
    // Checks for ground collision
    private void CheckGrounded()
    {
        //bool groundHit = Physics2D.CapsuleCast(_col.bounds.center, _col.size, CapsuleDirection2D.Vertical, 0, Vector2.down, GroundDistance, ~PlayerLayer);
        //bool ceilingHit = Physics2D.CapsuleCast(_col.bounds.center, _col.size, CapsuleDirection2D.Horizontal, 0, Vector2.up, GroundDistance, ~PlayerLayer);
        bool groundHit = Physics2D.BoxCast(_col.bounds.center, _col.size, 0, Vector2.down, GroundDistance, ~PlayerLayer);
        bool ceilingHit = Physics2D.BoxCast(_col.bounds.center, _col.size, 0, Vector2.up, GroundDistance, ~PlayerLayer);

        // Hit a ceiling
        if (ceilingHit) _frameVelocity.y = Mathf.Min(0, _frameVelocity.y);
        // Landed
        if(!_grounded && groundHit)
        {
            _grounded = true;
            _coyoteable = true;
            _endedJumpEarly = false;
        } 
        // Left ground
        else if(_grounded && !groundHit)
        {
            _grounded = false;
            _frameUngrounded = _time;
        }
    }
    // Checks if a jump is possible
    private void ProcessJump()
    {
        // Release space bar early to stop the jump early
        if (!_endedJumpEarly && !_grounded && !_frameInput.JumpHeld && _rb.velocity.y > 0) _endedJumpEarly = true;
        // Check if the player can do a buffered jump
        if (!_canProcessJump && !(_hasBufferedJump && _time < _jumpPressTime + JumpBuffer)) return;

        if (_grounded || (_coyoteable && !_grounded && _time < _frameUngrounded + CoyoteTime)) ExecuteJump();

        _canProcessJump = false;
    }
    // Executes the jump
    private void ExecuteJump()
    {
        Debug.Log("Jumping!");
        _endedJumpEarly = false;
        _jumpPressTime = 0;
        _hasBufferedJump = false;
        _coyoteable = false;
        _frameVelocity.y = JumpPower;
    }
    private void HandleGravity()
    {
        if (_grounded && _frameVelocity.y <= 0f)
        {
            _frameVelocity.y = GroundingForce;
        }
        else
        {
            var inAirGravity = FallAccel;
            if (_endedJumpEarly && _frameVelocity.y > 0) inAirGravity *= JumpEndEarlyGravMod;
            _frameVelocity.y = Mathf.MoveTowards(_frameVelocity.y, -TerminalVelocity, inAirGravity * Time.fixedDeltaTime);
        }
    }
    public struct FrameInput
    {
        public bool JumpDown;
        public bool JumpHeld;
        public Vector2 Move;
    }
}
