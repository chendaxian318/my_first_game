using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{

    [Header("状态参数")]
    public bool isGround = false;
    public bool isCrouch=false;

    [Header("地面检测")]
    public float groundCheckRadius;//检测范围
    public LayerMask GroundLayer; //指定碰撞层级
    public Vector2 offset;
    private void CheckGround()
    {
        Vector2 checkPos = transform.position;
        isGround = Physics2D.OverlapCircle(checkPos + offset, groundCheckRadius, GroundLayer);
    }
    private void FixedUpdate()
    {
        CheckGround();// 每tickle检测地面
    }

    private void OnDrawGizmos()
    {
        //设置颜色 碰撞为绿，未碰撞为红
        Gizmos.color = isGround ? Color.yellow : Color.green;

        // 绘制一个实心圆，方便看范围
        Vector2 checkPos = transform.position;
        Gizmos.DrawWireSphere(checkPos + offset, groundCheckRadius);
    }
}
