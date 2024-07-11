using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStateAtk : AiStateBase
{
    private float FirstAtkTime = 0.7f;  //第一次攻击的间隔时间

    private float AtkCDTimeMin;  //攻击最小间隔时间

    private float AtkCDTimeMax;  //攻击最大间隔时间

    private float AtkRange;  //攻击距离

    private bool IsCanAtk;  //是否能够攻击
    public AiStateAtk(AiLogic logic) : base(logic)
    {
        AtkRange=MonsterShuXing.AtkRange;
        AtkCDTimeMin=MonsterShuXing.AtkMinTime;
        AtkCDTimeMax=MonsterShuXing.AtkMaxTime;
    }

    public override void EnterAiState()
    {
        Debug.Log("攻击状态");
        IsCanAtk = false;  //刚进来时先不能直接攻击，防止切其他状态时出错

        //利用协程去进行一个第一下攻击的等待时间
        Logic.Monster.TimeInterval(FirstAtkTime, () => { IsCanAtk = true; });
    }

    public override void ExitAiState()
    {
        IsCanAtk = false;
    }

    public override void UpdateAiState()
    {
        if (IsCanAtk)
        {
            Logic.Monster.Atk();

            //攻击结束后，修改状态不能攻击等待下次攻击           
            IsCanAtk = false;
            Logic.Monster.StandBy();  //切动画

            //利用协程去处理下一次攻击的等待时间
            Logic.Monster.TimeInterval(Random.Range(AtkCDTimeMin, AtkCDTimeMax), () => { IsCanAtk = true; });
        }

        //如果Player超出Monster的攻击距离 就切换状态追击Player
        if (GetDistance(PlayerObject.PlayerPos.x, Logic.Monster.transform.position.x) > AtkRange)
        {
            IsCanAtk=false;
            Logic.ChangeState(E_State.MOVE);  //切换状态
            return;
        }
    }
}
