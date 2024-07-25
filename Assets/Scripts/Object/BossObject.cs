using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossObject : ObjectBase
{
    /// <summary>
    /// Monster属性
    /// </summary>
    public ShuXing_Monster MonsterShuXing;

    /// <summary>
    /// Ai逻辑
    /// </summary>
    private AiBossLogic Ai;

    /// <summary>
    /// 是否受伤
    /// </summary>
    private bool IsHurt;

    /// <summary>
    /// 攻击方式的概率
    /// </summary>
    private int[] ProbabilityArray;

    protected override void Awake()
    {
        base.Awake();
        //NowDir = Vector2.right;
        InitShuXing();  //初始化属性
        Ai = new AiBossLogic(this);  //实例化
    }

    protected override void Update()
    {
        base.Update();

        if (IsHurt)
            MonsterShuXing.Speed = 0;
        else
            MonsterShuXing.Speed = 1;
        
        if (Ai != null)
            Ai.UpdateState();  //执行Ai
    }

    public override void InitShuXing()  //初始化属性
    {
        ShuXing = new ShuXing_Monster(1);
        MonsterShuXing = ShuXing as ShuXing_Monster;

        ProbabilityArray = new int[3] { 50, 25, 25 };
    }
    public override void StandBy()  //待机
    {
        Animator.SetBool("IsWalk", false);  //切动画
    }

    public void Move()  //移动
    {
        Animator.SetBool("IsWalk", true);
    }

    private int AtkProbability(int[] probabilityArray,int total)  //攻击概率
    {
        int r = Random.Range(0, total + 1);
        int t = 0;
        for(int i=0;i<probabilityArray.Length;i++)
        {
            t += probabilityArray[i];
            if(r<t)
            return i;
        }
        return 0;
    }
    public override void Atk()  //攻击
    {
        

        switch (AtkProbability(ProbabilityArray, 100))
        {
            case 0:
                Animator.SetBool("IsAtk1", true);
                print("Atk1");
                break;
            case 1:
                Animator.SetBool("IsAtk2", true);
                print("Atk2");
                break;
            case 2:
                Animator.SetBool("IsAtk3", true);
                print("Atk3");
                break;
        }
    }

    private void AfterAtk1()  //攻击结束后
    {
        Animator.SetBool("IsAtk1", false);
        StandBy();
    }

    private void AfterAtk2()  //攻击结束后
    {
        Animator.SetBool("IsAtk2", false);
        StandBy();
    }
    private void AfterAtk3()  //攻击结束后
    {
        Animator.SetBool("IsAtk3", false);
        StandBy();
    }


    public override void Hurt(float value)   //受伤
    {
        IsHurt = true;
        Animator.SetBool("IsHurt", true);  //切动画
    }

    private void AfterHurn()  //受伤结束后
    {
        IsHurt = false;
        Animator.SetBool("IsHurt", false);
    }

    public override void Dead()  //死亡
    {

    }


}
