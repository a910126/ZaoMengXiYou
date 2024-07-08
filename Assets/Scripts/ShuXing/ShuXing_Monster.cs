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
    /// 出生点
    /// </summary>
    public Vector3 BornPos;
    public ShuXing_Monster(int info) : base(info)
    {
        Speed= 2;
        BornPos=new Vector2(2,-2.5f);
        PatrolRange = 4;
        WaitTime = 0.5f;
        VisionRange = 6;
    }
}
