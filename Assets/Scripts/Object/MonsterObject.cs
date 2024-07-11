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
        if (Ai != null)
            Ai.UpdateState();  //执行Ai
    }
    public override void StandBy()
    {
        Animator.SetBool("IsWalk", false);  //切动画
    }

    public override void Atk()
    {
        print("Monster Atk");
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
        
    }

    public override void Dead()
    {
        
    }

   
}
