using UnityEngine;

public class PlayerAnimationSet : MonoBehaviour
{
    private Animator anim;
    private PlayControl playControl;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playControl = GetComponent<PlayControl>();
    }
    private void Update()
    {
        SetAnimation();
    }
    private void SetAnimation()
    {
        anim.SetFloat("Speed", Mathf.Abs(playControl.moveInput.x) * playControl.moveSpeed);
        anim.SetFloat("VelocityY", playControl.rb.linearVelocityY);
        anim.SetBool("isGround", playControl.physicsCheck.isGround);
        anim.SetBool("isCrouch", playControl.physicsCheck.isCrouch);
    }
}
