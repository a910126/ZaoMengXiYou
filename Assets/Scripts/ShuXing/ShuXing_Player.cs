using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuXing_Player : ShuXingBase
{
    /// <summary>
    /// �������
    /// </summary>
    public int MaxMp;

    /// <summary>
    /// ��ǰ����
    /// </summary>
    public int NowMp;

    /// <summary>
    /// �������辭��ֵ
    /// </summary>
    public int MaxExp;

    /// <summary>
    /// ��ǰ����ֵ
    /// </summary>
    public int NowExp;

    /// <summary>
    /// ����ȼ�
    /// </summary>
    public int Level;

    /// <summary>
    /// ���
    /// </summary>
    public int Soul;

    /// <summary>
    /// ����
    /// </summary>
    public int Luck;

    /// <summary>
    /// ��Ծ�߶�
    /// </summary>
    public float JumpHeight;

    /// <summary>
    /// ��Ծ�ٶ�
    /// </summary>
    public float JumpTime; 

    /// <summary>
    /// ս����
    /// </summary>
    public int ZhanDouLi;

    /// <summary>
    /// ������
    /// </summary>
    public float Crit;

    public ShuXing_Player(int info):base(info)
    {
        Speed = 5;
        JumpHeight = 6.5f;
    }
}
