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

    /// <summary>
    /// �Ƿ�����
    /// </summary>
    private bool IsHurt;

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

        if (IsHurt)
            MonsterShuXing.Speed=0;
        else
            MonsterShuXing.Speed = 1;
        if (Ai != null)
            Ai.UpdateState();  //ִ��Ai

        
    }
    public override void StandBy()
    {
        Animator.SetBool("IsWalk", false);  //�ж���
    }

    public override void Atk()  //����
    {
        print("Monster Atk");
        Animator.SetBool("IsAtk1", true);
    }

    private void AfterAtk()  //����������
    {
        Animator.SetBool("IsAtk1", false);
        StandBy();
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
        IsHurt = true;
        Animator.SetBool("IsHurt",true);  //�ж���
    }

    private void AfterHurn()  //���˽�����
    {
        IsHurt=false;
        Animator.SetBool("IsHurt", false);
    }

    public override void Dead()
    {
        
    }

   
}
