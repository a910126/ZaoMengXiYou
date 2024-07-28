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

    /// <summary>
    /// Atk3特效的位置
    /// </summary>
    private Transform Atk2EffPos;

    /// <summary>
    /// Atk3特效的位置
    /// </summary>
    private Transform Atk3EffPos;

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

        Atk2EffPos = this.transform.Find("Atk2EffPos");
        Atk3EffPos = this.transform.Find("Atk3EffPos");
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
                PoolMgr.GetInstance().GetObj("Eff/Boss/Boss3/", "Atk2Eff", (obj) => {
                    obj.transform.SetParent(Atk3EffPos, false);
                });
                break;
            case 2:
                Animator.SetBool("IsAtk3", true);
                PoolMgr.GetInstance().GetObj("Eff/Boss/Boss3/", "Atk3Eff", (obj) => {
                    obj.transform.SetParent(Atk3EffPos,false);
                });
                break;
        }
    }

    private void AfterAtk()  //攻击结束后
    {
        if (Animator.GetCurrentAnimatorStateInfo(0).IsName("ani_Boss3_Atk1"))
        {
            Animator.SetBool("IsAtk1", false);
        }
        else if (Animator.GetCurrentAnimatorStateInfo(0).IsName("ani_Boss3_Atk2"))
        {
            Animator.SetBool("IsAtk2", false);
            GameObject obj = transform.Find("Atk2EffPos/" + "Atk2Eff").gameObject;
            PoolMgr.GetInstance().PushObj("Atk2Eff", obj);
        }
        else if (Animator.GetCurrentAnimatorStateInfo(0).IsName("ani_Boss3_Atk3"))
        {
            Animator.SetBool("IsAtk3", false);
            GameObject obj = transform.Find("Atk3EffPos/" + "Atk3Eff").gameObject;
            PoolMgr.GetInstance().PushObj("Atk3Eff", obj);
        }
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
