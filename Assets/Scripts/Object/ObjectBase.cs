using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class ObjectBase : MonoBehaviour
{
    /// <summary>
    /// 移动方向
    /// </summary>
    public Vector2 NowDir = Vector2.zero;

    ///// <summary>
    ///// 处理连击（在time内连击触发二段跳或者不同模式的攻击）
    ///// </summary>
    //protected float time;

    /// <summary>
    /// Object的SpriteRenderer
    /// </summary>
    public SpriteRenderer Sprite;

    /// <summary>
    /// 属性
    /// </summary>
    protected ShuXingBase ShuXing;

    /// <summary>
    /// 是否死亡
    /// </summary>
    public bool IsDead;

    /// <summary>
    /// 与时间相关的事情 要利用通用的时间间隔
    /// </summary>
    protected UnityAction Act_With_Time;

    /// <summary>
    /// 自身的刚体
    /// </summary>
    protected Rigidbody2D Rigidbody;

    /// <summary>
    /// 自身的碰撞体
    /// </summary>
    protected Collider2D Collider;

    protected virtual void Awake()
    {
        Sprite = this.gameObject.GetComponent<SpriteRenderer>();
        Rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
        Collider = this.gameObject.GetComponent<Collider2D>();
    }
    protected virtual void Start()
    {
        
    }
    protected virtual void Update()
    {
        this.gameObject.transform.Translate(Time.deltaTime * ShuXing.Speed * NowDir.normalized);  //移动
    }

    #region 利用协程写通用的计时器
    public void TimeInterval(float time, UnityAction act)  //时间间隔  计时器
    {
        Act_With_Time += act;
        StartCoroutine(ReallyTimeInterVal(time));
    }

    IEnumerator ReallyTimeInterVal(float time)
    {
        yield return new WaitForSeconds(time);
        if (Act_With_Time!=null)
        Act_With_Time.Invoke();
    }

    #endregion

    public abstract void Atk();  //攻击
    public abstract void Hurt(float value);  //受伤
    public abstract void Dead();  //死亡

    public abstract void InitShuXing();  //初始化属性
}
