using System.Collections;
using UnityEngine;


public class enemy : MonoBehaviour
{
    [HideInInspector] protected Rigidbody2D rb;
    [HideInInspector] protected Animator anim;
    public PhysicsCheck physicsCheck;
    [Header("运动参数")]
    public float normalSpeed;//当前速度
    public float runSpeed;//奔跑速度
    public float originalSpeed;//原来的速度
    public bool isIdle;
    public bool isRun;
    public bool isWalk;
    public bool isHurt;
    public bool isDead;
    public float hurtForce;

    private BaseState currentSate;
    protected BaseState patrolState;
    protected BaseState chaseState;

    [HideInInspector]public double  t1;
    [HideInInspector]public double t2;

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        isWalk = true;
    }


    private void OnEnable()
    {
        //初始状态
        currentSate = patrolState;
        currentSate?.OnEnter(this);
        
    }


    protected virtual void Update()
    {
        setAnimation();
        currentSate?.LogicUpdate();
        
    }
    private void FixedUpdate()
    {
        currentSate?.PhysicsUpdate();
    }
    #region 事件执行方法
    private void OnDisable()
    {
        currentSate?.OnExit();
    }
    public virtual void move() { }

    public virtual void GetIdle() { }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
    public void OnTakeHurt(Transform attack)
    {
        StartCoroutine(HurtRoutine(attack));
    }
    protected IEnumerator HurtRoutine(Transform attack)
    {
        anim.SetTrigger("hurt");
        isHurt = true;
        physicsCheck.faceDir = (transform.position.x > attack.position.x) ? -1 : 1;
        rb.AddForce(new Vector2(-hurtForce* (float)physicsCheck.faceDir, 0), ForceMode2D.Impulse);
        transform.localScale = new Vector3((float)-physicsCheck.faceDir, 1, 1);
        yield return new WaitForSeconds(0.45f);
        isWalk = false;
        isRun = true;
        normalSpeed = runSpeed;
        isHurt  = false;
    }
    public void OnTakeDead()
    {

        anim.SetTrigger("dead");
    }
    public void setAnimation()
    {

        anim.SetBool("isIdle", isIdle);
        anim.SetBool("isRun", isRun);
        anim.SetBool("isWalk", isWalk);
    }
    #endregion

    protected virtual void countIdleTime() { }

}
