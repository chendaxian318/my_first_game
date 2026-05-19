using UnityEngine;

public class BoarChaseState : BaseState
{
    public override void LogicUpdate()
    {
        currentEnemy.move();
        currentEnemy.t1 = currentEnemy.transform.position.x;
        if ((((currentEnemy.physicsCheck.faceDir > 0 && currentEnemy.physicsCheck.isRightWall)
            || (currentEnemy.physicsCheck.faceDir < 0 && currentEnemy.physicsCheck.isLeftWall))
            || !currentEnemy.physicsCheck.isGround) && !currentEnemy.isIdle)
        {
            currentEnemy.GetIdle();
            currentEnemy.SwitchState(NPCState.Patrol);
        }
    }

    public override void OnEnter(enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.normalSpeed = currentEnemy.runSpeed;
        currentEnemy.isRun = true; ;
        currentEnemy.isIdle = false;
        currentEnemy.isWalk = false;
    }

    public override void OnExit()
    {
    }

    public override void PhysicsUpdate()
    {
    }
}
