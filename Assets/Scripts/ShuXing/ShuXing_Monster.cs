using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuXing_Monster : ShuXingBase
{
    /// <summary>
    /// Ѳ�߷�Χ
    /// </summary>
    public float PatrolRange;

    /// <summary>
    /// ����Ŀ���ͣ�¶೤ʱ��
    /// </summary>
    public float WaitTime;

    /// <summary>
    /// ��Ұ��Χ
    /// </summary>
    public float VisionRange;

    /// <summary>
    /// ������С���ʱ��
    /// </summary>
    public float AtkMinTime;

    /// <summary>
    /// ���������ʱ��
    /// </summary>
    public float AtkMaxTime;

    /// <summary>
    /// ������
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
