using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossObject : ObjectBase
{
    /// <summary>
    /// Monster����
    /// </summary>
    public ShuXing_Monster MonsterShuXing;

    /// <summary>
    /// Ai�߼�
    /// </summary>
    private AiBossLogic Ai;

    /// <summary>
    /// �Ƿ�����
    /// </summary>
    private bool IsHurt;

    /// <summary>
    /// ������ʽ�ĸ���
    /// </summary>
    private int[] ProbabilityArray;

    protected override void Awake()
    {
        base.Awake();
        //NowDir = Vector2.right;
        InitShuXing();  //��ʼ������
        Ai = new AiBossLogic(this);  //ʵ����
    }

    protected override void Update()
    {
        base.Update();

        if (IsHurt)
            MonsterShuXing.Speed = 0;
        else
            MonsterShuXing.Speed = 1;
        
        if (Ai != null)
            Ai.UpdateState();  //ִ��Ai
    }

    public override void InitShuXing()  //��ʼ������
    {
        ShuXing = new ShuXing_Monster(1);
        MonsterShuXing = ShuXing as ShuXing_Monster;

        ProbabilityArray = new int[3] { 50, 25, 25 };
    }
    public override void StandBy()  //����
    {
        Animator.SetBool("IsWalk", false);  //�ж���
    }

    public void Move()  //�ƶ�
    {
        Animator.SetBool("IsWalk", true);
    }

    private int AtkProbability(int[] probabilityArray,int total)  //��������
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
    public override void Atk()  //����
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

    private void AfterAtk1()  //����������
    {
        Animator.SetBool("IsAtk1", false);
        StandBy();
    }

    private void AfterAtk2()  //����������
    {
        Animator.SetBool("IsAtk2", false);
        StandBy();
    }
    private void AfterAtk3()  //����������
    {
        Animator.SetBool("IsAtk3", false);
        StandBy();
    }


    public override void Hurt(float value)   //����
    {
        IsHurt = true;
        Animator.SetBool("IsHurt", true);  //�ж���
    }

    private void AfterHurn()  //���˽�����
    {
        IsHurt = false;
        Animator.SetBool("IsHurt", false);
    }

    public override void Dead()  //����
    {

    }


}
