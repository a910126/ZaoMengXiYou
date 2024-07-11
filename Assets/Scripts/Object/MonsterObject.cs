using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterObject : ObjectBase
{
    /// <summary>
    /// Monster����
    /// </summary>
    public ShuXing_Monster MonsterShuXing;

    /// <summary>
    /// Ai�߼�
    /// </summary>
    private AiLogic Ai;

    protected override void Awake()
    {
        base.Awake();
        //NowDir = Vector2.right;
        InitShuXing();  //��ʼ������
        Ai = new AiLogic(this);  //ʵ����

    }

    protected override void Update()
    {
        base.Update();
        if (Ai != null)
            Ai.UpdateState();  //ִ��Ai
    }
    public override void StandBy()
    {
        Animator.SetBool("IsWalk", false);  //�ж���
    }

    public override void Atk()
    {
        print("Monster Atk");
    }

    public void Move()
    {
        Animator.SetBool("IsWalk", true);  //�ж���
    }

    public override void InitShuXing()  //��ʼ������
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
