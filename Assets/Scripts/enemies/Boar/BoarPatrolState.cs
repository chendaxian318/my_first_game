using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class BoarPatrolState : BaseState
{
    public override void LogicUpdate()
    {
        //TODO:·¢ÏÖplayerÇÐ»»µ½chase 
        if (currentEnemy.FindPlay())
        {
            currentEnemy.SwitchState(NPCState.Chase);
            
        }

        currentEnemy.move();
        currentEnemy.t1 = currentEnemy.transform.position.x;
        if ((((currentEnemy.physicsCheck.faceDir > 0 && currentEnemy.physicsCheck.isRightWall) 
            || (currentEnemy.physicsCheck.faceDir < 0 && currentEnemy.physicsCheck.isLeftWall)) 
            || !currentEnemy.physicsCheck.isGround) && !currentEnemy.isIdle)
        {
            currentEnemy.GetIdle();
        }
    }

    public override void OnEnter(enemy enemy)
    {
        currentEnemy = enemy;
    }

    public override void OnExit()
    {
    }

    public override void PhysicsUpdate()
    {

    }
}
