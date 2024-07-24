using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class AiStateMove : AiStateBase
{
    private float FirstMoveTime = 1f;  //第一次移动等待的时间

    public AiStateMove(AiLogic logic) : base(logic)
    {
    }

    public override void EnterAiState()
    {
        Debug.Log("追击状态");
        Logic.Monster.NowDir = Vector2.zero;
        IsWait = true;
        Logic.Monster.TimeInterval(FirstMoveTime, () => { IsWait = false; });
    }

    public override void ExitAiState()
    {
        Logic.Monster.StandBy();  //切动画
    }

    public override void UpdateAiState()
    {
        if (!IsWait)
        {
            Logic.Monster.Move();
            Logic.Monster.NowDir.x = PlayerObject.PlayerPos.x - Logic.Monster.transform.position.x;  //朝着Player方向去移动

            //Player到达Monster的攻击范围 Monster停下来攻击Player
            if (GetDistance(PlayerObject.PlayerPos.x, Logic.Monster.transform.position.x) <Logic.Monster.MonsterShuXing.AtkRange)
            {
                IsWait= true;   
                Logic.Monster.NowDir = Vector2.zero;  //停止移动
                Logic.Monster.StandBy();  //切动画
                Logic.ChangeState(E_State.ATK);  //切换状态
                return;
            }
        }
        else
            Logic.Monster.StandBy();
    }
}
