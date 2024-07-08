using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class AiStateBase 
{
    protected AiLogic Logic;

    protected float Distance;  //Player和Monster之间的距离

    protected bool IsWait = false;  //是否需要等待
    public AiStateBase(AiLogic logic)
    {
        this.Logic = logic;

        MonoMgr.GetInstance().AddUpdateListener(TurnAround);  //利用MonoMgr使用Update
    }

    public abstract void EnterAiState();  //刚进入处理逻辑
    public abstract void UpdateAiState();  //处理逻辑
    public abstract void ExitAiState();  //退出处理逻辑
    private void TurnAround()  //转身
    {
        if (Logic.Monster.NowDir.x > 0)  
            Logic.Monster.Sprite.flipX = false;
        else if(Logic.Monster.NowDir.x < 0)
            Logic.Monster.Sprite.flipX = true;
    }

    protected float GetDistance(float player, float monster)  //计算Monster与Player的距离
    {
        if ((player >= 0 && monster >= 0) || (player < 0 && monster < 0))
            Distance = Math.Abs(player - monster);
        else if ((player > 0 && monster < 0) || (monster > 0 && player < 0))
        {
            if (monster < 0)
                Distance = Math.Abs(monster) + player;
            else if (player < 0)
                Distance = Math.Abs(player) + monster;
        }
        return Distance;
    }
}
