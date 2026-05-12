using NUnit.Framework.Constraints;
using System;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayControl : MonoBehaviour
{
    public Rigidbody2D rb;
    public PlayerInput playerInput;

    public PhysicsCheck physicsCheck;
    float originSpeed;
    public CapsuleCollider2D capsuleCollider;
    private Vector2 originCapsualeSize;
    private Vector2 originCapsualeOffset;

    [Header("速度参数")]
    public Vector2 moveInput;
    public bool isRun;
    public bool isHurt;
    public bool isDead;
    public float moveSpeed;
    public float jumpForce;
    public float hurtForce;
    int direction;
    public double VelocityY;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        physicsCheck = GetComponent<PhysicsCheck>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    private void OnEnable()
    {
        playerInput.actions["GamePlay/Move"].performed += OnMovePerformed;
        playerInput.actions["GamePlay/Move"].canceled += OnMoveCanceled;


        playerInput.actions["GamePlay/Jump"].performed += OnJumpPerformed;
        playerInput.actions["GamePlay/Jump"].canceled += OnJumpCanceled;


        playerInput.actions["GamePlay/Run_Walk"].performed += OnRun_WalkPerformed;
        playerInput.actions["GamePlay/Run_Walk"].canceled += OnRun_WalkCanceled;

        playerInput.actions["GamePlay/Crouch"].canceled += OnCrouchCanceled;
        playerInput.actions["GamePlay/Crouch"].performed += OnCrouchPerformed;
    }

    private void OnCrouchPerformed(InputAction.CallbackContext context)
    {
        originSpeed = moveSpeed;
        originCapsualeOffset = capsuleCollider.offset;
        originCapsualeSize = capsuleCollider.size;
        if (physicsCheck.isGround)
        {
            moveSpeed = 0f;
            physicsCheck.isCrouch = true;
            rb.linearVelocityX = 0;
            capsuleCollider.offset = new Vector2(capsuleCollider.offset.x, 0.9f);
            capsuleCollider.size = new Vector2(capsuleCollider.size.x, 1.75f);
        }

    }

    private void OnCrouchCanceled(InputAction.CallbackContext context)
    {
        physicsCheck.isCrouch = false;
        moveSpeed = originSpeed;
        capsuleCollider.offset = originCapsualeOffset;
        capsuleCollider.size = originCapsualeSize;
    }

    private void OnRun_WalkCanceled(InputAction.CallbackContext context)
    {
    }

    private void OnRun_WalkPerformed(InputAction.CallbackContext context)
    {
        if (isRun)
        {
            isRun = false;
            moveSpeed /= 2;
        }
        else {
            isRun = true;
            moveSpeed *= 2;
        }
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        if (physicsCheck.isGround) {
            rb.linearVelocityY = jumpForce;
        }
    }
    private void OnJumpCanceled(InputAction.CallbackContext context)
    {
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        direction = (int)moveInput.x;
        //死亡后不能转向
            transform.localScale = new Vector3(direction, transform.localScale.y, transform.localScale.z);

    }
    private void OnMoveCanceled(InputAction.CallbackContext context)
    {

        moveInput = Vector2.zero;
    }

    private void Update()
    {
    }
    void FixedUpdate()
    {
        //Velocity is objection speed
        if (!physicsCheck.isCrouch&& !isHurt)
            rb.linearVelocityX = moveInput.x * moveSpeed;
    }

    private void OnDisable()
    {
        playerInput.actions["GamePlay/Move"].performed -= OnMovePerformed;
        playerInput.actions["GamePlay/Move"].canceled -= OnMoveCanceled;
        playerInput.actions["GamePlay/Jump"].performed -= OnJumpPerformed;
        playerInput.actions["GamePlay/Jump"].canceled -= OnJumpCanceled;
    }

    
    public void GetHurt(Transform AttackTransform)
    {
        Vector2 dir = new Vector2 ((transform.position.x - AttackTransform.position.x),0).normalized;
        isHurt = true;
        rb.linearVelocity = Vector2.zero;

        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
    }
}
