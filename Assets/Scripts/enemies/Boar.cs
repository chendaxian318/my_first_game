using UnityEngine;

public class Boar :enemy
{
    public float waitIdleTime;
    public float currentTime;

    protected override void Awake()
    {
        base.Awake();
        patrolState = new BoarPatrolState();
    }
    protected override void Update()
    {
        base.Update();
        //每个敌人的计时部分应该是不同的
        countIdleTime();
    }
    public override void move()
    {;
        if (isHurt) return;
        rb.linearVelocityX = (float)physicsCheck.faceDir * normalSpeed;

    }
    
    protected  override void countIdleTime()
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
                normalSpeed = originalSpeed;
            }
        }
    }
    public override void GetIdle()
    {
        isWalk = false;
        isIdle = true;
        isRun = false;
        originalSpeed = normalSpeed;
        normalSpeed = 0;
        //设定开始计时
        currentTime = waitIdleTime;
    }

}
