using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimationSet : MonoBehaviour
{
    private Animator anim;
    private PlayControl playControl;
    private Character character;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playControl = GetComponent<PlayControl>();
        character = GetComponent<Character>();
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
        anim.SetBool("isAttack", playControl.isAttack);
    }
    public void TakeDamage(Transform transform)
    {

        anim.SetTrigger("isHurt");
    }

    public void TakeDead(object character)
    {
        playControl.rb.linearVelocity =new Vector2(0, playControl.rb.linearVelocity.y);
        playControl.isDead = true;
        playControl.playerInput.enabled = false;
        anim.SetBool("isDead", true);
    }
    
}
