using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class ObjectBase : MonoBehaviour
{
    /// <summary>
    /// 移动方向
    /// </summary>
    protected Vector2 NowDir = Vector2.zero;

    ///// <summary>
    ///// 处理连击（在time内连击触发二段跳或者不同模式的攻击）
    ///// </summary>
    //protected float time;

    /// <summary>
    /// Object的SpriteRenderer
    /// </summary>
    protected SpriteRenderer Sprite;

    /// <summary>
    /// 属性
    /// </summary>
    protected ShuXingBase ShuXing;

    /// <summary>
    /// 与时间相关的事情 要利用通用的时间间隔
    /// </summary>
    protected UnityAction Act_With_Time;

    protected Rigidbody2D Rigidbody;

    protected virtual void Awake()
    {
        Sprite = this.gameObject.GetComponent<SpriteRenderer>();
        Rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
    }
    protected virtual void Start()
    {
        
    }
    protected virtual void Update()
    {
        this.gameObject.transform.Translate(Time.deltaTime * /*ShuXing.Speed*/5 * NowDir);  //移动
    }

    #region 利用协程写通用的计时器
    protected void TimeInterval(float time, UnityAction act)  //时间间隔  计时器
    {
        Act_With_Time += act;
        StartCoroutine(ReallyTimeInterVal(time));
    }

    IEnumerator ReallyTimeInterVal(float time)
    {
        if(Act_With_Time!=null)
        Act_With_Time.Invoke();
        yield return new WaitForSeconds(time);
    }

    #endregion



    public abstract void Atk();  //攻击
}
