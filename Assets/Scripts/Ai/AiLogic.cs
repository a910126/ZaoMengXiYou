using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

/// <summary>
/// 状态的枚举
/// </summary>
public enum E_State
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
public class AiLogic 
{
    public MonsterObject Monster;  //得到Monster

    public E_State NowState=E_State.NULL;  //Ai状态

    //利用字典存储状态的逻辑
    private Dictionary<E_State,AiStateBase> stateDic=new Dictionary<E_State,AiStateBase>();

    public AiStateBase NowAiState;

    //public T_AI_Info aiInfo;
    public AiLogic(/*MonsterObject monster*/ Object obj)  //初始化将逻辑加入字典
    {
        if(obj is MonsterObject)
        {
            this.Monster = new MonsterObject();
            stateDic.Add(E_State.PATROL, new AiStatePatrol(this));
            stateDic.Add(E_State.MOVE, new AiStateMove(this));
            stateDic.Add(E_State.ATK, new AiStateAtk(this));
            ChangeState(E_State.PATROL);  //切换巡逻状态
        }
        
        else if(obj is BossObject)
        {
            this.Monster = obj as MonsterObject;
            stateDic.Add(E_State.PATROL, new AiStatePatrol(this));
            stateDic.Add(E_State.MOVE, new AiStateMove(this));
            stateDic.Add(E_State.ATK, new AiStateAtk(this));
            ChangeState(E_State.PATROL);  //切换巡逻状态
        }
    }
    public void UpdateState()  //循环状态
    {
        if (Monster.IsDead) return;
        NowAiState.UpdateAiState();
    }    
    public void ChangeState(E_State state)  //改变状态
    {        
        //先执行上一次状态的结束逻辑
        if(NowState!=E_State.NULL)
        stateDic[NowState].ExitAiState();
        //再改变状态 
        NowState=state;
        //再执行最新状态的开始逻辑
        stateDic[NowState].EnterAiState();
        NowAiState = stateDic[NowState];
    }
}
