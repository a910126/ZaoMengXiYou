using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class AiBossStateBase
{
    protected AiBossLogic Logic;

    private float MonsterSpeed;  //Monster���ٶ�

    protected float Distance;  //Player��Monster֮��ľ���

    protected bool IsWait = false;  //�Ƿ���Ҫ�ȴ�

    protected ShuXing_Monster MonsterShuXing;  //Monster����
    public AiBossStateBase(AiBossLogic logic)
    {
        this.Logic = logic;

        MonsterShuXing = logic.Boss.MonsterShuXing;

        MonoMgr.GetInstance().AddUpdateListener(TurnAround);  //����MonoMgrʹ��Update

        MonsterSpeed = MonsterShuXing.Speed;
    }

    public abstract void EnterAiState();  //�ս��봦���߼�
    public abstract void UpdateAiState();  //�����߼�
    public abstract void ExitAiState();  //�˳������߼�

    private void StopMove()  //ֹͣMove
    {
        MonsterShuXing.Speed = 0;
    }

    private void StartMove()  //��ʼMove
    {
        MonsterShuXing.Speed = MonsterSpeed;
    }
    private void TurnAround()  //ת��
    {
        if (Logic.Boss.NowDir.x > 0)
            Logic.Boss.Sprite.flipX = true;
        else if (Logic.Boss.NowDir.x < 0)
            Logic.Boss.Sprite.flipX = false;
    }
    protected float GetDistance(float pos, float monster)  //����Monster��Player��TargetPos�ľ���
    {
        if ((pos >= 0 && monster >= 0) || (pos < 0 && monster < 0))
            Distance = Math.Abs(pos - monster);
        else if ((pos > 0 && monster < 0) || (monster > 0 && pos < 0))
        {
            if (monster < 0)
                Distance = Math.Abs(monster) + pos;
            else if (pos < 0)
                Distance = Math.Abs(pos) + monster;
        }
        return Distance;
    }
}
