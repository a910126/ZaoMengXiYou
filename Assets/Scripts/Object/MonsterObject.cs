using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterObject : ObjectBase
{
    /// <summary>
    /// Monster属性
    /// </summary>
    public ShuXing_Monster MonsterShuXing;

    /// <summary>
    /// Ai逻辑
    /// </summary>
    private AiLogic Ai;

    /// <summary>
    /// 是否受伤
    /// </summary>
    private bool IsHurt;

    protected override void Awake()
    {
        base.Awake();
        //NowDir = Vector2.right;
        InitShuXing();  //初始化属性
        Ai = new AiLogic(this);  //实例化
    }

    protected override void Update()
    {
        base.Update();

        if (IsHurt)
            MonsterShuXing.Speed=0;
        else
            MonsterShuXing.Speed = 1;
        if (Ai != null)
            Ai.UpdateState();  //执行Ai

        
    }
    public override void StandBy()
    {
        Animator.SetBool("IsWalk", false);  //切动画
    }

    public override void Atk()  //攻击
    {
        print("Monster Atk");
        Animator.SetBool("IsAtk1", true);
    }

    private void AfterAtk()  //攻击结束后
    {
        Animator.SetBool("IsAtk1", false);
        StandBy();
    }
    public void Move()
    {
        Animator.SetBool("IsWalk", true);  //切动画
    }

    public override void InitShuXing()  //初始化属性
    {
        ShuXing = new ShuXing_Monster(1);
        MonsterShuXing = ShuXing as ShuXing_Monster;
    }

    public override void Hurt(float value)
    {
        IsHurt = true;
        Animator.SetBool("IsHurt",true);  //切动画
    }

    private void AfterHurn()  //受伤结束后
    {
        IsHurt=false;
        Animator.SetBool("IsHurt", false);
    }

    public override void Dead()
    {
        
    }

   
}
