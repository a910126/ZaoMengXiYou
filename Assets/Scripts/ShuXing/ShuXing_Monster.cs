using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuXing_Monster : ShuXingBase
{
    /// <summary>
    /// 巡逻范围
    /// </summary>
    public float PatrolRange;

    /// <summary>
    /// 到达目标点停下多长时间
    /// </summary>
    public float WaitTime;

    /// <summary>
    /// 视野范围
    /// </summary>
    public float VisionRange;

    /// <summary>
    /// 攻击最小间隔时间
    /// </summary>
    public float AtkMinTime;

    /// <summary>
    /// 攻击最大间隔时间
    /// </summary>
    public float AtkMaxTime;

    /// <summary>
    /// 出生点
    /// </summary>
    public Vector3 BornPos;
    public ShuXing_Monster(int info) : base(info)
    {
        Speed= 2;
        BornPos=new Vector2(2,-2.5f);
        PatrolRange = 4;
        WaitTime = 2f;
        VisionRange = 6;
        AtkRange = 2f;
        AtkMinTime = 1.2f;
        AtkMaxTime = 2f;
    }
}
