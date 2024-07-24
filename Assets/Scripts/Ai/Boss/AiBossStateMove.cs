using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiBossStateMove : AiBossStateBase
{
    private float FirstMoveTime = 1f;  //第一次移动等待的时间
    public AiBossStateMove(AiBossLogic logic) : base(logic)
    {
    }

    public override void EnterAiState()
    {
        Logic.Boss.NowDir = Vector2.zero;
        IsWait = true;
        Logic.Boss.TimeInterval(FirstMoveTime, () => { IsWait = false; });
    }

    public override void ExitAiState()
    {
        Logic.Boss.StandBy();  //切动画
    }

    public override void UpdateAiState()
    {
        if (!IsWait)
        {
            Logic.Boss.Move();
            Logic.Boss.NowDir.x = PlayerObject.PlayerPos.x - Logic.Boss.transform.position.x;  //朝着Player方向去移动

            //Player到达Monster的攻击范围 Monster停下来攻击Player
            if (GetDistance(PlayerObject.PlayerPos.x, Logic.Boss.transform.position.x) < Logic.Boss.MonsterShuXing.AtkRange)
            {
                IsWait = true;
                Logic.Boss.NowDir = Vector2.zero;  //停止移动
                Logic.Boss.StandBy();  //切动画
                Logic.ChangeState(E_BossState.ATK);  //切换状态
                return;
            }
        }
        else
            Logic.Boss.StandBy();
    }
}
