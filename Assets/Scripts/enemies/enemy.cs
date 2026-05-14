using UnityEngine;

public class enemy : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator anim;
    protected PhysicsCheck physicsCheck;
    [Header("运动参数")]
    public float normalSpeed;//当前速度
    public float runSpeed;//奔跑速度
    public float originalSpeed;//原来的速度
    public bool isIdle;
    public bool isRun;
    public bool isWalk;

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    protected virtual void Update()
    {
        move();
    }
    protected virtual void move()
    {
        rb.linearVelocityX = (float)physicsCheck.faceDir * normalSpeed;
    }


}
