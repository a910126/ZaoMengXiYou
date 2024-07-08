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
    /// ������
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
