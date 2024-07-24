using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiBossStateAtk : AiBossStateBase
{
    private float FirstAtkTime = 0.7f;  //第一次攻击的间隔时间

    private float AtkCDTimeMin;  //攻击最小间隔时间

    private float AtkCDTimeMax;  //攻击最大间隔时间

    private float AtkRange;  //攻击距离

    private bool IsCanAtk;  //是否能够攻击
    public AiBossStateAtk(AiBossLogic logic) : base(logic)
    {
        AtkRange = MonsterShuXing.AtkRange;
        AtkCDTimeMin = MonsterShuXing.AtkMinTime;
        AtkCDTimeMax = MonsterShuXing.AtkMaxTime;
    }

    public override void EnterAiState()
    {
        IsCanAtk = false;  //刚进来时先不能直接攻击，防止切其他状态时出错

        //利用协程去进行一个第一下攻击的等待时间
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

            //攻击结束后，修改状态不能攻击等待下次攻击           
            IsCanAtk = false;
            Logic.Boss.StandBy();  //切动画

            //利用协程去处理下一次攻击的等待时间
            Logic.Boss.TimeInterval(Random.Range(AtkCDTimeMin, AtkCDTimeMax), () => { IsCanAtk = true; });
        }

        //如果Player超出Monster的攻击距离 就切换状态追击Player
        if (GetDistance(PlayerObject.PlayerPos.x, Logic.Boss.transform.position.x) > AtkRange)
        {
            IsCanAtk = false;
            Logic.ChangeState(E_BossState.MOVE);  //切换状态
            return;
        }
    }

}
