using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class ObjectBase : MonoBehaviour
{
    /// <summary>
    /// �ƶ�����
    /// </summary>
    protected Vector2 NowDir = Vector2.zero;

    ///// <summary>
    ///// ������������time�������������������߲�ͬģʽ�Ĺ�����
    ///// </summary>
    //protected float time;

    /// <summary>
    /// Object��SpriteRenderer
    /// </summary>
    protected SpriteRenderer Sprite;

    /// <summary>
    /// ����
    /// </summary>
    protected ShuXingBase ShuXing;

    /// <summary>
    /// ��ʱ����ص����� Ҫ����ͨ�õ�ʱ����
    /// </summary>
    protected UnityAction Act_With_Time;

    /// <summary>
    /// ����ĸ���
    /// </summary>
    protected Rigidbody2D Rigidbody;

    /// <summary>
    /// �������ײ��
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
        this.gameObject.transform.Translate(Time.deltaTime * /*ShuXing.Speed*/5 * NowDir);  //�ƶ�
    }

    #region ����Э��дͨ�õļ�ʱ��
    protected void TimeInterval(float time, UnityAction act)  //ʱ����  ��ʱ��
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

    public abstract void Atk();  //����

    public abstract void Hurt(float value);  //����

    public abstract void Dead();  //����
}
