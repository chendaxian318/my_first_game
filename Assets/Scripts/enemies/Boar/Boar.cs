using UnityEngine;

public class Boar :enemy
{
    public float waitIdleTime;
    public float currentTime;

    protected override void Awake()
    {
        base.Awake();
        patrolState = new BoarPatrolState();
        chaseState = new BoarChaseState();
    }
    protected override void Update()
    {
        base.Update();
        //每个敌人的计时部分应该是不同的
        CountIdleTime();
    }
    public override void move()
    {;
        if (isHurt) return;
        rb.linearVelocityX = (float)physicsCheck.faceDir * normalSpeed;

    }
    
    protected  override void CountIdleTime()
    {
        if (isIdle)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                isIdle = false;
                isRun = false;
                isWalk = true;
                //最后转身
                physicsCheck.faceDir = -physicsCheck.faceDir;
                transform.localScale = new Vector3((float)-physicsCheck.faceDir, 1, 1);
                normalSpeed = walkSpeed;
            }
        }
    }
    public override void GetIdle()
    {
        isWalk = false;
        isIdle = true;
        isRun = false;
        normalSpeed = 0;
        //设定开始计时
        currentTime = waitIdleTime;
    }

    public override bool FindPlay()
    {
        
        return Physics2D.OverlapBox(transform.position + (Vector3)centerOffset+new Vector3(patrolDistance*physicsCheck.faceDir,0,0), new(patrolRatation.x+ patrolDistance, patrolRatation.y), 0,LayerMask.GetMask("player"));
    }

}
