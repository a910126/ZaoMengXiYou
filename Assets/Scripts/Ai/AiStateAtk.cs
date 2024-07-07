using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStateAtk : AiStateBase
{
    private bool CanAtk = false;  //是否能够攻击

    private float FirstAtkTime = 1f;  //第一次攻击的间隔时间
    private float AtkCDTimeMin = 1.2f;  //攻击最小间隔时间
    private float AtkCDTimeMax = 2f;  //攻击最大间隔时间
    /// <summary>
    /// X方向的攻击距离
    /// </summary>
    private float AtkRangeX = 3f;  //X方向的攻击距离
    /// <summary>
    /// Y方向的攻击距离
    /// </summary>
    private float AtkRangeY = 0.5f;  //Y方向的攻击距离
    public AiStateAtk(AiLogic logic) : base(logic)
    {
    }

    public override void EnterAiState()
    {
        CanAtk = false;  //刚进来时先不能直接攻击，防止切其他状态时出错

        //利用协程去进行一个第一下攻击的等待时间
        //MonoMgr.GetInstance().StartCoroutine(FirstAtkWaitTime());
    }

    public override void ExitAiState()
    {

    }

    public override void UpdateAiState()
    {
        if (CanAtk)
        {
            Logic.Monster.Atk();
            //攻击结束后，修改状态不能攻击等待下次攻击           
            CanAtk = false;
            //利用协程去处理下一次攻击的等待时间
            MonoMgr.GetInstance().StartCoroutine(AtkWaitCDTime());
        }
        //如果monster是朝向右边的，那么如果player的x减去monster的x绝对值小于X轴上的攻击距离，
        //如果monster是朝向右边的，那么如果player的x减去monster的x绝对值小于X轴上的攻击距离，
        //或者player的y减去monster的y值大于Y轴上的攻击距离，或者player在monster的身后都会脱离ATK状态从而切换为MOVE状态
        //如果monster是朝向左边的，那么如果monster的x减去player的x值大于X轴上的攻击距离，
        //或者monster的y减去player的y值大于Y轴上的攻击距离，或者player在monster的身后都会脱离ATK状态从而切换为MOVE状态            
        //    if(Mathf.Abs(logic.monster.transform.position.y-PlayerObject.player.transform.position.y)>AtkRangeY||
        //       (logic.monster.IsRight && 
        //       (PlayerObject.player.transform.position.x - logic.monster.transform.position.x > AtkRangeX||
        //        PlayerObject.player.transform.position.x<logic.monster.transform.position.x)||
        //        !logic.monster.IsRight &&
        //       (logic.monster.transform.position.x - PlayerObject.player.transform.position.x > AtkRangeX ||
        //        logic.monster.transform.position.x < PlayerObject.player.transform.position.x)))
        //    {
        //        CanAtk = false;
        //        logic.ChangeState(E_State.MOVE);
        //    }
        //}

        IEnumerator FirstAtkWaitTime()  //利用协程来处理第一次攻击的前摇
        {
            yield return new WaitForSeconds(FirstAtkTime);
            CanAtk = true;
        }

        IEnumerator AtkWaitCDTime()  //利用协程处理下一次攻击的前摇（CD） 
        {
            yield return new WaitForSeconds(Random.Range(AtkCDTimeMin, AtkCDTimeMax));
            CanAtk = true;
        }
    }
}
