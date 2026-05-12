using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("攻击相关的属性")]
    public int damage;
    public double attackRage;


    private void OnTriggerStay2D(Collider2D other)
    {
        other.GetComponent<Character>()?.TakeDamage(this);
    }

}
