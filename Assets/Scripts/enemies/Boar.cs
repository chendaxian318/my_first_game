using UnityEngine;

public class Boar :enemy
{
    public float waitIdleTime;
    public float currentTime;

    protected override void Awake()
    {
        physicsCheck = GetComponent<PhysicsCheck>();
        base.Awake();
        isWalk = true;
    }
    protected override void Update()
    {
        base.Update();
        setAnimation();
        countIdleTime();
    }
    protected override void move()
    {
        base.move();
        if ((((physicsCheck.faceDir > 0 && physicsCheck.isRightWall)|| (physicsCheck.faceDir < 0 && physicsCheck.isLeftWall))||!physicsCheck.isGround)&&!isIdle)
        {
            Debug.Log("test");
            
            getIdle();
        }
    }
    private void setAnimation()
    {
        //    public bool isIdle;
        //public bool isRun;
        //public bool isWalk;
        anim.SetBool("isIdle", isIdle);
        anim.SetBool("isRun", isRun);
        anim.SetBool("isWalk", isWalk);
    }
    private void countIdleTime()
    {
        if (isIdle)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                Debug.Log("test");
                
                isIdle = false;

                isWalk = true;
                //最后转身
                physicsCheck.faceDir = -physicsCheck.faceDir;
                transform.localScale = new Vector3((float)-physicsCheck.faceDir, 1, 1);
                normalSpeed = originalSpeed;
            }
        }
    }
    private void getIdle()
    {
        isWalk = false;
        isIdle = true;
        originalSpeed = normalSpeed;
        normalSpeed = 0;
        //设定开始计时
        currentTime = waitIdleTime;
    }
}
