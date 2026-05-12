using UnityEngine;
public class Character : MonoBehaviour
{
    public int healthAll;
    public int healthCurrent;
    private Animator ani;
    [Header("计时器")]
    public double timeCount;
    public bool invulnerableDamage = false;
    public double invulnearbleTime;
    private void Awake()
    {
        healthCurrent = healthAll;
        ani=GetComponent<Animator>();
    }
    public void TakeDamage(Attack attack)
    {
        if (invulnerableDamage)
            return;
        if(healthCurrent - attack.damage > 0)
        {
            //执行受伤操作
            hurtCalculate(attack);

        }
        else
        {
            healthCurrent = 0;
            //执行死亡操作
        }
    }
    private void Update()
    {
        invulnerableTimeUpdate();
    }

    //受伤伤害计算
    private void hurtCalculate(Attack attack)
    {
        invulnerableDamage = true;
        invulnearbleTime = timeCount;
        healthCurrent -= attack.damage;
        ani.SetTrigger("isHurt");
    }

    //无敌时间更新
    private void invulnerableTimeUpdate()
    {
        if (invulnearbleTime <= 0)
        {
            invulnerableDamage = false;
        }
        else
        {
            invulnearbleTime -= Time.deltaTime;
        }
    }
   
}
