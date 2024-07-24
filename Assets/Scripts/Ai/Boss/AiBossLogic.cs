using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

/// <summary>
/// ״̬��ö��
/// </summary>
public enum E_BossState
{
    NULL,
    /// <summary>
    /// Ѳ��״̬
    /// </summary>
    PATROL,
    /// <summary>
    /// �ƶ�״̬
    /// </summary>
    MOVE,
    /// <summary>
    /// ����״̬
    /// </summary>
    ATK,
    /// <summary>
    /// ����Χ���س�����
    /// </summary>
    BACK,
}
public class AiBossLogic
{
    public BossObject Boss;  //�õ�Monster

    public E_BossState NowState = E_BossState.NULL;  //Ai״̬

    //�����ֵ�洢״̬���߼�
    private Dictionary<E_BossState, AiBossStateBase> stateDic = new Dictionary<E_BossState, AiBossStateBase>();

    public AiBossStateBase NowAiState;

    //public T_AI_Info aiInfo;
    public AiBossLogic(BossObject boss)  //��ʼ�����߼������ֵ�
    {
        this.Boss = boss;
        stateDic.Add(E_BossState.MOVE, new AiBossStateMove(this));
        stateDic.Add(E_BossState.ATK, new AiBossStateAtk(this));
        ChangeState(E_BossState.PATROL);  //�л�Ѳ��״̬
    }
    public void UpdateState()  //ѭ��״̬
    {
        if (Boss.IsDead) return;
        NowAiState.UpdateAiState();
    }
    public void ChangeState(E_BossState state)  //�ı�״̬
    {
        //��ִ����һ��״̬�Ľ����߼�
        if (NowState != E_BossState.NULL)
            stateDic[NowState].ExitAiState();
        //�ٸı�״̬ 
        NowState = state;
        //��ִ������״̬�Ŀ�ʼ�߼�
        stateDic[NowState].EnterAiState();
        NowAiState = stateDic[NowState];
    }
}
