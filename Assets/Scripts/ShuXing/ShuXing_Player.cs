using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuXing_Player : ShuXingBase
{
    /// <summary>
    /// 最大蓝量
    /// </summary>
    public int MaxMp;

    /// <summary>
    /// 当前蓝量
    /// </summary>
    public int NowMp;

    /// <summary>
    /// 升级所需经验值
    /// </summary>
    public int MaxExp;

    /// <summary>
    /// 当前经验值
    /// </summary>
    public int NowExp;

    /// <summary>
    /// 人物等级
    /// </summary>
    public int Level;

    /// <summary>
    /// 灵魂
    /// </summary>
    public int Soul;

    /// <summary>
    /// 幸运
    /// </summary>
    public int Luck;

    /// <summary>
    /// 跳跃高度
    /// </summary>
    public float JumpHeight;

    /// <summary>
    /// 跳跃速度
    /// </summary>
    public float JumpTime; 

    /// <summary>
    /// 战斗力
    /// </summary>
    public int ZhanDouLi;

    /// <summary>
    /// 暴击率
    /// </summary>
    public float Crit;

    public ShuXing_Player(int info):base(info)
    {
        Speed = 5;
        JumpHeight = 6.5f;
    }
}
