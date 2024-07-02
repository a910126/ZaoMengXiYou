using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : ObjectBase
{
    /// <summary>
    /// ��������
    /// </summary>
    private int AtkCount;

    /// <summary>
    /// ��Ծ����
    /// </summary>
    private int JumpCount;

    /// <summary>
    /// ��������������time�������������������߲�ͬģʽ�Ĺ�����
    /// </summary>
    private float AtkIntervalTime=1.5f;

    /// <summary>
    /// ��¼��ǰ����ʱ��
    /// </summary>
    private float NowAtkTime;

    /// <summary>
    /// ��¼��ǰ�������yֵ
    /// </summary>
    private float NowY;

    /// <summary>
    /// ��Ծ�ٶ�
    /// </summary>
    private float JumpSpeed =/*ShuXing.JumpSpeed*/5;

    /// <summary>
    /// ������ĵ�
    /// </summary>
    public GameObject CheckPoint;

    protected override void Awake()
    {
        base.Awake();
        ShuXing = new ShuXing_Player(1);

        InputMgr.GetInstance().StartOrEndCheck(true);  //�����������
        GetKeyCodePower();  //������������
    }

    protected override void Update()
    {
        base.Update();

        //��ͨ�����ڹ涨ʱ�������û���ڹ涨ʱ�����ٴδ�����������
        if (Time.time - NowAtkTime >= AtkIntervalTime)
            AtkCount = 0;

        if(JumpCount>=2 )
        CheckJump();  //������
        //print("JumpCount" + JumpCount);
    }

    #region ��������
    private void GetKeyCodePower()  //�õ�������Ȩ
    {
        EventCenter.GetInstance().AddEventListener<KeyCode>("SomeKeyDown", CheckKeyDown);
        EventCenter.GetInstance().AddEventListener<KeyCode>("SomeKeyUp", CheckKeyUp);
        EventCenter.GetInstance().AddEventListener<float>("Horizontal", CheckX);
        //EventCenter.GetInstance().AddEventListener<float>("SomeKeyDown", CheckY);
    }

    private void RemoveKeyCodePower()  //���ᰴ������Ȩ
    {
        EventCenter.GetInstance().RemoveEventListener<KeyCode>("SomeKeyDown", CheckKeyDown);
        EventCenter.GetInstance().RemoveEventListener<KeyCode>("SomeKeyUp", CheckKeyUp);
        EventCenter.GetInstance().RemoveEventListener<float>("Horizontal", CheckX);
        //EventCenter.GetInstance().RemoveEventListener<float>("SomeKeyDown", CheckY);
    }
    #endregion

    #region �������
    private void CheckKeyDown(KeyCode keyCode)  //��ⰴ������
    {
        switch(keyCode)
        {
            case KeyCode.W:
                break;
            case KeyCode.S:
                break;
            case KeyCode.J:
                Atk();
                break;
            case KeyCode.K:
                if(JumpCount<=2)
                Jump();
                break;
            case KeyCode.L:
                break;
            case KeyCode.Y:
                break;
            case KeyCode.U:
                break;
            case KeyCode.I:
                break;
            case KeyCode.O:
                break;
            case KeyCode.H:
                break;
            case KeyCode.Space:
                break;
        }
    }
    private void CheckKeyUp(KeyCode keyCode)  //��ⰴ��̧��
    {

    }

    private void CheckX(float x)  //����ƶ�
    {
        NowDir.x = x;

        //�жϳ���
        if (x > 0)
            Sprite.flipX = false;
        else if(x<0)
            Sprite.flipX = true;
    }
    //private void CheckY(float y)  
    //{

    //}
    #endregion

    public override void Atk()  //���﹥��
    {
        //AtkCount++;  //������ʽ����+1

        //NowAtkTime = Time.time;  //��¼��ǰ��ʱ��

        ////��ͨ���������� �ܹ���������ͨ������ʽ
        //if (AtkCount > 4)
        //{
        //    AtkCount = 1;
        //}

        LianXuAct("Atk",ref AtkCount,ref NowAtkTime);
        print(AtkCount + "������ʽ");
    }
    public void Jump()  //������Ծ
    {
        //JumpCount++;  //��Ծ����+1
        //NowJumpTime = Time.time;  //��¼��ǰ��ʱ��

        ////���ֻ�ܶ�����
        //if (JumpCount > 2)
        //{
        //    JumpCount = 1;
        //}

        //���ֻ�ܶ�����
        JumpCount++;
        if (JumpCount > 2)
            JumpCount = 3;

        //ʵ����Ծ
        if (JumpCount != 3)
            Rigidbody.velocity = new Vector2(0,JumpSpeed);
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(CheckPoint.transform.position, new Vector2(0.2f, 0.2f));
    }
#endif
    private void CheckJump()  //����Ƿ��ܹ��ٴ���Ծ
    {
        //��Χ��� ���ڽŵ��¼�����
        var collider = Physics2D.OverlapBox(CheckPoint.transform.position, new Vector2(0.2f, 0.2f), 0, LayerMask.GetMask("Ground"));
            if (collider != null)
            {
            
                JumpCount = 0;
                JumpSpeed = 0;
            }
            else
            {
                print("��ⲻ��");
                JumpCount = 3;
            }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name">������Ϊ������</param>
    /// <param name="count">������Ϊ�ļ���</param>
    /// <param name="nowtime">������Ϊ�ĵ�ǰʱ��</param>
    private void LianXuAct(string name,ref int count,ref float nowtime)  //����������Ϊ ����������������
    {
        count++;
        nowtime = Time.time;  //��¼��ǰ��ʱ��

        if (name == "Atk")
        {
            //��ͨ���������� �ܹ���������ͨ������ʽ
            if (count > 4)
            {
                count = 1;
            }
        }
        else if (name == "Jump")
        {
            
            if (count > 2)
            {
                count =3;
            }
        }
    }

    private void OnDestroy()
    {
        InputMgr.GetInstance().StartOrEndCheck(false);
        RemoveKeyCodePower();
    }

   

}
