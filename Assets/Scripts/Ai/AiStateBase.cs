using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class AiStateBase 
{
    protected AiLogic Logic;  

    private float MonsterSpeed;  //Monster的速度

    protected float Distance;  //Player和Monster之间的距离

    protected bool IsWait = false;  //是否需要等待

    protected ShuXing_Monster MonsterShuXing;  //Monster属性
    public AiStateBase(AiLogic logic)
    {
        this.Logic = logic;

        MonsterShuXing = logic.Monster.MonsterShuXing;

        MonoMgr.GetInstance().AddUpdateListener(TurnAround);  //利用MonoMgr使用Update

        MonsterSpeed=MonsterShuXing.Speed;
    }

    public abstract void EnterAiState();  //刚进入处理逻辑
    public abstract void UpdateAiState();  //处理逻辑
    public abstract void ExitAiState();  //退出处理逻辑

    private void StopMove()  //停止Move
    {
        MonsterShuXing.Speed = 0;
    }

    private void StartMove()  //开始Move
    {
        MonsterShuXing.Speed=MonsterSpeed;  
    }
    private void TurnAround()  //转身
    {
        if (Logic.Monster.NowDir.x > 0)  
            Logic.Monster.Sprite.flipX = true;
        else if(Logic.Monster.NowDir.x < 0)
            Logic.Monster.Sprite.flipX = false;
    }
    protected float GetDistance(float pos, float monster)  //计算Monster与Player或TargetPos的距离
    {
        if ((pos >= 0 && monster >= 0) || (pos < 0 && monster < 0))
            Distance = Math.Abs(pos - monster);
        else if ((pos > 0 && monster < 0) || (monster > 0 && pos < 0))
        {
            if (monster < 0)
                Distance = Math.Abs(monster) + pos;
            else if (pos < 0)
                Distance = Math.Abs(pos) + monster;
        }
        return Distance;
    }
}
