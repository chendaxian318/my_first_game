using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{

    private enemy enemy;
    [Header("状态参数")]
    public bool isGround = false;
    public bool isCrouch=false;
    public bool isLeftWall = false;
    public bool isRightWall = false;

    [Header("地面检测")]
    public float groundCheckRadius;//检测范围
    public LayerMask GroundLayer; //指定碰撞层级
    public Vector2 bottomOffset;
    public Vector2 leftOffset;
    public double faceDir;
    public Vector2 rightOffset; 
    private void Awake()
    {

        faceDir = -transform.localScale.x;
        Debug.Log(transform.localScale.x);
    }
    private void Check()
    {
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2((float)(bottomOffset.x * -faceDir),bottomOffset.y), groundCheckRadius, GroundLayer);
        isLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, groundCheckRadius, GroundLayer);
        isRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, groundCheckRadius, GroundLayer);
    }
    private void FixedUpdate()
    {
        Check();// 每tickle检测各种状态
    }

    private void OnDrawGizmos()
    {
        //设置颜色 碰撞为绿，未碰撞为红
        Gizmos.color = isGround ? Color.yellow : Color.green;

        // 绘制一个实心圆，方便看范围
        Gizmos.DrawWireSphere((Vector2)transform.position + new Vector2((float)(bottomOffset.x * -faceDir), bottomOffset.y), groundCheckRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, groundCheckRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, groundCheckRadius);
    }
}
