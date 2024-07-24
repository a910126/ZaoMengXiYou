using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiBossStateAtk : AiBossStateBase
{
    private float FirstAtkTime = 0.7f;  //��һ�ι����ļ��ʱ��

    private float AtkCDTimeMin;  //������С���ʱ��

    private float AtkCDTimeMax;  //���������ʱ��

    private float AtkRange;  //��������

    private bool IsCanAtk;  //�Ƿ��ܹ�����
    public AiBossStateAtk(AiBossLogic logic) : base(logic)
    {
        AtkRange = MonsterShuXing.AtkRange;
        AtkCDTimeMin = MonsterShuXing.AtkMinTime;
        AtkCDTimeMax = MonsterShuXing.AtkMaxTime;
    }

    public override void EnterAiState()
    {
        IsCanAtk = false;  //�ս���ʱ�Ȳ���ֱ�ӹ�������ֹ������״̬ʱ����

        //����Э��ȥ����һ����һ�¹����ĵȴ�ʱ��
        Logic.Boss.TimeInterval(FirstAtkTime, () => { IsCanAtk = true; });
    }

    public override void ExitAiState()
    {
        Logic.Boss.NowDir.x = 0;
    }

    public override void UpdateAiState()
    {
        if (IsCanAtk)
        {
            Logic.Boss.Atk();

            //�����������޸�״̬���ܹ����ȴ��´ι���           
            IsCanAtk = false;
            Logic.Boss.StandBy();  //�ж���

            //����Э��ȥ������һ�ι����ĵȴ�ʱ��
            Logic.Boss.TimeInterval(Random.Range(AtkCDTimeMin, AtkCDTimeMax), () => { IsCanAtk = true; });
        }

        //���Player����Monster�Ĺ������� ���л�״̬׷��Player
        if (GetDistance(PlayerObject.PlayerPos.x, Logic.Boss.transform.position.x) > AtkRange)
        {
            IsCanAtk = false;
            Logic.ChangeState(E_BossState.MOVE);  //�л�״̬
            return;
        }
    }

}
