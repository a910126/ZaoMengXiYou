using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class AiStatePatrol : AiStateBase
{
    private int NowPointIndex = 0;  //当前位置的索引

    private float WaitTime;  //等待时间

    private float PatrolRange;  //巡逻范围

    private float VisionRange;  //视野范围，玩家进入这个范围 发现玩家 切换状态

    private Vector3 MonsterPos;  //Monster的BornPos

    private Vector3 TargetPos;  //记录目标点的位置

    private bool IsArrive = false;  //是否到达目标点

    private List<Vector2> Points= new List<Vector2>();  //通过点去巡逻，存储固定点的容器    
 
    public AiStatePatrol(AiLogic logic) : base(logic)
    {
        MonsterPos = MonsterShuXing.BornPos;

        PatrolRange = MonsterShuXing.PatrolRange;
        
        Points.Add(new Vector2(MonsterPos.x+PatrolRange,MonsterPos.y));  //第一个点
        Points.Add(new Vector2(MonsterPos.x - PatrolRange, MonsterPos.y));  //第二个点
        TargetPos = Points[NowPointIndex];  //第一个目标点
        WaitTime = MonsterShuXing.WaitTime;
        VisionRange= MonsterShuXing.VisionRange;
    }

    public override void EnterAiState()
    {
        Debug.Log("巡逻状态");
    }

    public override void ExitAiState()
    {
        Logic.Monster.NowDir = Vector3.zero;  //停止移动
        Logic.Monster.StandBy();  //切动画
    }

    public override void UpdateAiState()
    {
        if (!IsWait)
        {
            Logic.Monster.Move();
            //发现Player
            if (GetDistance(PlayerObject.PlayerPos.x,Logic.Monster.transform.position.x)<= VisionRange)
            {
                IsWait=true;
                Logic.Monster.NowDir = Vector2.zero;  //停止移动
                Logic.Monster.StandBy();  //切动画
                Logic.ChangeState(E_State.MOVE);  //切换状态
                return;
            }
            
            if (IsArrive)
            {
                Logic.Monster.NowDir = Vector2.zero;  //停止移动
                Logic.Monster.StandBy();  //切动画
                GetTargetPos();
                IsArrive = false;
            }
            else
            {
                //改变方向 朝着巡逻点移动
                Logic.Monster.NowDir.x = TargetPos.x-Logic.Monster.transform.position.x;
                
                //如果到达目标点附近
                if (GetDistance(Logic.Monster.transform.position.x, TargetPos.x) <= 0.1f)
                {
                    Logic.Monster.NowDir = Vector3.zero;  //停止移动
                    IsArrive =true;
                    IsWait=true;
                    Logic.Monster.TimeInterval(WaitTime, () => { IsWait = false;});
                }
            }
        }
       else
            Logic.Monster.StandBy();
    }
    private void GetTargetPos()  //得到一个目标点
    {    
        if (NowPointIndex != Points.Count - 1)
            NowPointIndex++;
        else
            NowPointIndex = 0;

        TargetPos = Points[NowPointIndex];
    }

}
