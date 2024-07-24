using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiBossStateMove : AiBossStateBase
{
    private float FirstMoveTime = 1f;  //��һ���ƶ��ȴ���ʱ��
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
        Logic.Boss.StandBy();  //�ж���
    }

    public override void UpdateAiState()
    {
        if (!IsWait)
        {
            Logic.Boss.Move();
            Logic.Boss.NowDir.x = PlayerObject.PlayerPos.x - Logic.Boss.transform.position.x;  //����Player����ȥ�ƶ�

            //Player����Monster�Ĺ�����Χ Monsterͣ��������Player
            if (GetDistance(PlayerObject.PlayerPos.x, Logic.Boss.transform.position.x) < Logic.Boss.MonsterShuXing.AtkRange)
            {
                IsWait = true;
                Logic.Boss.NowDir = Vector2.zero;  //ֹͣ�ƶ�
                Logic.Boss.StandBy();  //�ж���
                Logic.ChangeState(E_BossState.ATK);  //�л�״̬
                return;
            }
        }
        else
            Logic.Boss.StandBy();
    }
}
