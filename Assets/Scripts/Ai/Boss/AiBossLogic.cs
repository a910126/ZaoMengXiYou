using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

/// <summary>
/// 状态的枚举
/// </summary>
public enum E_BossState
{
    NULL,
    /// <summary>
    /// 巡逻状态
    /// </summary>
    PATROL,
    /// <summary>
    /// 移动状态
    /// </summary>
    MOVE,
    /// <summary>
    /// 攻击状态
    /// </summary>
    ATK,
    /// <summary>
    /// 出范围返回出生点
    /// </summary>
    BACK,
}
public class AiBossLogic
{
    public BossObject Boss;  //得到Monster

    public E_BossState NowState = E_BossState.NULL;  //Ai状态

    //利用字典存储状态的逻辑
    private Dictionary<E_BossState, AiBossStateBase> stateDic = new Dictionary<E_BossState, AiBossStateBase>();

    public AiBossStateBase NowAiState;

    //public T_AI_Info aiInfo;
    public AiBossLogic(BossObject boss)  //初始化将逻辑加入字典
    {
        this.Boss = boss;
        stateDic.Add(E_BossState.MOVE, new AiBossStateMove(this));
        stateDic.Add(E_BossState.ATK, new AiBossStateAtk(this));
        ChangeState(E_BossState.PATROL);  //切换巡逻状态
    }
    public void UpdateState()  //循环状态
    {
        if (Boss.IsDead) return;
        NowAiState.UpdateAiState();
    }
    public void ChangeState(E_BossState state)  //改变状态
    {
        //先执行上一次状态的结束逻辑
        if (NowState != E_BossState.NULL)
            stateDic[NowState].ExitAiState();
        //再改变状态 
        NowState = state;
        //再执行最新状态的开始逻辑
        stateDic[NowState].EnterAiState();
        NowAiState = stateDic[NowState];
    }
}
