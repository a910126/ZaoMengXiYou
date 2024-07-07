using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class AiStateMove : AiStateBase
{
    private Vector3 targetpos;  //目标点的位置

    private float FirstMoveTime = 1f;  //第一次移动等待的时间

    private int maxRange = 5;  //最大范围，拉怪返回出生点

    public AiStateMove(AiLogic logic) : base(logic)
    {
    }

    public override void EnterAiState()
    {
        //IsWait = true;
        Debug.Log("追击状态");
        Logic.Monster.NowDir = Vector2.zero;
        //MonoMgr.GetInstance().StartCoroutine(FirstWaitTime());
    }

    public override void ExitAiState()
    {

    }

    public override void UpdateAiState()
    {
        //if (Vector3.Distance(logic.monster.transform.position, logic.monster.bornPos) > maxRange)
        //{
        //    logic.monster.Move(Vector2.zero);
        //    logic.ChangeState(E_State.BACK);
        //    return;
        //}

        if (!IsWait)
        {
            //if()
        }

        //if (IsWait) return;
        ////利用三目运算符得到目标点的位置，如果monster是在player的右边的话（monster的x大于player的x）
        ////目标点就是player的Vector3.Right的前面两个位置，如果是monster在player的左边的话（monster的x小于player的x）
        ////目标点就是在player的Vector3.Left的后面两个位置（不管在哪边目标点都是在player的前面）
        //targetpos=PlayerObject.player.transform.position.x<=logic.monster.transform.position.x
        //    ?PlayerObject.player.transform.position+Vector3.right*2
        //    :PlayerObject.player.transform.position+ Vector3.left*2;
        ////得到目标点后就朝着目标点移动 方向就是目标点的位置减去monster的位置
        //logic.monster.Move(targetpos-logic.monster.transform.position);
        
        //if (Vector3.Distance(targetpos, logic.monster.transform.position)<0.2f)
        //{
        //    logic.monster.Move(Vector2.zero);
        //    logic.ChangeState(E_State.ATK);
        //}        
    }

    IEnumerator FirstWaitTime()
    {
        yield return new WaitForSeconds(FirstMoveTime);
        IsWait = false;
    }
}
